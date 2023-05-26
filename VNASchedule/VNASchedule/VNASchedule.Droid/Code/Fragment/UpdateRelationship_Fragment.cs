using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V13.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Class;

namespace VNASchedule.Droid.Code.Fragment
{
    public class UpdateRelationship_Fragment : Android.App.Fragment, View.IOnTouchListener
    {
        public EditText edt_search;
        private MainActivity mainAct;
        private LayoutInflater _inflater;
        private View _rootView;
        private ImageView _imgBack, _imgBackCer, _imgFrontCer;
        private TextView _txtSave, _txrDateOfBirth;
        private EditText _edtNameENG, _edtNameVN, _edtIDCardNumber, _edtAddress, _edtTelephoneNumber, _edtMobileNumber;
        private Button _btnRemove;
        private Spinner spinnerRela;
        private BeanUserRelationshipDraff rela_change = new BeanUserRelationshipDraff();
        private BeanUserRelationshipDraff rela_info = new BeanUserRelationshipDraff();
        private bool add;
        private bool edit;
        string nameImgae;
        private string idUser;
        private Animation click_animation;
        public InfoUserFragmentV2 infoUserFragment = new InfoUserFragmentV2();
        public List<BeanUserRelationshipDraff> rela_info_add_update = new List<BeanUserRelationshipDraff>();
        public List<BeanUserRelationshipDraff> rela_info_add_update_temp = new List<BeanUserRelationshipDraff>();
        public int rela_info_add_update_temp_id;
        public List<BeanUserRelationshipDraff> rela_info_remove = new List<BeanUserRelationshipDraff>();
        public List<BeanUserRelationshipDraff> lst_relationship = new List<BeanUserRelationshipDraff>();
        public List<BeanRelationshipType> lstRelaType = new List<BeanRelationshipType>();
        private LinearLayout linearType;
        private TextView tvType;
        Bundle args = new Bundle();
        PopupMenu popup;
        private Dialog dig;
        private bool chooseImage = true;
        Java.IO.File newfile;
        string file1;
        string file2;
        private int TakePictureRequestCode = 111, ChoosePictureRequestCode = 222;
        Bitmap myBitmap;
        private int _layoutWigth, _layoutHeight;
        List<KeyValuePair<string, string>> lstImageValue = new List<KeyValuePair<string, string>>();
        KeyValuePair<string, string> lstImageValue_SendToAPIImage_Front = new KeyValuePair<string, string>();
        KeyValuePair<string, string> lstImageValue_SendToAPIImage_Back = new KeyValuePair<string, string>();
        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> lstImageValue_SendToAPIImage = new List<KeyValuePair<string, string>>();
        KeyValuePair<string, string> infor_back_kaypair = new KeyValuePair<string, string>();
        KeyValuePair<string, string> infor_front_kaypair = new KeyValuePair<string, string>();
        BeanUserDraff beanUser;
        private int id_relationtype = 0;
        private ProviderUser p_user = new ProviderUser();
        private bool isOnline;
        private Drawable iconcalendar;
        private bool isEditCardID, isEditPassportImage;
        private int position;
        private LinearLayout _lnRelationship1PDetail;
        private bool checkapprove;
        private bool isEditInfo;
        private bool isEditRela;
        private bool isEditRelaDetail;
        public UpdateRelationship_Fragment(List<BeanUserRelationshipDraff> lst_relationship, bool add, string idUser, bool edit,
             List<BeanUserRelationshipDraff> rela_info_add_update, List<BeanUserRelationshipDraff> rela_info_remove, List<BeanRelationshipType> lstRelaType
            , BeanUserDraff beanUser, bool isOnline, bool isEditCardID, bool isEditPassportImage, bool checkapprove,
             bool isEditInfo, bool isEditRela)
        {
            this.add = add;
            this.idUser = idUser;
            this.edit = edit;
            this.lst_relationship = lst_relationship;
            this.rela_info_remove = rela_info_remove;
            this.lst_relationship = lst_relationship;
            this.lstRelaType = lstRelaType;
            this.beanUser = beanUser;
            this.isOnline = isOnline;
            this.isEditCardID = isEditCardID;
            this.isEditPassportImage = isEditPassportImage;
            this.checkapprove = checkapprove;
            this.isEditInfo = isEditInfo;
            this.isEditRela = isEditRela;


        }
        public UpdateRelationship_Fragment(List<BeanUserRelationshipDraff> lst_relationship, BeanUserRelationshipDraff rela_info, bool edit,
            List<BeanUserRelationshipDraff> rela_info_add_update, List<BeanUserRelationshipDraff> rela_info_remove, List<BeanRelationshipType> lstRelaType, BeanUserDraff beanUser, bool isOnline, bool isEditCardID, bool isEditPassportImage
            , bool checkapprove, bool isEditInfo, bool isEditRela)
        {
            this.rela_info = rela_info;
            this.edit = edit;
            this.rela_info_add_update = rela_info_add_update;
            this.rela_info_remove = rela_info_remove;
            this.lst_relationship = lst_relationship;
            this.lstRelaType = lstRelaType;
            this.beanUser = beanUser;
            this.isOnline = isOnline;
            this.isEditCardID = isEditCardID;
            this.isEditPassportImage = isEditPassportImage;
            this.checkapprove = checkapprove;
            this.isEditRela = isEditRela;
            this.isEditInfo = isEditInfo;

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.Update_RelationShip_Layout, container, false);
            mainAct = (MainActivity)this.Activity;

            _layoutWigth = _rootView.Context.Resources.DisplayMetrics.WidthPixels;
            _layoutHeight = _rootView.Context.Resources.DisplayMetrics.HeightPixels;


            _imgBack = _rootView.FindViewById<ImageView>(Resource.Id.img_InfoUser_Back_Rela);
            _txtSave = _rootView.FindViewById<TextView>(Resource.Id.tv_InfoUser_Edit_Rela);
            _edtNameENG = _rootView.FindViewById<EditText>(Resource.Id.edt_NameENG);
            _edtNameVN = _rootView.FindViewById<EditText>(Resource.Id.edt_NameVN);
            _txrDateOfBirth = _rootView.FindViewById<TextView>(Resource.Id.edt_DateOfBirthRelationShip);

            _txtSave = _rootView.FindViewById<TextView>(Resource.Id.tv_InfoUser_Edit_Rela);
            _edtIDCardNumber = _rootView.FindViewById<EditText>(Resource.Id.edt_IdCardNumber_Relation);
            _edtAddress = _rootView.FindViewById<EditText>(Resource.Id.edt_Address_Relation);
            _edtTelephoneNumber = _rootView.FindViewById<EditText>(Resource.Id.edt_TelephoneNumber_RelationShip);
            _edtMobileNumber = _rootView.FindViewById<EditText>(Resource.Id.edt_MobileNumber_Relationship);

            _imgBackCer = _rootView.FindViewById<ImageView>(Resource.Id.imgBackCerRela);
            _imgFrontCer = _rootView.FindViewById<ImageView>(Resource.Id.imgFrontCerRela);

            _btnRemove = _rootView.FindViewById<Button>(Resource.Id.BtnRemoveRelation);
            _lnRelationship1PDetail = _rootView.FindViewById<LinearLayout>(Resource.Id.lnRelationship1PDetail);
            linearType = _rootView.FindViewById<LinearLayout>(Resource.Id.ln_Type_Relationship);
            tvType = _rootView.FindViewById<TextView>(Resource.Id.tv_Type_relationshio);

           


            iconcalendar = ContextCompat.GetDrawable(_rootView.Context, Resource.Drawable.calendar);
            popup = new PopupMenu(_rootView.Context, tvType);
            popup.Menu.Add(Menu.None, 0, 0, "Choose");
            foreach (BeanRelationshipType i in lstRelaType)
            {
                popup.Menu.Add(Menu.None, i.ID, i.ID, i.TitleEN);
            }
            popup.Menu.GetItem(0).SetChecked(true);
            #region event
            _txtSave.Click += _txtSave_Click;
            _imgBack.Click += _imgBack_Click;
            _btnRemove.Click += _btnRemove_Click;
            _lnRelationship1PDetail.Touch += _lnRelationship1PDetail_Touch;
            _txrDateOfBirth.Click += delegate
            {
                DateTime start = DateTime.Now;
                new DatePickerFragment(start, delegate (DateTime time)
                {
                    _txrDateOfBirth.Text = time.ToString("dd/MM/yyyy");
                })
              .Show(FragmentManager, DatePickerFragment.TAG);
            };
            #endregion
            _txrDateOfBirth.SetCompoundDrawables(null, null, null, null);
            linearType.Click += LinearType_Click;
            _imgBackCer.Click += _imgBackCer_Click; ; _imgFrontCer.Click += _imgFrontCer_Click;
            loadValueImagePath();
            initData();
            _edtNameENG.TextChanged += TextChanged;
            _edtNameVN.TextChanged += TextChanged;
            _txrDateOfBirth.TextChanged += TextChanged;
            _edtAddress.TextChanged += TextChanged;
            _edtIDCardNumber.TextChanged += TextChanged;
            _edtTelephoneNumber.TextChanged += TextChanged;
            _edtMobileNumber.TextChanged += TextChanged;

            return _rootView;
        }
        private async void loadValueImagePath()
        {
            await Task.Run(() =>
            {
                CmmDroidFunction.getpathImage_Exist(rela_info.ImageBack);
                CmmDroidFunction.getpathImage_Exist(rela_info.ImageFront);
            });
        }

        private void TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            isEditRela = true;
            isEditRelaDetail = true;
        }

        private void _lnRelationship1PDetail_Touch(object sender, View.TouchEventArgs e)
        {
            HideKeyboard();
        }

        public void HideKeyboard()
        {
            InputMethodManager imm = InputMethodManager.FromContext(_rootView.Context);

            imm.HideSoftInputFromWindow(_rootView.WindowToken, HideSoftInputFlags.NotAlways);
        }
        #region event set img to cer
        private void _imgBackCer_Click(object sender, EventArgs e)
        {
            View popupImage = _inflater.Inflate(Resource.Layout.ChooseImage, null);
            TextView tv_chonanh = popupImage.FindViewById<TextView>(Resource.Id.tv_ChooseImage_ChooseImage);
            ImageView img_chonanh = popupImage.FindViewById<ImageView>(Resource.Id.img_ChooseImage_ChooseImage);
            TextView tv_chupanh = popupImage.FindViewById<TextView>(Resource.Id.tv_ChooseImage_TakePhoto);
            ImageView img_chupanh = popupImage.FindViewById<ImageView>(Resource.Id.img_ChooseImage_TakePhoto);
            LinearLayout ln_fullScreenImage = popupImage.FindViewById<LinearLayout>(Resource.Id.linearLayout_full_screen_image);
            LinearLayout ln_ChooseImage = popupImage.FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            LinearLayout ln_TakePicture = popupImage.FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            position = 2;
            if (!edit)
            {
                if (rela_info.ImageBack != null && rela_info.ImageBack != "")
                {
                    loadFullScreen("", 2);
                }
            }
            else
            {
                if (edit && rela_info.ImageBack != null && rela_info.ImageBack != "")
                {
                    ln_ChooseImage.Visibility = ViewStates.Visible;
                    ln_TakePicture.Visibility = ViewStates.Visible;
                    ln_fullScreenImage.Visibility = ViewStates.Visible;

                }
                else
                {
                    ln_ChooseImage.Visibility = ViewStates.Visible;
                    ln_TakePicture.Visibility = ViewStates.Visible;
                    ln_fullScreenImage.Visibility = ViewStates.Gone;

                }

                if (popupImage != null)
                {
                    dig = new Dialog(_rootView.Context);
                    Window window = dig.Window;
                    dig.RequestWindowFeature(1);
                    dig.SetCanceledOnTouchOutside(true);
                    dig.SetCancelable(true);
                    window.SetGravity(GravityFlags.Center);

                    DisplayMetrics dm = Resources.DisplayMetrics;
                    ViewGroup.LayoutParams pa = popupImage.LayoutParameters;
                    dig.SetContentView(popupImage);
                    dig.Show();
                    WindowManagerLayoutParams s = window.Attributes;
                    s.Width = dm.WidthPixels;
                    window.Attributes = s;
                    popupImage.SetOnTouchListener(mainAct);
                }
            }
            nameImgae = "imgBackCer.jpg";
            lstImageValue.Add(new KeyValuePair<string, string>(nameImgae, System.Guid.NewGuid().ToString("N")));
            tv_chupanh.Click += (s, v) =>
            {
                dig.Dismiss();
                chooseImage = false;
                RequestRead(nameImgae);

            };
            tv_chonanh.Click += (s, v) =>
            {
                dig.Dismiss();
                chooseImage = true;
                RequestRead(nameImgae);
            };
            ln_fullScreenImage.Click += Ln_fullScreenImage_Click;
        }

        private void _imgFrontCer_Click(object sender, EventArgs e)
        {
            View popupImage = _inflater.Inflate(Resource.Layout.ChooseImage, null);
            TextView tv_chonanh = popupImage.FindViewById<TextView>(Resource.Id.tv_ChooseImage_ChooseImage);
            ImageView img_chonanh = popupImage.FindViewById<ImageView>(Resource.Id.img_ChooseImage_ChooseImage);
            TextView tv_chupanh = popupImage.FindViewById<TextView>(Resource.Id.tv_ChooseImage_TakePhoto);
            ImageView img_chupanh = popupImage.FindViewById<ImageView>(Resource.Id.img_ChooseImage_TakePhoto);
            LinearLayout ln_fullScreenImage = popupImage.FindViewById<LinearLayout>(Resource.Id.linearLayout_full_screen_image);
            LinearLayout ln_ChooseImage = popupImage.FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            LinearLayout ln_TakePicture = popupImage.FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            position = 1;
            if (!edit)
            {
                if (rela_info.ImageFront == null && rela_info.ImageFront == "")
                {
                    loadFullScreen("", 1);
                }
            }
            else
            {
                if (edit && rela_info.ImageFront != null && rela_info.ImageFront != "")
                {
                    ln_ChooseImage.Visibility = ViewStates.Visible;
                    ln_TakePicture.Visibility = ViewStates.Visible;
                    ln_fullScreenImage.Visibility = ViewStates.Visible;

                }
                else
                {
                    ln_ChooseImage.Visibility = ViewStates.Visible;
                    ln_TakePicture.Visibility = ViewStates.Visible;
                    ln_fullScreenImage.Visibility = ViewStates.Gone;

                }

                if (popupImage != null)
                {
                    dig = new Dialog(_rootView.Context);
                    Window window = dig.Window;
                    dig.RequestWindowFeature(1);
                    dig.SetCanceledOnTouchOutside(true);
                    dig.SetCancelable(true);
                    window.SetGravity(GravityFlags.Center);

                    DisplayMetrics dm = Resources.DisplayMetrics;
                    ViewGroup.LayoutParams pa = popupImage.LayoutParameters;
                    dig.SetContentView(popupImage);
                    dig.Show();
                    WindowManagerLayoutParams s = window.Attributes;
                    s.Width = dm.WidthPixels;
                    window.Attributes = s;
                    popupImage.SetOnTouchListener(mainAct);
                }
            }
            nameImgae = "imgFrontCer.jpg";
            lstImageValue.Add(new KeyValuePair<string, string>(nameImgae, System.Guid.NewGuid().ToString("N")));
            tv_chupanh.Click += (s, v) =>
            {
                dig.Dismiss();
                chooseImage = false;
                RequestRead(nameImgae);

            };
            tv_chonanh.Click += (s, v) =>
            {
                dig.Dismiss();
                chooseImage = true;
                RequestRead(nameImgae);
            };
            ln_fullScreenImage.Click += Ln_fullScreenImage_Click;
        }

        private void Ln_fullScreenImage_Click(object sender, EventArgs e)
        {
            loadFullScreen("", position);
        }

        private void loadFullScreen(string path, int position)
        {
            View popupImage_fullImage = _inflater.Inflate(Resource.Layout.layout_viewFullImage, null);
            ImageView img_chonanh = popupImage_fullImage.FindViewById<ImageView>(Resource.Id.img_viewFull);
            ImageButton imgClose = popupImage_fullImage.FindViewById<ImageButton>(Resource.Id.btnCloseImage);

            if (popupImage_fullImage != null)
            {
                dig = new Dialog(_rootView.Context);
                Window window = dig.Window;
                dig.RequestWindowFeature(1);
                dig.SetCanceledOnTouchOutside(true);
                dig.SetCancelable(true);
                window.SetGravity(GravityFlags.Center);

                DisplayMetrics dm = Resources.DisplayMetrics;
                ViewGroup.LayoutParams pa = popupImage_fullImage.LayoutParameters;
                dig.SetContentView(popupImage_fullImage);

                WindowManagerLayoutParams s = window.Attributes;
                s.Width = dm.WidthPixels;
                s.Height = dm.HeightPixels - 400;
                window.Attributes = s;
                popupImage_fullImage.SetOnTouchListener(mainAct);
            }

            Bitmap bm;
            int resource;
            if (position == 1)
            {

                bm = ((BitmapDrawable)_imgFrontCer.Drawable).Bitmap;
                img_chonanh.SetImageBitmap(bm);
                dig.Show();



            }
            if (position == 2)
            {

                bm = ((BitmapDrawable)_imgBackCer.Drawable).Bitmap;
                img_chonanh.SetImageBitmap(bm);
                dig.Show();

            }

            imgClose.Click += (s, e) =>
            {
                dig.Dismiss();
            };

        }
        private void RequestRead(string _nameImage)
        {
            if (ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.WriteExternalStorage) != Android.Content.PM.Permission.Granted ||
                ContextCompat.CheckSelfPermission(mainAct, Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(mainAct,
                            new string[] { Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, MainActivity.MY_PERMISSIONS_REQUEST_CAMERA_EXTERNAL_STORAGE);
            }
            else
            {
                nameImgae = _nameImage;
                ReadFile(nameImgae);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == MainActivity.MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE)
            {
                if (grantResults[0] == Android.Content.PM.Permission.Granted)
                {
                    ReadFile(nameImgae);
                }
                else
                {

                }
                return;
            }
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void ReadFile(string _nameImage)
        {
            try
            {
                var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "CameraApp";
                file1 = dir + System.Guid.NewGuid().ToString("N") + ".jpg";
                if (!chooseImage)
                {
                    Intent intent = new Intent(MediaStore.ActionImageCapture);
                    newfile = new Java.IO.File(file1);
                    try
                    {
                        newfile.CreateNewFile();
                    }
                    catch (System.IO.IOException e)
                    {

                    }
                    //intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(photoBackside._file));
                    intent.PutExtra(MediaStore.ExtraOutput, FileProvider.GetUriForFile(mainAct, "com.Vuthao.VNASchedule", newfile));
                    StartActivityForResult(intent, TakePictureRequestCode);
                }
                else
                {
                    Intent intent = new Intent(Intent.ActionGetContent);
                    intent.SetFlags(ActivityFlags.ClearTop);
                    intent.SetType("image/*");
                    StartActivityForResult(intent, ChoosePictureRequestCode);
                }
            }
            catch (Exception ex)
            { }

        }
        public override void OnActivityResult(int requestCode, [GeneratedEnum] Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            try
            {
                if (requestCode == TakePictureRequestCode && resultCode == Android.App.Result.Ok)
                {
                    isEditRela = true;
                    isEditRelaDetail = true;
                    Android.Net.Uri contentUri = null;
                    contentUri = FileProvider.GetUriForFile(mainAct, "com.Vuthao.VNASchedule", newfile);
                    string c = contentUri.Path;
                    var b = nameImgae;

                    //myBitmap = BitmapHelper.LoadAndResizeBitmap(file1, 250, 250);
                    //myBitmap = BitmapHelper.LoadAndResizeBitmap(file1, 250, 250);
                    myBitmap = BitmapHelper.LoadAndResizeBitmap(file1, _layoutWigth, _layoutHeight);
                    try
                    {
                        FileStream stream = new FileStream(file1, FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
                        myBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                        stream.Close();
                    }
                    catch (Exception e)
                    {

                    }
                    if (nameImgae == "imgBackCer.jpg")
                    {
                        //img_info_imgBackCer = new KeyValuePair<string, string>("AVATAR.jpg", file1);
                        //myBitmap= modifyOrientation(myBitmap, file1);
                        infor_back_kaypair = new KeyValuePair<string, string>(nameImgae, file1);
                        lstImageValue_SendToAPIImage_Back = new KeyValuePair<string, string>(nameImgae, file1); //=> send ten hinh
                        _imgBackCer.SetImageBitmap(myBitmap);
                    }
                    else
                    {
                        infor_front_kaypair = new KeyValuePair<string, string>(nameImgae, file1);
                        lstImageValue_SendToAPIImage_Front = new KeyValuePair<string, string>(nameImgae, file1);
                        _imgFrontCer.SetImageBitmap(myBitmap);

                    }


                    data = null;

                }
                if (requestCode == ChoosePictureRequestCode && resultCode == Android.App.Result.Ok)
                {
                    isEditRela = true;
                    isEditRelaDetail = true;
                    if (data != null)
                    {
                        var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "CameraApp";

                        string localpath2 = "";
                        string name_GUIDfile = "";
                        Android.Net.Uri uri = data.Data;
                        string localPath = "";
                        string filename = "";
                        string nametoAddKeypair = "";
                        if (nameImgae == "imgBackCer.jpg")
                        {
                            result = lstImageValue.Where(x => x.Key.Equals(nameImgae)).ToList();
                            name_GUIDfile = result[0].Value + ".jpg";
                        }
                        else
                        {
                            result = lstImageValue.Where(x => x.Key.Equals(nameImgae)).ToList();
                            name_GUIDfile = result[0].Value + ".jpg";
                        }

                        if (uri.ToString().Contains("primary"))
                        {
                            //orgin_path = AndroidCoreOS.Environment.ExternalStorageDirectory.Path + "/" + uri.Path.Split(':')[1];
                            filename = System.IO.Path.GetFileName(uri.Path);
                            if (filename.Contains(":"))
                            {
                                filename = uri.Path.Split(':')[1];
                            }
                            localPath = GetActualPathFromFile(uri);
                        }
                        else if (!uri.ToString().Contains("primary") && uri.ToString().Contains("externalstorage"))//thẻ nhớ máy
                        {
                            localPath = "/storage/extSdCard/" + uri.Path.Split(':')[1];
                            filename = System.IO.Path.GetFileName(uri.Path);
                            if (filename.Contains(":"))
                            {
                                filename = uri.Path.Split(':')[1];
                            }
                        }
                        else if (uri.ToString().ToLower().Contains("fileprovider"))
                        {
                            localPath = GetActualPathFromFile(uri);
                        }
                        else
                        {
                            localPath = GetActualPathFromFile(uri);

                        }
                        /*List<string> lstdir = localPath.Split("/").ToList();
                        lstdir.RemoveAt(lstdir.Count()-1);
                        foreach(string i in lstdir)
                        {
                            localpath2 += "/" + i;
                        }
                        localpath2 = localpath2 +"/"+name_GUIDfile;*/
                        localpath2 = dir + name_GUIDfile;
                        List<string> listDir = localpath2.Split("/").ToList();
                        nametoAddKeypair = listDir[listDir.Count() - 1];
                        if (!File.Exists(localpath2))
                        {
                            System.IO.File.Copy(localPath, localpath2);
                        }



                        try
                        {
                            myBitmap = BitmapHelper.LoadAndResizeBitmap(localpath2, 1000, 1500);
                            FileStream stream = new FileStream(localpath2, FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
                            myBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                            stream.Close();
                        }
                        catch (Exception e)
                        {

                        }

                        if (nameImgae == "imgBackCer.jpg")
                        {
                            //img_info_imgBackCer = new KeyValuePair<string, string>("AVATAR.jpg", file1);
                            //myBitmap= modifyOrientation(myBitmap, file1);
                            infor_back_kaypair = new KeyValuePair<string, string>(nameImgae, nametoAddKeypair);
                            lstImageValue_SendToAPIImage_Back = new KeyValuePair<string, string>(nameImgae, localpath2);
                            //lstImageValue_SendToAPI.Add(new KeyValuePair<string, string>(nameImgae, file2)); //=> send ten hinh
                            _imgBackCer.SetImageBitmap(myBitmap);
                        }
                        else
                        {
                            infor_front_kaypair = new KeyValuePair<string, string>(nameImgae, nametoAddKeypair);
                            lstImageValue_SendToAPIImage_Front = new KeyValuePair<string, string>(nameImgae, localpath2);

                            _imgFrontCer.SetImageBitmap(myBitmap);

                        }


                        data = null;
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private string GetActualPathFromFile(Android.Net.Uri uri)
        {
            bool isKitKat = Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat;

            if (isKitKat && DocumentsContract.IsDocumentUri(mainAct, uri))
            {
                // ExternalStorageProvider
                if (isExternalStorageDocument(uri))
                {
                    string docId = DocumentsContract.GetDocumentId(uri);

                    char[] chars = { ':' };
                    string[] split = docId.Split(chars);
                    string type = split[0];

                    if ("primary".Equals(type, StringComparison.OrdinalIgnoreCase))
                    {
                        return Android.OS.Environment.ExternalStorageDirectory + "/" + split[1];
                    }
                }
                // DownloadsProvider
                else if (isDownloadsDocument(uri))
                {
                    string id = DocumentsContract.GetDocumentId(uri);

                    Android.Net.Uri contentUri = ContentUris.WithAppendedId(
                                    Android.Net.Uri.Parse("content://downloads/public_downloads"), long.Parse(id));

                    //System.Diagnostics.Debug.WriteLine(contentUri.ToString());

                    return getDataColumn(mainAct, contentUri, null, null);
                }
                // MediaProvider
                else if (isMediaDocument(uri))
                {
                    string docId = DocumentsContract.GetDocumentId(uri);

                    char[] chars = { ':' };
                    string[] split = docId.Split(chars);

                    string type = split[0];

                    Android.Net.Uri contentUri = null;
                    if ("image".Equals(type))
                    {
                        contentUri = MediaStore.Images.Media.ExternalContentUri;
                    }
                    else if ("video".Equals(type))
                    {
                        contentUri = MediaStore.Video.Media.ExternalContentUri;
                    }
                    else if ("audio".Equals(type))
                    {
                        contentUri = MediaStore.Audio.Media.ExternalContentUri;
                    }

                    string selection = "_id=?";
                    string[] selectionArgs = new string[]
                    {
                split[1]
                    };

                    return getDataColumn(mainAct, contentUri, selection, selectionArgs);
                }
            }
            // MediaStore (and general)
            else if ("content".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
            {

                // Return the remote address
                if (isGooglePhotosUri(uri))
                    return uri.LastPathSegment;

                return getDataColumn(mainAct, uri, null, null);
            }
            // File
            else if ("file".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return uri.Path;
            }

            return null;
        }

        public static string getDataColumn(Context context, Android.Net.Uri uri, string selection, string[] selectionArgs)
        {
            ICursor cursor = null;
            string column = "_data";
            string[] projection = { column };
            try
            {
                cursor = context.ContentResolver.Query(uri, projection, selection, selectionArgs, null);
                if (cursor != null && cursor.MoveToFirst())
                {
                    int index = cursor.GetColumnIndexOrThrow(column);
                    return cursor.GetString(index);
                }
            }
            finally
            {
                if (cursor != null)
                    cursor.Close();
            }
            return null;
        }
        public static bool isExternalStorageDocument(Android.Net.Uri uri)
        {
            return "com.android.externalstorage.documents".Equals(uri.Authority);
        }

        //Whether the Uri authority is DownloadsProvider.
        public static bool isDownloadsDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.downloads.documents".Equals(uri.Authority);
        }

        //Whether the Uri authority is MediaProvider.
        public static bool isMediaDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.media.documents".Equals(uri.Authority);
        }

        //Whether the Uri authority is Google Photos.
        public static bool isGooglePhotosUri(Android.Net.Uri uri)
        {
            return "com.google.android.apps.photos.content".Equals(uri.Authority);
        }



        #endregion

        private void LinearType_Click(object sender, EventArgs e)
        {

            popup.MenuItemClick += Popup_MenuItemClick;
            popup.Show();
        }

        private void Popup_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            tvType.Text = e.Item.TitleFormatted.ToString();
            if (e.Item.TitleFormatted.ToString() == lstRelaType[0].TitleEN)
            {
                id_relationtype = 1;
            }
            else if (e.Item.TitleFormatted.ToString() == lstRelaType[1].TitleEN)
            {
                id_relationtype = 2;
            }
            else if (e.Item.TitleFormatted.ToString() == lstRelaType[2].TitleEN)
            {
                id_relationtype = 3;
            }
            else if (e.Item.TitleFormatted.ToString() == lstRelaType[3].TitleEN)
            {
                id_relationtype = 4;
            }
            else if (e.Item.TitleFormatted.ToString() == lstRelaType[4].TitleEN)
            {
                id_relationtype = 5;
            }
            else if (e.Item.TitleFormatted.ToString() == lstRelaType[5].TitleEN)
            {
                id_relationtype = 6;
            }
            else
            {
                id_relationtype = 0;
            }



        }

        private void _btnRemove_Click(object sender, EventArgs e)
        {
            try
            {

                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(_rootView.Context);
                alert.SetTitle("Notifications");
                alert.SetMessage("Are you sure you want to remove?");
                alert.SetCancelable(false);

                alert.SetNegativeButton("Ok", (senderAlert, args1) =>
                {
                    isEditRela = true;

                    List<string> arrayDateOfBirth;
                    if (!string.IsNullOrEmpty(_txrDateOfBirth.Text))
                    {
                        arrayDateOfBirth = _txrDateOfBirth.Text.Split("/").ToList();

                    }
                    else
                    {
                        arrayDateOfBirth = new List<string> { "00", "00", "0000" };
                    }
                    #region add to 1 relationship
                    if (add)
                    {
                        rela_change = new BeanUserRelationshipDraff
                        {
                            KeyName = _edtNameENG.Text,
                            FullName = _edtNameVN.Text,
                            RelationshipTypeID = id_relationtype,
                            DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                            MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[1]),
                            YearOfBirth = Convert.ToInt32(arrayDateOfBirth[2]),
                            IdentityNum = _edtIDCardNumber.Text,
                            Address = _edtAddress.Text,
                            Mobile = _edtMobileNumber.Text,
                            Telephone = _edtTelephoneNumber.Text,
                            Action = 1,
                            ImageFront = lstImageValue_SendToAPIImage_Front.Value,
                            ImageBack = lstImageValue_SendToAPIImage_Back.Value,
                            DateUpdate = DateTime.Now
                        };
                        /* DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                             MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                             YearOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),*/
                    }
                    else
                    {
                        if (rela_info.IdUpdate == 0 || rela_info.IdUpdate == null)
                        {
                            rela_change = new BeanUserRelationshipDraff
                            {
                                IdUpdate = rela_info.ID,
                                KeyName = _edtNameENG.Text,
                                FullName = _edtNameVN.Text,
                                RelationshipTypeID = id_relationtype,
                                DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                                MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[1]),
                                YearOfBirth = Convert.ToInt32(arrayDateOfBirth[2]),
                                IdentityNum = _edtIDCardNumber.Text,
                                Address = _edtAddress.Text,
                                Mobile = _edtMobileNumber.Text,
                                Telephone = _edtTelephoneNumber.Text,
                                Action = -2,
                                ImageFront = lstImageValue_SendToAPIImage_Front.Value,
                                ImageBack = lstImageValue_SendToAPIImage_Back.Value,
                                DateUpdate = DateTime.Now
                            };
                        }
                        else
                        {
                            rela_change = new BeanUserRelationshipDraff
                            {
                                IdUpdate = rela_info.IdUpdate,
                                KeyName = _edtNameENG.Text,
                                FullName = _edtNameVN.Text,
                                RelationshipTypeID = id_relationtype,
                                DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                                MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[1]),
                                YearOfBirth = Convert.ToInt32(arrayDateOfBirth[2]),
                                IdentityNum = _edtIDCardNumber.Text,
                                Address = _edtAddress.Text,
                                Mobile = _edtMobileNumber.Text,
                                Telephone = _edtTelephoneNumber.Text,
                                Action = -2,
                                ImageFront = lstImageValue_SendToAPIImage_Front.Value,
                                ImageBack = lstImageValue_SendToAPIImage_Back.Value,
                                DateUpdate = DateTime.Now
                            };
                        }
                    }
                    #endregion


                    rela_info_remove.Add(rela_change);

                    infoUserFragment = new InfoUserFragmentV2(rela_info_add_update, rela_info_remove, lst_relationship, lstRelaType, beanUser, isOnline, isEditCardID, isEditPassportImage, checkapprove, isEditInfo, isEditRela);
                    args.PutBoolean("isBack", true);
                    args.PutBoolean("isEdit", edit);
                    infoUserFragment.Arguments = args;
                    mainAct.BackFragment();
                    mainAct.ShowFragment(mainAct.FragmentManager, infoUserFragment, "updateRelationship_Fragment");

                    alert.Dispose();
                });
                alert.SetPositiveButton("Cancel", (senderAlert, args) =>
                {
                    alert.Dispose();
                });
                Dialog dialog = alert.Create();
                dialog.Show();
            }
            catch (Exception)
            {


            }

        }

        private async void _imgBack_Click(object sender, EventArgs e)
        {
            infoUserFragment = new InfoUserFragmentV2(rela_info_add_update, rela_info_remove, lst_relationship, lstRelaType, beanUser, isOnline, isEditCardID, isEditPassportImage, checkapprove, isEditInfo, isEditRela);

            mainAct.BackFragment();
            /*if (!edit || (!isEditRela && edit) || !isEditRelaDetail)
            {



                infoUserFragment = new InfoUserFragment(rela_info_add_update, rela_info_remove, lst_relationship, lstRelaType, beanUser, isOnline, isEditCardID, isEditPassportImage, checkapprove, isEditInfo, isEditRela);
                args.PutBoolean("isBack", true);
                args.PutBoolean("isEdit", edit);
                infoUserFragment.Arguments = args;

                mainAct.BackFragment();
                if (string.IsNullOrEmpty(rela_info.ImageBack)&&string.IsNullOrEmpty(rela_info.ImageFront))
                {
                    mainAct.ShowFragment(mainAct.FragmentManager, infoUserFragment, "updateRelationship_Fragment");

                }
                else
                {
                    await Task.Run(() =>
                    {
                        CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);

                        mainAct.RunOnUiThread(() =>
                        {
                            mainAct.ShowFragment(mainAct.FragmentManager, infoUserFragment, "updateRelationship_Fragment");

                            CmmDroidFunction.HideProcessingDialog();
                        });
                    });
                }
            }
            else
            {
                try
                {

                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(_rootView.Context);
                    alert.SetTitle("Notifications");
                    alert.SetMessage("The changes you have made will not be saved, are you sure you want to do this ?");
                    alert.SetCancelable(false);

                    alert.SetNegativeButton("Ok", async (senderAlert, args1) =>
                    {

                        infoUserFragment = new InfoUserFragment(rela_info_add_update, rela_info_remove, lst_relationship, lstRelaType, beanUser, isOnline, isEditCardID, isEditPassportImage, checkapprove, isEditInfo, isEditRela);
                        args.PutBoolean("isBack", true);
                        args.PutBoolean("isEdit", edit);
                        infoUserFragment.Arguments = args;
                        mainAct.BackFragment();
                        if (string.IsNullOrEmpty(rela_info.ImageBack)&&string.IsNullOrEmpty(rela_info.ImageFront))
                        {
                            mainAct.ShowFragment(mainAct.FragmentManager, infoUserFragment, "updateRelationship_Fragment");

                        }
                        else
                        {
                            await Task.Run(() =>
                            {
                                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);

                                mainAct.RunOnUiThread(() =>
                                {
                                    mainAct.ShowFragment(mainAct.FragmentManager, infoUserFragment, "updateRelationship_Fragment");

                                    CmmDroidFunction.HideProcessingDialog();
                                });
                            });
                        }
                        alert.Dispose();
                    });
                    alert.SetPositiveButton("Cancel", (senderAlert, args) =>
                    {
                        alert.Dispose();
                    });
                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                catch (Exception)
                {


                }
            }
*/

        }

        private async void _txtSave_Click(object sender, EventArgs e)
        {
            HideKeyboard();
            if (_edtNameENG.Text == "" || _edtNameENG.Text == null)
            {

                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(_rootView.Context);
                alert.SetTitle("Notifications");
                alert.SetMessage("Your english name is empty, please type something in Name(eng) feild !");
                alert.SetCancelable(false);
                alert.SetPositiveButton("OK", (senderAlert, args) =>
                {
                    alert.Dispose();
                });
                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {

                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                List<string> arrayDateOfBirth;
                if (!string.IsNullOrEmpty(_txrDateOfBirth.Text))
                {
                    arrayDateOfBirth = _txrDateOfBirth.Text.Split("/").ToList();
                }
                else
                {
                    arrayDateOfBirth = new List<string> { "00", "00", "0000" };
                }
                #region add to 1 relationship
                if (add || rela_info.GuidIDAdd != null)
                {
                    string GuidIDAddString = "";
                    if (rela_info.GuidIDAdd == null)
                    {
                        GuidIDAddString = System.Guid.NewGuid().ToString("N");
                    }
                    else
                    {
                        GuidIDAddString = rela_info.GuidIDAdd;
                    }
                    rela_change = new BeanUserRelationshipDraff
                    {
                        GuidIDAdd = GuidIDAddString,
                        KeyName = _edtNameENG.Text,
                        FullName = _edtNameVN.Text,
                        RelationshipTypeID = id_relationtype,
                        DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                        MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[1]),
                        YearOfBirth = Convert.ToInt32(arrayDateOfBirth[2]),
                        IdentityNum = _edtIDCardNumber.Text,
                        Address = _edtAddress.Text,
                        Mobile = _edtMobileNumber.Text,
                        Telephone = _edtTelephoneNumber.Text,
                        Action = 1,
                        ImageFront = lstImageValue_SendToAPIImage_Front.Value,
                        ImageBack = lstImageValue_SendToAPIImage_Back.Value,
                        DateUpdate = DateTime.Now


                    };
                    /* DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                         MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                         YearOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),*/
                }
                else
                {
                    if (rela_info.IdUpdate == 0 || rela_info.IdUpdate == null)
                    {
                        rela_change = new BeanUserRelationshipDraff
                        {

                            IdUpdate = rela_info.ID,
                            KeyName = _edtNameENG.Text,
                            FullName = _edtNameVN.Text,
                            RelationshipTypeID = id_relationtype,
                            DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                            MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[1]),
                            YearOfBirth = Convert.ToInt32(arrayDateOfBirth[2]),
                            IdentityNum = _edtIDCardNumber.Text,
                            Address = _edtAddress.Text,
                            Mobile = _edtMobileNumber.Text,
                            Telephone = _edtTelephoneNumber.Text,
                            Action = 2,
                            ImageFront = lstImageValue_SendToAPIImage_Front.Value,
                            ImageBack = lstImageValue_SendToAPIImage_Back.Value,
                            DateUpdate = DateTime.Now
                        };
                    }
                    else
                    {
                        rela_change = new BeanUserRelationshipDraff
                        {
                            IdUpdate = rela_info.IdUpdate,
                            KeyName = _edtNameENG.Text,
                            FullName = _edtNameVN.Text,
                            RelationshipTypeID = id_relationtype,
                            DayOfBirth = Convert.ToInt32(arrayDateOfBirth[0]),
                            MonthOfBirth = Convert.ToInt32(arrayDateOfBirth[1]),
                            YearOfBirth = Convert.ToInt32(arrayDateOfBirth[2]),
                            IdentityNum = _edtIDCardNumber.Text,
                            Address = _edtAddress.Text,
                            Mobile = _edtMobileNumber.Text,
                            Telephone = _edtTelephoneNumber.Text,
                            Action = 2,
                            ImageFront = lstImageValue_SendToAPIImage_Front.Value,
                            ImageBack = lstImageValue_SendToAPIImage_Back.Value,
                            DateUpdate = DateTime.Now
                        };
                    }
                }
                if (string.IsNullOrEmpty(rela_change.ImageFront))
                {
                    if (!string.IsNullOrEmpty(rela_info.ImageFront))
                        rela_change.ImageFront = rela_info.ImageFront;
                }
                if (string.IsNullOrEmpty(rela_change.ImageBack))
                {
                    if (!string.IsNullOrEmpty(rela_info.ImageBack))
                        rela_change.ImageBack = rela_info.ImageBack;
                }
                #endregion
                ProviderUserRelationship pro_user_rela = new ProviderUserRelationship();
                lstImageValue_SendToAPIImage.Clear();
                lstImageValue_SendToAPIImage.Add(lstImageValue_SendToAPIImage_Front);
                lstImageValue_SendToAPIImage.Add(lstImageValue_SendToAPIImage_Back);
                if (string.IsNullOrEmpty(lstImageValue_SendToAPIImage_Front.Value) && string.IsNullOrEmpty(lstImageValue_SendToAPIImage_Back.Value))
                {

                }
                else
                {
                    
                    if (string.IsNullOrEmpty(lstImageValue_SendToAPIImage[0].Value))
                        lstImageValue_SendToAPIImage.Remove(lstImageValue_SendToAPIImage[0]);
                    else if (string.IsNullOrEmpty(lstImageValue_SendToAPIImage[1].Value))
                        lstImageValue_SendToAPIImage.Remove(lstImageValue_SendToAPIImage[1]);
                    //var resultAvatar = pro_user_rela.UploadCer(lstImageValue_SendToAPIImage, null);
                    string ApiServerUrl = "/API/UploadUserImageProfile.ashx?func=uploadCertificate";
                    await Task.Run(() =>
                    {
                        CmmFunction.UploadImage(lstImageValue_SendToAPIImage, ApiServerUrl);
                    });
                }


                if ((rela_change.ID == null || rela_change.ID == 0) && (rela_change.IdUpdate == 0 || rela_change.IdUpdate == null))
                {
                    if (rela_info_add_update.Count == 0)
                    {
                        rela_info_add_update.Add(rela_change);
                    }
                    else
                    {
                        BeanUserRelationshipDraff tempAdd = new BeanUserRelationshipDraff();
                        foreach (BeanUserRelationshipDraff i in rela_info_add_update)
                        {
                            if (i.GuidIDAdd == rela_change.GuidIDAdd)
                            {
                                i.KeyName = rela_change.KeyName;
                                i.FullName = rela_change.KeyName;
                                i.RelationshipTypeID = rela_change.RelationshipTypeID;
                                i.DayOfBirth = rela_change.DayOfBirth;
                                i.MonthOfBirth = rela_change.MonthOfBirth;
                                i.YearOfBirth = rela_change.YearOfBirth;
                                i.IdentityNum = rela_change.IdentityNum;
                                i.Address = rela_change.Address;
                                i.Mobile = rela_change.Mobile;
                                i.Telephone = rela_change.Telephone;
                                i.Action = rela_change.Action;
                                i.ImageFront = rela_change.ImageFront;
                                i.ImageBack = rela_change.ImageBack;
                                i.DateUpdate = rela_change.DateUpdate;

                            }
                            else
                            {
                                tempAdd = rela_change;
                            }
                        }
                        if (tempAdd != null)
                        {
                            rela_info_add_update.Add(tempAdd);
                        }
                    }

                }
                else
                {
                    if (rela_info_add_update.Count == 0)
                    {
                        rela_info_add_update.Add(rela_change);
                    }
                    else
                    {
                        for (int u = 0; u < rela_info_add_update.Count; u++)
                        {
                            if (rela_info_add_update[u].IdUpdate == rela_change.IdUpdate)
                            {
                                rela_info_add_update_temp_id = u;
                            }

                        }
                        if (rela_info_add_update_temp_id != null)
                        {
                            rela_info_add_update.RemoveAt(rela_info_add_update_temp_id);
                            rela_info_add_update.Add(rela_change);

                        }



                    }


                }
                if (string.IsNullOrEmpty(infor_back_kaypair.Value) && string.IsNullOrEmpty(infor_front_kaypair.Value))
                {
                    rela_change.ImageBack = CmmDroidFunction.getpathImage_Exist(rela_info.ImageBack);
                    rela_change.ImageFront = CmmDroidFunction.getpathImage_Exist(rela_info.ImageFront);
                }

                if (rela_info.RelationshipTypeID != rela_change.RelationshipTypeID)
                    isEditRela = true;


                infoUserFragment = new InfoUserFragmentV2(rela_info_add_update, rela_info_remove, lst_relationship, lstRelaType, beanUser, isOnline, isEditCardID, isEditPassportImage, checkapprove, isEditInfo, isEditRela);
                args.PutBoolean("isBack", true);
                args.PutBoolean("isEdit", edit);
                infoUserFragment.Arguments = args;
                mainAct.BackFragment();
                if (string.IsNullOrEmpty(rela_change.ImageBack) && string.IsNullOrEmpty(rela_change.ImageFront))
                {
                    mainAct.ShowFragment(mainAct.FragmentManager, infoUserFragment, "updateRelationship_Fragment");
                }
                else
                {
                    await Task.Run(() =>
                    {

                        mainAct.RunOnUiThread(() =>
                        {
                            mainAct.ShowFragment(mainAct.FragmentManager, infoUserFragment, "updateRelationship_Fragment");
                            CmmDroidFunction.HideProcessingDialog();
                        });
                    });
                }

            }

        }




        private string getpathImage_Exist(string path)
        {
            if (path != null && path != "" && !path.StartsWith("/storage/emulated/"))
            {
                string url = CmmVariable.M_Domain + "/" + path;
                var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "CameraApp";
                List<string> dir1 = dir.Split("/").ToList();
                dir1.RemoveAt(dir1.Count() - 1);
                var path1 = path.Split("/").Last();
                dir = "";
                foreach (string i in dir1)
                {
                    dir += i + "/";
                }
                string filepathMobile = dir + path1;


                if (p_user.DownloadFile(url, filepathMobile, CmmVariable.M_AuthenticatedHttpClient))
                {

                    path = filepathMobile;
                }
                else
                {
                    path = "";
                }

            }

            return path;
        }

        private void initData()
        {
            if (edit)
            {
                _edtNameENG.Enabled = true;
                _edtNameVN.Enabled = true;
                //  spinnerRela.Enabled = true;
                _txrDateOfBirth.Enabled = true;
                _edtIDCardNumber.Enabled = true;
                _edtAddress.Enabled = true;
                _edtTelephoneNumber.Enabled = true;
                _edtMobileNumber.Enabled = true;

                _btnRemove.Enabled = true;


            }
            else
            {
                _txtSave.Visibility = ViewStates.Invisible;
                linearType.Enabled = false;
                _btnRemove.Enabled = false;
                _edtNameENG.Enabled = false;
                _edtNameVN.Enabled = false;
                // spinnerRela.Enabled = false;
                _txrDateOfBirth.Enabled = false;
                _edtIDCardNumber.Enabled = false;
                _edtAddress.Enabled = false;
                _edtTelephoneNumber.Enabled = false;
                _edtMobileNumber.Enabled = false;

            }
            if (add && edit)
            {
                _edtNameENG.Enabled = true;
                _edtNameVN.Enabled = true;
                // spinnerRela.Enabled = true;
                _txrDateOfBirth.Enabled = true;
                _edtIDCardNumber.Enabled = true;
                _edtAddress.Enabled = true;
                _edtTelephoneNumber.Enabled = true;
                _edtMobileNumber.Enabled = true;

                _btnRemove.Enabled = true;

                _btnRemove.Visibility = ViewStates.Invisible;
                _edtNameENG.Text = "";
                _edtNameVN.Text = "";
                tvType.Text = "Choose";
                _txrDateOfBirth.Hint = "dd/MM/yyyy";
                _edtIDCardNumber.Text = "";
                _edtTelephoneNumber.Text = "";
                _edtMobileNumber.Text = "";
                _edtAddress.Text = "";
            }
            else if (edit && !add)
            {
                _edtNameENG.Enabled = true;
                _edtNameVN.Enabled = true;
                //spinnerRela.Enabled = true;
                _txrDateOfBirth.Enabled = true;
                _edtIDCardNumber.Enabled = true;
                _edtAddress.Enabled = true;
                _edtTelephoneNumber.Enabled = true;
                _edtMobileNumber.Enabled = true;

                _btnRemove.Enabled = true;
                _txrDateOfBirth.SetCompoundDrawablesWithIntrinsicBounds(null, null, iconcalendar, null);
                if (string.IsNullOrEmpty(rela_info.KeyName))
                {
                    _edtNameENG.Text = "";
                }
                else
                {
                    _edtNameENG.Text = rela_info.KeyName;
                }


                if (string.IsNullOrEmpty(rela_info.FullName))
                {
                    _edtNameVN.Text = "";
                }
                else
                {
                    _edtNameVN.Text = rela_info.FullName;
                }


                /*if (string.IsNullOrEmpty(Convert.ToString(rela_info.RelationshipTypeID)))
                {
                    spinnerRela.SetSelection(0);
                }
                else
                {
                    if (Convert.ToInt32(rela_info.RelationshipTypeID) < 8)
                    {
                        if (Convert.ToInt32(rela_info.RelationshipTypeID) == 2 || Convert.ToInt32(rela_info.RelationshipTypeID) == 2)
                        {
                            spinnerRela.SetSelection(2);

                        }
                        else
                        {
                            spinnerRela.SetSelection(Convert.ToInt32(rela_info.RelationshipTypeID));
                        }
                    }
                }*/
                // set thong tin dropdown
                if (rela_info.RelationshipTypeID == 1) { tvType.Text = lstRelaType[0].TitleEN; id_relationtype = 1; }
                else if (rela_info.RelationshipTypeID == 2) { tvType.Text = lstRelaType[1].TitleEN; id_relationtype = 2; }
                else if (rela_info.RelationshipTypeID == 3) { tvType.Text = lstRelaType[2].TitleEN; id_relationtype = 3; }
                else if (rela_info.RelationshipTypeID == 4) { tvType.Text = lstRelaType[3].TitleEN; id_relationtype = 4; }
                else if (rela_info.RelationshipTypeID == 5) { tvType.Text = lstRelaType[4].TitleEN; id_relationtype = 5; }
                else if (rela_info.RelationshipTypeID == 6) { tvType.Text = lstRelaType[5].TitleEN; id_relationtype = 6; }
                else { tvType.Text = "Choose"; id_relationtype = 0; }

                if ((string.IsNullOrEmpty(rela_info.YearOfBirth.ToString()) && string.IsNullOrEmpty(rela_info.MonthOfBirth.ToString()) && string.IsNullOrEmpty(rela_info.DayOfBirth.ToString()))
                    || ((rela_info.YearOfBirth == 00 || rela_info.MonthOfBirth == 0 || rela_info.DayOfBirth == 0))
                    || (string.IsNullOrEmpty(rela_info.YearOfBirth.ToString()) || string.IsNullOrEmpty(rela_info.MonthOfBirth.ToString()) || string.IsNullOrEmpty(rela_info.DayOfBirth.ToString()))
                    )
                {
                    _txrDateOfBirth.Text = "";
                    _txrDateOfBirth.Hint = "dd/MM/yyyy";


                }
                else
                {
                    _txrDateOfBirth.Text = "";
                    DateTime datetime = new DateTime((int)rela_info.YearOfBirth, (int)rela_info.MonthOfBirth, (int)rela_info.DayOfBirth);
                    if (!string.IsNullOrEmpty(rela_info.DayOfBirth.ToString()))
                    {
                        _txrDateOfBirth.Text = datetime.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        _txrDateOfBirth.Hint = "dd";
                    }
                    if (!string.IsNullOrEmpty(rela_info.MonthOfBirth.ToString()))
                    {
                        _txrDateOfBirth.Text = datetime.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        _txrDateOfBirth.Hint += "/MM";
                    }
                    if (!string.IsNullOrEmpty(rela_info.YearOfBirth.ToString()))
                    {
                        _txrDateOfBirth.Text = datetime.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        _txrDateOfBirth.Hint += "/yyyy";
                    }

                }



                if (string.IsNullOrEmpty(rela_info.IdentityNum))
                {
                    _edtIDCardNumber.Text = "";
                }
                else
                {
                    _edtIDCardNumber.Text = rela_info.IdentityNum;
                }

                if (string.IsNullOrEmpty(rela_info.Telephone))
                {
                    _edtTelephoneNumber.Text = "";
                }
                else
                {
                    _edtTelephoneNumber.Text = rela_info.Telephone;
                }

                if (string.IsNullOrEmpty(rela_info.Mobile))
                {
                    _edtMobileNumber.Text = "";
                }
                else
                {
                    _edtMobileNumber.Text = rela_info.Mobile;
                }
                if (string.IsNullOrEmpty(rela_info.Address))
                {
                    _edtAddress.Text = "";
                }
                else
                {
                    _edtAddress.Text = rela_info.Address;
                }

                //set image if not null
                string path = "";
                Bitmap bm;
                if (!string.IsNullOrEmpty(rela_info.ImageFront))
                {
                    try
                    {
                        /* path = getpathImage_Exist(rela_info.ImageFront);

                         //myBitmap =BitmapHelper.LoadAndResizeBitmap(path, _layoutWigth, _layoutHeight);
                         FileStream stream = new FileStream(path, FileMode.OpenOrCreate,
                                        FileAccess.ReadWrite,
                                        FileShare.None);
                         myBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);*/
                        _imgFrontCer.SetImageBitmap(BitmapFactory.DecodeFile(CmmDroidFunction.getpathImage_Exist(rela_info.ImageFront)));


                    }
                    catch (Exception e)
                    {

                    }


                }
                if (!string.IsNullOrEmpty(rela_info.ImageBack))
                {
                    try
                    {
                        /*path = getpathImage_Exist(rela_info.ImageBack);
                        //myBitmap  = BitmapHelper.LoadAndResizeBitmap(path, _layoutWigth, _layoutHeight);
                        FileStream stream = new FileStream(path, FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
                        myBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                        _imgBackCer.SetImageBitmap(myBitmap);*/

                        _imgBackCer.SetImageBitmap(BitmapFactory.DecodeFile(CmmDroidFunction.getpathImage_Exist(rela_info.ImageBack)));
                    }
                    catch (Exception e)
                    {

                    }

                }

            }
            else if (!edit && !add)
            {
                _edtNameENG.Enabled = false;
                _edtNameVN.Enabled = false;
                //spinnerRela.Enabled = false;
                _txrDateOfBirth.Enabled = false;
                _edtIDCardNumber.Enabled = false;
                _edtAddress.Enabled = false;
                _edtTelephoneNumber.Enabled = false;
                _edtMobileNumber.Enabled = false;

                _btnRemove.Enabled = false;
                //_txtSave.Visibility = ViewStates.Gone;
                _btnRemove.Visibility = ViewStates.Invisible;


                if (string.IsNullOrEmpty(rela_info.KeyName))
                {
                    _edtNameENG.Text = "";
                }
                else
                {
                    _edtNameENG.Text = rela_info.KeyName;
                }


                if (string.IsNullOrEmpty(rela_info.FullName))
                {
                    _edtNameVN.Text = "";
                }
                else
                {
                    _edtNameVN.Text = rela_info.FullName;
                }


                /*if (string.IsNullOrEmpty(Convert.ToString(rela_info.RelationshipTypeID)))
                {
                    spinnerRela.SetSelection(0);
                }
                else
                {
                    if (Convert.ToInt32(rela_info.RelationshipTypeID) < 8)
                    {
                        if (Convert.ToInt32(rela_info.RelationshipTypeID) == 2 || Convert.ToInt32(rela_info.RelationshipTypeID) == 2)
                        {
                            spinnerRela.SetSelection(2);

                        }
                        else
                        {
                            spinnerRela.SetSelection(Convert.ToInt32(rela_info.RelationshipTypeID));
                        }
                    }
                }*/
                if (rela_info.RelationshipTypeID == 1) { tvType.Text = lstRelaType[0].TitleEN; id_relationtype = 1; }
                else if (rela_info.RelationshipTypeID == 2) { tvType.Text = lstRelaType[1].TitleEN; id_relationtype = 2; }
                else if (rela_info.RelationshipTypeID == 3) { tvType.Text = lstRelaType[2].TitleEN; id_relationtype = 3; }
                else if (rela_info.RelationshipTypeID == 4) { tvType.Text = lstRelaType[3].TitleEN; id_relationtype = 4; }
                else if (rela_info.RelationshipTypeID == 5) { tvType.Text = lstRelaType[4].TitleEN; id_relationtype = 5; }
                else if (rela_info.RelationshipTypeID == 6) { tvType.Text = lstRelaType[5].TitleEN; id_relationtype = 6; }
                else { tvType.Text = "Choose"; id_relationtype = 0; }


                if ((string.IsNullOrEmpty(rela_info.YearOfBirth.ToString()) && string.IsNullOrEmpty(rela_info.MonthOfBirth.ToString()) && string.IsNullOrEmpty(rela_info.DayOfBirth.ToString()))
                    || ((rela_info.YearOfBirth == 00 || rela_info.MonthOfBirth == 0 || rela_info.DayOfBirth == 0))
                    || (string.IsNullOrEmpty(rela_info.YearOfBirth.ToString()) || string.IsNullOrEmpty(rela_info.MonthOfBirth.ToString()) || string.IsNullOrEmpty(rela_info.DayOfBirth.ToString()))
                    )
                {
                    _txrDateOfBirth.Text = "";
                    _txrDateOfBirth.Hint = "dd/MM/yyyy";


                }

                else
                {
                    _txrDateOfBirth.Text = "";

                    DateTime datetime = new DateTime((int)rela_info.YearOfBirth, (int)rela_info.MonthOfBirth, (int)rela_info.DayOfBirth);
                    if (!string.IsNullOrEmpty(rela_info.DayOfBirth.ToString()))
                    {
                        _txrDateOfBirth.Text = datetime.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        _txrDateOfBirth.Hint = "dd";
                    }
                    if (!string.IsNullOrEmpty(rela_info.MonthOfBirth.ToString()))
                    {
                        _txrDateOfBirth.Text = datetime.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        _txrDateOfBirth.Hint += "/MM";
                    }
                    if (!string.IsNullOrEmpty(rela_info.YearOfBirth.ToString()))
                    {
                        _txrDateOfBirth.Text = datetime.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        _txrDateOfBirth.Hint += "/yyyy";
                    }

                }



                if (string.IsNullOrEmpty(rela_info.IdentityNum))
                {
                    _edtIDCardNumber.Text = "";
                }
                else
                {
                    _edtIDCardNumber.Text = rela_info.IdentityNum;
                }

                if (string.IsNullOrEmpty(rela_info.Telephone))
                {
                    _edtTelephoneNumber.Text = "";
                }
                else
                {
                    _edtTelephoneNumber.Text = rela_info.Telephone;
                }

                if (string.IsNullOrEmpty(rela_info.Mobile))
                {
                    _edtMobileNumber.Text = "";
                }
                else
                {
                    _edtMobileNumber.Text = rela_info.Mobile;
                }
                if (string.IsNullOrEmpty(rela_info.Address))
                {
                    _edtAddress.Text = "";
                }
                else
                {
                    _edtAddress.Text = rela_info.Address;
                }


            }


        }
        public void hideKeyboard()
        {
            InputMethodManager inputMethodManager = (InputMethodManager)mainAct.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(edt_search.WindowToken, 0);
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            hideKeyboard();
            return false;
        }
    }
}