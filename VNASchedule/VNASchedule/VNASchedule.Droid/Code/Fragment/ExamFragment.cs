using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using SQLite;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Adapter;
using VNASchedule.Droid.Code.Class;
using static Android.App.ActionBar;
using static Android.Widget.ExpandableListView;
using static VNASchedule.Droid.Code.Class.CmmDroidEvent;

namespace VNASchedule.Droid.Code.Fragment
{
    //
    public class ExamFragment : Android.App.Fragment, IOnGroupClickListener
    {
        public MainActivity mainAct;
        private LayoutInflater _inflater;
        View view;
        private List<BeanSurvey> lst_survey_Full;
        private ExpandableListView listViewExam;
        private SurveyAdapter surveyAdapter;
        private LinearLayout ln_NoData;
        private LinearLayoutManager Mliner;
        private LinearLayout ln_Search, _lnSearch;
        private EditText _edtSearch;
        private ImageView _imgDeleteSearch;
        private Switch switchUngraded;
        private LinearLayout ln_Type;
        private TextView tv_Type;
        private PopupWindow _popupType;
        private View popupView;
        private LinearLayout ln_viewLine;
        private List<BeanSurveyCategory> surveyCategories = new List<BeanSurveyCategory>();
        private TypeCategoryExamAdapter _CategoryTypeAdapter;
        private Dictionary<BeanSurvey, List<BeanSurvey>> diction_lst_survey = new Dictionary<BeanSurvey, List<BeanSurvey>>();
        private int categoryId;

        public LinearLayoutManager MLayoutManager { get => Mliner; set => Mliner = value; }


        public enum EnumSortCourse
        {
            All = 0,
            Ascending = 1,
            Descending = 2,
        }
        private int SortType = (int)EnumSortCourse.All;

        public ExamFragment() { }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (view == null)
            {
                view = inflater.Inflate(Resource.Layout.Exam, container, false);
                mainAct = (MainActivity)this.Activity;
                _inflater = inflater;

                listViewExam = view.FindViewById<ExpandableListView>(Resource.Id.listview_Exam);
                ln_Search = view.FindViewById<LinearLayout>(Resource.Id.linear_exam_Search);
                ln_NoData = view.FindViewById<LinearLayout>(Resource.Id.ln_EmptyData_Exam);
                switchUngraded = view.FindViewById<Switch>(Resource.Id.switchLanguage);
                ln_Type = view.FindViewById<LinearLayout>(Resource.Id.ln_Type);
                tv_Type = view.FindViewById<TextView>(Resource.Id.tv_Type);
                ln_viewLine = view.FindViewById<LinearLayout>(Resource.Id.ln_viewLine);

                MLayoutManager = new LinearLayoutManager(view.Context);

                ln_Search.Visibility = ViewStates.Gone;

                _lnSearch = view.FindViewById<LinearLayout>(Resource.Id.ln_Exam_Search);
                _edtSearch = view.FindViewById<EditText>(Resource.Id.edt_Exam_Search);
                _imgDeleteSearch = view.FindViewById<ImageView>(Resource.Id.img_Exam_Search);
                _lnSearch.Visibility = ViewStates.Visible;
                
                _edtSearch.TextChanged += TextChanged_EdtSearch;
                _imgDeleteSearch.Click += Click_imgDeleteSearch;
                switchUngraded.CheckedChange += SwitchUngraded_CheckedChange;
                ln_Type.Click += Ln_Type_Click;

                CmmDroidEvent.PagerTraining_imgSearchEvent += Click_imgSearch;
                CmmDroidEvent.ClickItemSort_PagerTrainingEvent += Click_ItemSort_FromParentFragment;
                new Handler().PostDelayed(() =>
                {
                    setData(categoryId);
                    LoadListCategory();
                }, 200);
            }
            return view;
        }

        private void LoadListCategory()
        {
            //var conn = new SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                #region Init View
                popupView = _inflater.Inflate(Resource.Layout.PopupTypeCategoryExam, null);
                var rcl_filter_TypeExam = popupView.FindViewById<RecyclerView>(Resource.Id.rcl_filter_TypeExam);
                #endregion

                var _query = "SELECT * FROM BeanSurveyCategory ORDER BY Title";
                var allCategories = new BeanSurveyCategory { Title = "All", ID = 0, Modified = null };
                surveyCategories.Add(allCategories);
                //surveyCategories.AddRange(conn.Query<BeanSurveyCategory>(_query));
                surveyCategories.AddRange(SQLiteHelper.GetList<BeanSurveyCategory>(_query).ListData);

                if (surveyCategories != null && surveyCategories.Count > 0)
                {
                    rcl_filter_TypeExam.SetLayoutManager(new LinearLayoutManager(mainAct, LinearLayoutManager.Vertical, false));
                    _CategoryTypeAdapter = new TypeCategoryExamAdapter(view.Context, surveyCategories);
                    _CategoryTypeAdapter.mSelectedItem = 0;
                    _CategoryTypeAdapter.ItemClick += _CategoryTypeAdapter_ItemClick;
                    rcl_filter_TypeExam.SetAdapter(_CategoryTypeAdapter);
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
            finally
            {
                //conn.Close();
            }
        }

        private void Ln_Type_Click(object sender, EventArgs e)
        {
            #region Config Popup
            _popupType = new PopupWindow(popupView, LayoutParams.MatchParent, LayoutParams.MatchParent - (int)CmmDroidFunction.convertDpToPixel(95f, view.Context))
            {
                Focusable = true,
                OutsideTouchable = true
            };
            _popupType.ShowAsDropDown(ln_viewLine);
            #endregion
        }

        private void _CategoryTypeAdapter_ItemClick(object sender, int pos)
        {
            try
            {
                if (pos != -1)
                {
                    if (surveyCategories[pos] != null)
                    {
                        tv_Type.Text = surveyCategories[pos].Title;
                        categoryId = surveyCategories[pos].ID;
                        setData(categoryId);
                    }
                    _popupType.Dismiss();
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
        }

        private void SwitchUngraded_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            try
            {
                bool isChecked = e.IsChecked;
                if (isChecked)
                {
                    lst_survey_Full = lst_survey_Full.FindAll(r => r.Status == 1).ToList();
                    SetList(lst_survey_Full);
                    _edtSearch.Text = _edtSearch.Text;
                    _edtSearch.SetSelection(_edtSearch.Text.Length);
                }
                else
                    setData(categoryId);
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
        }

        private void setData(int filterType)
        {
            //var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                //ProviderBase provider = new ProviderBase();
                //provider.UpdateMasterData<BeanSurvey>(true);
                //provider.UpdateAllDynamicData(true, CmmVariable.SysConfig.DataLimitDay, false);
                string query = "";
                if (filterType == 0 || filterType == -1) // default hoặc chưa filter
                {
                    if (SortType == (int)EnumSortCourse.Ascending)
                    {
                        query = string.Format(@"SELECT * FROM BeanSurvey where (Status <> -1 or Status <> -2)  ORDER BY Title ASC");
                    }
                    else if (SortType == (int)EnumSortCourse.Descending)
                    {
                        query = string.Format(@"SELECT * FROM BeanSurvey where (Status <> -1 or Status <> -2)  ORDER BY Title DESC");
                    }
                    else // All
                    {
                        query = string.Format(@"SELECT * FROM BeanSurvey where (Status <> -1 or Status <> -2)  ORDER BY Created DESC");
                    }
                }
                else
                    query = string.Format(@"SELECT * FROM BeanSurvey where (Status <> -1 or Status <> -2) AND SurveyCategoryId = {0} ORDER BY Created DESC", filterType);

                //lst_survey_Full = conn.Query<BeanSurvey>(query);
                lst_survey_Full = SQLiteHelper.GetList<BeanSurvey>(query).ListData;
                SetList(lst_survey_Full);
                //conn.Close();
                _edtSearch.Text = _edtSearch.Text;
                _edtSearch.SetSelection(_edtSearch.Text.Length); // focus vào character cuối cùng
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
            finally
            {
                //conn.Close();
            }
        }

        private void SetList(List<BeanSurvey> lst_survey)
        {
            try
            {
                if (lst_survey != null && lst_survey.Count > 0)
                {
                    var expands = GroupListExam(lst_survey);

                    listViewExam.SetGroupIndicator(null);
                    listViewExam.SetChildIndicator(null);
                    listViewExam.DividerHeight = 0;
                    listViewExam.SetOnGroupClickListener(this);
                    //listViewExam.SetLayoutManager(MLayoutManager);
                    surveyAdapter = new SurveyAdapter(expands, view.Context, mainAct);
                    listViewExam.SetAdapter(surveyAdapter);
                    for (int i = 0; i < listViewExam.ExpandableListAdapter.GroupCount; i++)
                    {
                        //Expand group
                        listViewExam.ExpandGroup(i);
                    }
                    surveyAdapter.GroupClick += SurveyAdapter_GroupClick;
                    surveyAdapter.ChildClick += SurveyAdapter_ChildClick;
                    surveyAdapter.ExpandOrCollapse += SurveyAdapter_ExpandOrCollapse;
                    //surveyAdapter.ItemClick += ViewTraining;
                    ln_NoData.Visibility = ViewStates.Gone;
                }
                else
                {
                    ln_NoData.Visibility = ViewStates.Visible;
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
        }

        private void SurveyAdapter_ChildClick(object sender, BeanSurvey e)
        {
            ViewTraining(e);
        }

        private void SurveyAdapter_GroupClick(object sender, BeanSurvey e)
        {
            ViewTraining(e);
        }

        private void SurveyAdapter_ExpandOrCollapse(object sender, int groupPosition)
        {

            if (listViewExam.IsGroupExpanded(groupPosition))
                listViewExam.CollapseGroup(groupPosition);
            else
                listViewExam.ExpandGroup(groupPosition);
        }

        private List<BeanExpandExam> GroupListExam(List<BeanSurvey> surveys)
        {
            List<BeanExpandExam> beanExpandExams = new List<BeanExpandExam>();
            try
            {
                var newList = surveys.FindAll(r => string.IsNullOrEmpty(r.ParentId)).ToList().Count > 0 ? surveys.FindAll(r => string.IsNullOrEmpty(r.ParentId)).ToList() : surveys.FindAll(r => !string.IsNullOrEmpty(r.ParentId)).ToList();
                foreach (var item in newList)
                {
                    BeanExpandExam expandExam = new BeanExpandExam();
                    expandExam.ListSurvey = new List<BeanSurvey>();
                    var groups = surveys.Where(r => item.ID.Equals(r.ParentId)).ToList();
                    if (groups != null && groups.Count > 0)
                    {
                        expandExam = new BeanExpandExam
                        {
                            Survey = item,
                            ListSurvey = new List<BeanSurvey>(groups)
                        };
                    }
                    else
                        expandExam.Survey = item;

                    beanExpandExams.Add(expandExam);
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
            return beanExpandExams;
        }

        private void ViewTraining(BeanSurvey survey)
        {
            if (CmmDroidFunction.hasConnection())
            {
                try
                {
                    if (CmmDroidFunction.PreventMultipleClick() == true)
                    {
                        if (survey != null)
                        {
                            //if (survey.PermissionType == true)
                            //{
                            //    SurveyListPilot surveyListPilot = new SurveyListPilot(survey, this);
                            //    mainAct.ShowFragment(mainAct.FragmentManager, surveyListPilot, "SurveyListPilot");
                            //}
                            //else
                            //{
                            //    DetailExamFragment examFragment = new DetailExamFragment(survey);
                            //    mainAct.ShowFragment(mainAct.FragmentManager, examFragment, "SurveyListPilot");
                            //}
                            DetailExamFragment examFragment = new DetailExamFragment(survey);
                            mainAct.ShowFragment(mainAct.FragmentManager, examFragment, "SurveyListPilot");
                        }
                    }
                }
                catch (Exception ex)
                {
                    CmmDroidFunction.logErr(ex);
                }
            }
            else
            {
                mainAct.RunOnUiThread(() =>
                {
                    try
                    {
                        Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(view.Context);
                        alert.SetTitle("VietnamAirlines");
                        alert.SetMessage("Please connect to the internet!");
                        alert.SetCancelable(false);

                        alert.SetNegativeButton("Ok", (senderAlert, args) =>
                        {

                            alert.Dispose();
                        });
                        Dialog dialog = alert.Create();
                        dialog.Show();
                    }
                    catch (Exception)
                    {


                    }

                });
            }

        }

        /*private void ViewTraining(object sender, int e)
        {
            try
            {
                if (CmmDroidFunction.PreventMultipleClick() == true)
                {
                    List<BeanSurvey> _lstData = surveyAdapter.GetListData();

                    if (_lstData != null && _lstData.Count > 0)
                    {
                        if (_lstData[e].PermissionType == true)
                        {
                            SurveyListPilot surveyListPilot = new SurveyListPilot(_lstData[e], this);
                            mainAct.ShowFragment(mainAct.FragmentManager, surveyListPilot, "SurveyListPilot");
                        }
                        else
                        {
                            DetailExamFragment examFragment = new DetailExamFragment(_lstData[e]);
                            mainAct.ShowFragment(mainAct.FragmentManager, examFragment, "SurveyListPilot");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
        }*/


        /*private List<BeanExpandExam> CustomList(List<BeanSurvey> lst_survey)
        {
            List<BeanExpandExam> expandExams = new List<BeanExpandExam>();
            List<BeanExpandExam> expandExams2 = new List<BeanExpandExam>();

            for (int i =0;i< lst_survey.Count; i++)
            {
                if (CheckExist(lst_survey[i].ParentId, expandExams2))
                {
                    expandExams2.
                }
            }

        }

        private bool CheckExist(string ID, List<BeanExpandExam> lst_survey)
        {

            bool check = false;
            for (int i = 0; i < lst_survey.Count; i++)
            {
                BeanExpandExam beanEx = lst_survey[i];
                if (beanEx.Survey.ID== ID)
                {
                    check= true;
                    break;
                }
            }
            return check;
        }
        */
        private void TextChanged_EdtSearch(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(_edtSearch.Text))
                {
                    _imgDeleteSearch.Visibility = ViewStates.Visible;

                    string _content = CmmFunction.RemoveSignVietnamese(_edtSearch.Text).ToLowerInvariant();
                    List<BeanSurvey> _lstSearch = lst_survey_Full.FindAll(survey => (CmmFunction.RemoveSignVietnamese(survey.Title).ToLowerInvariant().Contains(_content)));
                    SetList(_lstSearch);
                }
                else
                {
                    _imgDeleteSearch.Visibility = ViewStates.Gone;
                    SetList(lst_survey_Full);
                }

            }
            catch (Exception ex)
            {
                CmmDroidFunction.logErr(ex);
            }
        }
        public void Click_imgSearch(object sender, PagerTraining_imgSearch e)
        {
            if (e.pagerPosition == 2) // đúng tab
            {
                if (_lnSearch.Visibility == ViewStates.Gone)
                {
                    _lnSearch.Visibility = ViewStates.Visible;

                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    _edtSearch.RequestFocus();
                    inputMethodManager.ShowSoftInput(view, 0);
                    inputMethodManager.ToggleSoftInput(ShowFlags.Forced, 0);
                }
                else
                {
                    _lnSearch.Visibility = ViewStates.Gone;

                    _edtSearch.Text = "";
                    InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(_edtSearch.WindowToken, 0);
                }
            }
        }
        private void Click_imgDeleteSearch(object sender, EventArgs e)
        {
            _edtSearch.Text = "";
        }
        private void Click_ItemSort_FromParentFragment(object sender, ClickItemSort_PagerTraining e)
        {
            try
            {
                if (e.pagerPosition == 2) // đúng tab
                {
                    switch (e.sortType.ToLowerInvariant())
                    {
                        case "0": // All
                            {
                                SortType = (int)EnumSortCourse.All;
                                setData(categoryId);
                                break;
                            }
                        case "1":
                            {
                                SortType = (int)EnumSortCourse.Ascending;
                                setData(categoryId);
                                break;
                            }
                        case "2":
                            {
                                SortType = (int)EnumSortCourse.Descending;
                                setData(categoryId);
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                CmmDroidFunction.logErr(ex, "khoahd - Click_ItemSort_FromParentFragment", this.GetType().Name);
#endif
            }
        }

        public bool OnGroupClick(ExpandableListView parent, View clickedView, int groupPosition, long id)
        {
            return true;
        }

        public class BeanExpandExam
        {
            public BeanSurvey Survey { get; set; }
            public List<BeanSurvey> ListSurvey { get; set; }
        }
    }
}
