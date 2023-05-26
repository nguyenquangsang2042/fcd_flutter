using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using Refractored.Controls;
using VNASchedule.Bean;
using VNASchedule.Class;
using VNASchedule.DataProvider;
using VNASchedule.Droid.Code.Class;

namespace VNASchedule.Droid.Code.Adapter
{
    class TestAdap : BaseAdapter<BeanUser>
    {

        private string local_url;
        private Context context;
        private List<BeanUser> lst_contact;
        private MainActivity mainAct;
        string localDocumentFilepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public TestAdap(List<BeanUser> lst_contact, Context context, MainActivity mainAct)
        {
            this.lst_contact = lst_contact;
            this.context = context;
            this.mainAct = mainAct;
        }
        public TestAdap(List<BeanUser> lst_contact, Context context, MainActivity mainAct,string urlFile)
        {
            this.lst_contact = lst_contact;
            this.context = context;
            this.mainAct = mainAct;
            this.local_url = urlFile;
        }


        public override BeanUser this[int position]
        {
            get
            {
                return lst_contact[position];
            }
        }

        public override int Count
        {
            get
            {
                return lst_contact.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater mInflater = LayoutInflater.From(this.context);
            View _rootView = mInflater.Inflate(Resource.Layout.ItemContacts, null);
            TextView tv_mobile = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemContacts_Phone);
            TextView tv_position = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemContacts_Position);
            TextView tv_deparment = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemContacts_Deparment);
            TextView tv_email = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemContacts_Email);
            TextView tv_name = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemContacts_Name);
            TextView tv_VNCode = _rootView.FindViewById<TextView>(Resource.Id.tv_ItemContacts_VNCode);
            CircleImageView imageView = _rootView.FindViewById<CircleImageView>(Resource.Id.img_pilot_contact);
            TextView txt_pilot_id = _rootView.FindViewById<TextView>(Resource.Id.tv_pilot_id);
            TextView txt_pilot_phone = _rootView.FindViewById<TextView>(Resource.Id.tv_pilot_phone);
            BeanUser contact = lst_contact[position];
            Bitmap tam;

            tv_name.Text = contact.FullName;
            tv_position.Text = string.IsNullOrEmpty(contact.PositionName) ? "Position: none" : contact.PositionName;
            tv_deparment.Text = string.IsNullOrEmpty(contact.DepartmentName) ? "Department: none" : contact.DepartmentName;
            tv_mobile.Text = string.IsNullOrEmpty(contact.Mobile) ? "Mobile: none" : contact.Mobile;
            tv_email.Text = string.IsNullOrEmpty(contact.Email) ? "Email: none" : contact.Email;
            txt_pilot_phone.Text = string.IsNullOrEmpty(contact.Mobile) ? "Mobile: none" : contact.Mobile;
            txt_pilot_id.Text = string.IsNullOrEmpty(contact.Code3) ? "Code3: none" : contact.Code3;
            if (!string.IsNullOrEmpty(contact.Avatar))
            {
                string url = CmmVariable.M_Domain + "/" + contact.Avatar + "?ver=" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //RequestOptions options = new RequestOptions().CenterCrop().Placeholder(Resource.Drawable.icon_avatar64).Error(Resource.Drawable.icon_avatar64);
                //CheckFileLocalIsExistAsync(url, imageView);
                //Glide.With(mainAct).Load(url).Apply(new RequestOptions().Override(200, 200).Error(Resource.Drawable.icon_avatar64).InvokeDiskCacheStrategy(Com.Bumptech.Glide.Load.Engine.DiskCacheStrategy.All)).Into(imageView);
                SetAvata(url, imageView, contact);
            }

            if (string.IsNullOrEmpty(contact.Email))
            {
                tv_VNCode.Text = "VN Code: none";
            }
            else
            {
                tv_VNCode.Text = "VN Code: " + contact.Code3;
            }
            //tv_VNCode.Text= string.IsNullOrEmpty(contact.Email) ? "VN Code: none" : contact.Email;
            return _rootView;
        }

        private async void CheckFileLocalIsExistAsync(string filepathURL, CircleImageView image_view)
        {
            try
            {
                //var filenames = filepathURL.Split('/');
                //string filename = filenames[filenames.Length - 2] + "_" + filenames[filenames.Length - 1];
                //filepathURL = CmmVariable.M_Domain + filepathURL;
                //string localfilePath = System.IO.Path.Combine(localDocumentFilepath, filename);

                bool result = false;
                string filename = filepathURL.Substring(filepathURL.LastIndexOf('/') + 1);
                

                string localfilePath = System.IO.Path.Combine(localDocumentFilepath, filename);
                if (!File.Exists(localfilePath))
                {
                    await Task.Run(() =>
                    {
                        ProviderBase provider = new ProviderBase();
                        if (provider.DownloadFile(filepathURL, localfilePath, CmmVariable.M_AuthenticatedHttpClient))
                        {
                            mainAct.RunOnUiThread(() =>
                            {
                                OpenFile(filename, image_view);
                            });
                        }
                        else
                        {
                            //myActivity.RunOnUiThread(() =>
                            //{
                            //    image_view.ima = Image.FromFile("Icons/icon_image_empty.png");
                            //});
                        }
                    });
                }
                else
                {
                    OpenFile(filename, image_view);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("NewsNewAdapter - checkFileLocalIsExist - Err: " + ex.ToString());
            }
        }

        private void OpenFile(string localfilename, CircleImageView image_view)
        {
            try
            {
                byte[] pictByteArray;
                string localfilePath = System.IO.Path.Combine(localDocumentFilepath, localfilename);
                Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(localfilePath));
                image_view.SetImageURI(uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine("NewsNewAdapter - openFile - Err: " + ex.ToString());
            }
        }

        private async void SetAvata(string url, ImageView imageView,BeanUser beanUser)
        {
            try
            {

                ProviderUser p_user = new ProviderUser();
                string extension = beanUser.ID + beanUser.FullName + ".jpg";
                string localPath = System.IO.Path.Combine(local_url, extension);
                //string localPath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, extension);
                string newfilepathurl =  url;
                bool result = false;
                if (!File.Exists(localPath))
                {
                    await Task.Run(() =>
                    {
                        result = p_user.DownloadFile(newfilepathurl, localPath, CmmVariable.M_AuthenticatedHttpClient);
                        if (result)
                        {
                            Bitmap tam =BitmapHelper.LoadAndResizeBitmap(localPath, 50, 50);
                            if(tam!=null )
                            {
                                FileStream stream = new FileStream(localPath, FileMode.Create);
                                tam.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                                stream.Close();
                            }
                            mainAct.RunOnUiThread(() =>
                            {
                                imageView.SetImageBitmap(tam);

                                //Glide.With(mainAct).Load(localPath).Apply(new RequestOptions().Override(200, 200).Error(Resource.Drawable.icon_avatar64).SkipMemoryCache(true)).Into(imageView);
                            });
                        }
                        //else
                        //{
                        //    mainAct.RunOnUiThread(() =>
                        //    {
                        //        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                        //        alert.SetTitle("Vietnam Airlines");
                        //        alert.SetMessage("Download Failed. Try again?");
                        //        alert.SetNegativeButton("Confirm", (senderAlert, args) =>
                        //        {
                        //            SetAvata(url);
                        //            alert.Dispose();
                        //        });
                        //        alert.SetPositiveButton("Cancel", (senderAlert, args) =>
                        //        {
                        //            alert.Dispose();
                        //        });
                        //        Dialog dialog = alert.Create();
                        //        dialog.SetCanceledOnTouchOutside(false);
                        //        dialog.Show();
                        //    });
                        //}
                    });

                }
                else
                {
                    mainAct.RunOnUiThread(() =>
                    {

                    Bitmap bm = BitmapFactory.DecodeFile(localPath);
                        if (bm != null)
                        {
                            imageView.SetImageBitmap(bm);
                        }
                        else
                            imageView.SetImageResource(Resource.Drawable.icon_avatar64);
                        //Glide.With(mainAct).Load(localPath).Apply(new RequestOptions().Override(200, 200).Error(Resource.Drawable.icon_avatar64).SkipMemoryCache(true)).Into(imageView);
                    });

                }

            }
            catch (Exception ex)
            { }
            finally
            {
             
            }
        }
    }

        
      
}