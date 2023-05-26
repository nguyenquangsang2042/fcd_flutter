using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.Droid.Code.Fragment;

namespace VNASchedule.Droid.Code.Class
{
    public class MoreMenuProperties
    {
        public Android.App.Fragment Fragment { get; set; }
        public RelativeLayout RelativeLayoutExtent { get; set; }
        public int[] HideControlIds { get; set; }
    }
    public class MoreMenu
    {
        MoreMenuProperties props;
        Dialog dialog;
        List<KeyValuePair<string, int>> lstAllMenu;
        Dictionary<string, int> lstButtonMenu;


        bool isAlowPopupMenuNavigation = true;
        public MoreMenu(MoreMenuProperties props)
        {
            this.props = props;
            this.props.RelativeLayoutExtent.Click += PopupNavigationMenu;
        }
        private void SetAllMenu()
        {
            lstAllMenu = new List<KeyValuePair<string, int>>();
            lstAllMenu.Add(new KeyValuePair<string, int>("Licence", Resource.Drawable.icon_lisence30));
            lstAllMenu.Add(new KeyValuePair<string, int>("Ticket request", Resource.Drawable.icon_ticket_booking30));
            lstAllMenu.Add(new KeyValuePair<string, int>("Training", Resource.Drawable.icon_training));
            lstAllMenu.Add(new KeyValuePair<string, int>("Payroll", Resource.Drawable.icon_payroll));
            lstAllMenu.Add(new KeyValuePair<string, int>("Library", Resource.Drawable.icon_library30));
            lstAllMenu.Add(new KeyValuePair<string, int>("Contacts", Resource.Drawable.icon_user2));
            lstAllMenu.Add(new KeyValuePair<string, int>("FAQs", Resource.Drawable.icon_FAQs));
            lstAllMenu.Add(new KeyValuePair<string, int>("Report", Resource.Drawable.icon_report));
            lstAllMenu.Add(new KeyValuePair<string, int>("Application", Resource.Drawable.ic_menu_application));
        }
        private void GetHomeMenu()
        {
            try
            {
                List<BeanMenuHome> menuHomes = SQLiteHelper.GetList<BeanMenuHome>("SELECT * FROM BeanMenuHome Where [Status] = 1 ORDER BY [Index]").ListData;
                //using (SQLiteConnection con = new SQLiteConnection(CmmVariable.M_DataPath))
                //{
                //    menuHomes = con.Query<BeanMenuHome>("SELECT * FROM BeanMenuHome Where [Status] = 1 ORDER BY [Index]");
                //}

                if (menuHomes != null && menuHomes.Count > 0)
                {
                    List<KeyValuePair<string, int>> lstMenuHome = menuHomes.Select(x =>
                            new KeyValuePair<string, int>(x.Title,
                                lstAllMenu.FirstOrDefault(m => m.Key == x.Key).Value
                            )).ToList();

                    //List<KeyValuePair<string, int>> lstMenuHome = lstAllMenu.Where(x=> menuHomes.Any(m=>m.Title.Trim().ToLower() == x.Key.ToLower())).ToList();

                    SetListHomeMenu(lstMenuHome);
                }
                else
                    SetListHomeMenu(lstAllMenu);
            }
            catch (Exception ex)
            {

            }
        }
        private void SetListHomeMenu(List<KeyValuePair<string, int>> menus)
        {
            lstButtonMenu = menus.ToDictionary(i => i.Key, i => i.Value);
        }


        private void PopupNavigationMenu(object sender, EventArgs e)
        {
            if (isAlowPopupMenuNavigation)
            {
                isAlowPopupMenuNavigation = false;
                View view = props.Fragment.LayoutInflater.Inflate(Resource.Layout.ExtendCustomDialog, null);

                #region hide control id
                if (props.HideControlIds != null && props.HideControlIds.Length > 0)
                {
                    foreach (int controlId in props.HideControlIds)
                    {
                        View control = view.FindViewById(controlId);
                        if (control != null)
                            control.Visibility = ViewStates.Gone;
                    }
                }
                #endregion

                TextView tv_cancel = view.FindViewById<TextView>(Resource.Id.txt_cancel);
                tv_cancel.Click += DismissMenuDialog;


                SetAllMenu();
                GetHomeMenu();






                LinearLayout ln_request = view.FindViewById<LinearLayout>(Resource.Id.ln_request);
                LinearLayout ln_traning = view.FindViewById<LinearLayout>(Resource.Id.ln_training);
                LinearLayout ln_license = view.FindViewById<LinearLayout>(Resource.Id.ln_license);
                LinearLayout ln_library = view.FindViewById<LinearLayout>(Resource.Id.ln_library);
                LinearLayout ln_payroll = view.FindViewById<LinearLayout>(Resource.Id.ln_payroll);
                LinearLayout ln_contact = view.FindViewById<LinearLayout>(Resource.Id.ln_contacts);
                LinearLayout ln_faq = view.FindViewById<LinearLayout>(Resource.Id.ln_faqs);
                LinearLayout ln_report = view.FindViewById<LinearLayout>(Resource.Id.ln_report);
                LinearLayout ln_application = view.FindViewById<LinearLayout>(Resource.Id.ln_application);
                //Textview 
                TextView tv_request = view.FindViewById<TextView>(Resource.Id.txt_request_ticket);
                TextView tv_traning = view.FindViewById<TextView>(Resource.Id.txt_training);
                TextView tv_license = view.FindViewById<TextView>(Resource.Id.txt_license);
                TextView tv_library = view.FindViewById<TextView>(Resource.Id.txt_library);
                TextView tv_payroll = view.FindViewById<TextView>(Resource.Id.txt_payroll);
                TextView tv_contact = view.FindViewById<TextView>(Resource.Id.txt_contact);
                TextView tv_faq = view.FindViewById<TextView>(Resource.Id.txt_faqs);
                TextView tv_report = view.FindViewById<TextView>(Resource.Id.txt_report);
                TextView tv_application = view.FindViewById<TextView>(Resource.Id.txt_application);

                ln_request.Visibility = ViewStates.Gone;
                ln_traning.Visibility = ViewStates.Gone;
                ln_license.Visibility = ViewStates.Gone;
                ln_library.Visibility = ViewStates.Gone;
                ln_payroll.Visibility = ViewStates.Gone;
                ln_contact.Visibility = ViewStates.Gone;
                ln_faq.Visibility = ViewStates.Gone;
                ln_report.Visibility = ViewStates.Gone;
                ln_application.Visibility = ViewStates.Gone;

                List<BeanMenuHome> menuHomes = SQLiteHelper.GetList<BeanMenuHome>("SELECT * FROM BeanMenuHome Where [Status] = 1 ORDER BY [Index]").ListData;
                foreach (var item in menuHomes)
                {
                    switch (item.Key)
                    {
                        case "Licence":
                            tv_license.Text = item.Title;
                            ln_license.Visibility = ViewStates.Visible;
                            break;
                        case "Ticket request":
                            tv_request.Text = item.Title;
                            ln_request.Visibility = ViewStates.Visible;
                            break;
                        case "Training":
                            tv_traning.Text = item.Title;
                            ln_traning.Visibility = ViewStates.Visible;
                            break;
                        case "Payroll":
                            tv_payroll.Text = item.Title;
                            ln_payroll.Visibility = ViewStates.Visible;
                            break;
                        case "Library":
                            tv_library.Text = item.Title;
                            ln_library.Visibility = ViewStates.Visible;
                            break;
                        case "Contacts":
                            tv_contact.Text = item.Title;
                            ln_contact.Visibility = ViewStates.Visible;
                            break;
                        case "FAQs":
                            tv_faq.Text = item.Title;
                            ln_faq.Visibility = ViewStates.Visible;
                            break;
                        case "Report":
                            tv_report.Text = item.Title;
                            ln_report.Visibility = ViewStates.Visible;
                            break;
                        case "Application":
                            tv_application.Text = item.Title;
                            ln_application.Visibility = ViewStates.Visible;
                            break;

                    }
                }
                tv_report.Click += ReportClick;
                tv_traning.Click += TraningClick;
                tv_license.Click += LicenseClick;
                tv_library.Click += LibraryClick;
                tv_payroll.Click += PayrollClick;
                tv_contact.Click += ContactClick;
                tv_request.Click += RequestClick;
                tv_faq.Click += FaqsClick;
                tv_application.Click += ApplicationClick;

                //dialog = new Dialog(props.Fragment.Context, Resource.Style.Dialog);
                dialog = new Dialog(props.Fragment.Context, Resource.Style.DialogAlphaAnimation);
                Window window = dialog.Window;
                dialog.RequestWindowFeature(1);

                window.SetGravity(GravityFlags.Bottom);
                Android.Util.DisplayMetrics dm = props.Fragment.Resources.DisplayMetrics;
                ViewGroup.LayoutParams pa = view.LayoutParameters;
                dialog.SetCancelable(false);
                dialog.SetContentView(view);
                dialog.Show();
                WindowManagerLayoutParams s = window.Attributes;

                s.Width = dm.WidthPixels - 70;
                s.Y = 50;
                window.Attributes = s;
            }
        }

        private void ShowFragment(Android.App.Fragment fragment, string tag, int type = 0)
        {
            dialog.Dismiss();

            props.Fragment.BackToHome();

            MainActivity.INSTANCE?.ShowFragmentAnim(props.Fragment.FragmentManager, fragment, tag, type);
        }

        private void ApplicationClick(object sender, EventArgs e)
        {
            ShowFragment(new ApplicationFragment(), "Application");
        }

        private void ReportClick(object sender, EventArgs e)
        {
            ShowFragment(new ReportsFragment(), "Faqs");
        }

        private void TraningClick(object sender, EventArgs e)
        {
            ShowFragment(new TrainingFragment(), "Training");
        }

        private void LicenseClick(object sender, EventArgs e)
        {
            ShowFragment(new LicenceFragment(), "Licence");
        }

        private void LibraryClick(object sender, EventArgs e)
        {
            ShowFragment(new LibraryFragment(), "Library");
        }

        private void PayrollClick(object sender, EventArgs e)
        {
            ShowFragment(new PayrollFragment(), "Payroll");
        }

        private void ContactClick(object sender, EventArgs e)
        {
            ShowFragment(new ContactsFragment(), "Contacts");
        }

        private void FaqsClick(object sender, EventArgs e)
        {
            ShowFragment(new SupportFragment(), "Support");
        }

        private void RequestClick(object sender, EventArgs e)
        {
            TextView tv = (TextView)sender;
            var supportCountTry = CmmFunction.GetAppSetting("TICKET_APP_SUPPORT_COUNTRY");
            string[] supportCountTryArr = supportCountTry.Split(",");
            var registUrl = CmmFunction.GetAppSetting("TICKET_REIGIST_URL");

            try
            {
                var keyNews = CmmFunction.GetAppSetting("NotifyRequestTicketAlert");
                foreach (string x in supportCountTryArr)
                {

                    if (CmmVariable.SysConfig.Nationality.ToLower().Contains(x.ToLower()))
                    {
                        ShowFragment(new TicketRequestFragment(), "TicketRequest");
                        return;
                    }
                    else
                    {
                        var uri = Android.Net.Uri.Parse(registUrl);
                        var intent = new Intent(Intent.ActionView, uri);
                        props.Fragment.StartActivity(intent);
                        tv.Focusable = false;
                        tv.Enabled = false;

                        Handler h = new Handler();
                        Action myAction = () =>
                        {
                            tv.Focusable = true;
                            tv.Enabled = true;
                        };
                        h.PostDelayed(myAction, 1500);
                        return;
                    }
                }

            }
            catch (Exception ex)
            { }
        }

        private void DismissMenuDialog(object sender, EventArgs e)
        {
            isAlowPopupMenuNavigation = true;
            dialog.Dismiss();
        }
    }
}