using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using VNASchedule.Bean;
using VNASchedule.Droid.Code.Class;
using System.Threading.Tasks;
using VNASchedule.DataProvider;
using VNASchedule.Class;
using System.IO;

namespace VNASchedule.Droid.Code.Fragment
{
    //
    public class LoginOTPGoogleFragment : Android.App.Fragment
    {
        private MainActivity mainAct;
        private View _rootView;
        private LayoutInflater _inflater;
        private TextView tv_otp1, tv_otp2, tv_otp3, tv_otp4, tv_otp5, tv_otp6, tv_clear, tv_resend, tv_resend1;
        private TextView tv_key1, tv_key2, tv_key3, tv_key4, tv_key5, tv_key6, tv_key7, tv_key8, tv_key9, tv_key0;
        BeanUser reg_user;
        private LinearLayout lv_resend;
        public LoginOTPGoogleFragment() { }
        public LoginOTPGoogleFragment(BeanUser reg_user)
        {
            this.reg_user = reg_user;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            mainAct = (MainActivity)this.Activity;
            _inflater = inflater;
            _rootView = inflater.Inflate(Resource.Layout.LoginOTP, null);
            tv_clear = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_Clear);
            tv_key0 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY0);
            tv_key1 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY1);
            tv_key2 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY2);
            tv_key3 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY3);
            tv_key4 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY4);
            tv_key5 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY5);
            tv_key6 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY6);
            tv_key7 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY7);
            tv_key8 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY8);
            tv_key9 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_KEY9);
            tv_otp1 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_OTP1);
            tv_otp2 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_OTP2);
            tv_otp3 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_OTP3);
            tv_otp4 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_OTP4);
            tv_otp5 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_OTP5);
            tv_otp6 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_OTP6);
            tv_resend = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_Resend);
            tv_resend1 = _rootView.FindViewById<TextView>(Resource.Id.tv_LoginOTP_Resend1);
            lv_resend = _rootView.FindViewById<LinearLayout>(Resource.Id.linear_LoginOTP_Resend);
            lv_resend.Visibility = ViewStates.Gone;

            tv_key0.Click += KEY0;
            tv_key1.Click += KEY1;
            tv_key2.Click += KEY2;
            tv_key3.Click += KEY3;
            tv_key4.Click += KEY4;
            tv_key5.Click += KEY5;
            tv_key6.Click += KEY6;
            tv_key7.Click += KEY7;
            tv_key8.Click += KEY8;
            tv_key9.Click += KEY9;
            tv_clear.Click += Clear;
            _rootView.SetOnTouchListener(mainAct);
            return _rootView;
        }
        private void Clear(object sender, EventArgs e)
        {
            try
            {
                tv_otp1.Text = string.Empty; tv_otp2.Text = string.Empty; tv_otp3.Text = string.Empty; tv_otp4.Text = string.Empty; tv_otp5.Text = string.Empty; tv_otp6.Text = string.Empty;
            }
            catch (Exception ex)
            {

            }
        }

        private void KEY9(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "9";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "9";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "9";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "9";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "9";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "9";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private async void Next()
        {
            try
            {
                string googlePincode = tv_otp1.Text + tv_otp2.Text + tv_otp3.Text + tv_otp4.Text + tv_otp5.Text + tv_otp6.Text;
                
                CmmDroidFunction.showProcessingDialog("Loading...", this.Activity, false);
                await Task.Run(() =>
                {
                    ProviderUser p_user = new ProviderUser();
                    ProviderBase p_base = new ProviderBase();
                    reg_user.GoogleAuthCode = googlePincode;
                    BeanUser res = p_user.OtpConfirm(reg_user);
                    p_base.UpdateAllMasterData(false, CmmVariable.SysConfig.DataLimitDay, true); //get all    
                    
                    if (res != null)
                    {
                        CmmVariable.SysConfig.UserId = res.ID;
                        CmmVariable.SysConfig.Title = res.FullName;
                        CmmVariable.SysConfig.DisplayName = res.FullName;
                        CmmVariable.SysConfig.Email = res.Email;
                        CmmVariable.SysConfig.Email2 = res.Email2;
                        CmmVariable.SysConfig.LoginName = res.LoginName;
                        CmmVariable.SysConfig.LoginPassword = CmmFunction.GetMd5Hash(reg_user.VerifyCode + "#");
                        CmmVariable.SysConfig.Birthday = res.Birthday;
                        CmmVariable.SysConfig.Mobile = res.Mobile;
                        CmmVariable.SysConfig.Address = res.Address;
                        if (res.Position.HasValue)
                        {
                            CmmVariable.SysConfig.Position = res.Position.Value;
                        }
                        CmmVariable.SysConfig.PositionName = res.PositionName;
                        if (res.Department.HasValue)
                        {
                            CmmVariable.SysConfig.Department = res.Department.Value;
                        }
                        CmmVariable.SysConfig.DepartmentName = res.DepartmentName;
                        CmmVariable.SysConfig.IdentityNum = res.IdentityNum;
                        CmmVariable.SysConfig.IdentityIssueDate = res.IdentityIssueDate;
                        CmmVariable.SysConfig.IdentityIssuePlace = res.IdentityIssuePlace;
                        CmmVariable.SysConfig.TicketLimitNumber = res.TicketLimitNumber;
                        CmmVariable.SysConfig.Code = res.Code;
                        CmmVariable.SysConfig.Code3 = res.Code3;
                        CmmVariable.SysConfig.UseAppDate = res.UseAppDate;

                        CmmVariable.SysConfig.WorkingPattern = res.WorkingPattern;
                        CmmVariable.SysConfig.Nationality = res.Nationality;

                        CmmVariable.SysConfig.HRCode = res.HRCode;
                        CmmVariable.SysConfig.IsBanLanhDao = res.IsBanLanhDao;

                        if (res.UseAppDate.HasValue)
                        {
                            CmmVariable.SysConfig.UseAppDate = res.UseAppDate.Value;
                        }
                        if (res.UserType.HasValue)
                        {
                            CmmVariable.SysConfig.UserType = res.UserType.Value;
                        }
                        else
                        {
                            CmmVariable.SysConfig.UserType = 0;
                        }
                        CmmFunction.WriteSettingToFile();
                        string url = CmmVariable.M_Domain + "/Data/Users/" + CmmVariable.SysConfig.UserId + "/avatar.jpg";
                        if (!File.Exists(CmmVariable.M_AvatarCus))
                        {
                            bool result = p_user.DownloadFile(url, CmmVariable.M_AvatarCus, CmmVariable.M_AuthenticatedHttpClient);
                        }
                        p_base.UpdateAllDynamicData(false, CmmVariable.SysConfig.DataLimitDay, true);// get all
                        mainAct.RunOnUiThread(() =>
                        {
                            PilotMainFragment PilotMain = new PilotMainFragment(true);
                            mainAct.ShowFragment(FragmentManager, PilotMain, "PilotMain");
                        });
                    }
                    else
                    {
                        mainAct.RunOnUiThread(() =>
                        {
                            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
                            alert.SetTitle("Vietnam Airlines");
                            alert.SetMessage("Authent fail, please try again or contact to admin for more information, thank you!");
                            alert.SetNegativeButton("Close", (senderAlert, args) =>
                            {
                                alert.Dispose();
                            });
                            Dialog dialog = alert.Create();
                            dialog.SetCanceledOnTouchOutside(false);
                            dialog.Show();
                            tv_otp1.Text = string.Empty; tv_otp2.Text = string.Empty; tv_otp3.Text = string.Empty; tv_otp4.Text = string.Empty; tv_otp5.Text = string.Empty; tv_otp6.Text = string.Empty;
                        });
                    }
                });
            }
            catch (Exception ex)
            { }
            finally
            {
                CmmDroidFunction.HideProcessingDialog();
            }
        }

        private void KEY8(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "8";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "8";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "8";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "8";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "8";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "8";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY7(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "7";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "7";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "7";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "7";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "7";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "7";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY6(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "6";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "6";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "6";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "6";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "6";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "6";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY5(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "5";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "5";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "5";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "5";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "5";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "5";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY4(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "4";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "4";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "4";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "4";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "4";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "4";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY3(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "3";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "3";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "3";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "3";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "3";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "3";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY2(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "2";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "2";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "2";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "2";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "2";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "2";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY0(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "0";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "0";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "0";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "0";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "0";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "0";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }

        private void KEY1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tv_otp1.Text))
                {
                    tv_otp1.Text = "1";
                }
                else if (string.IsNullOrEmpty(tv_otp2.Text))
                {
                    tv_otp2.Text = "1";
                }
                else if (string.IsNullOrEmpty(tv_otp3.Text))
                {
                    tv_otp3.Text = "1";
                }
                else if (string.IsNullOrEmpty(tv_otp4.Text))
                {
                    tv_otp4.Text = "1";
                }
                else if (string.IsNullOrEmpty(tv_otp5.Text))
                {
                    tv_otp5.Text = "1";
                }
                else if (string.IsNullOrEmpty(tv_otp6.Text))
                {
                    tv_otp6.Text = "1";
                }
                if (!string.IsNullOrEmpty(tv_otp1.Text) && !string.IsNullOrEmpty(tv_otp2.Text) && !string.IsNullOrEmpty(tv_otp3.Text) && !string.IsNullOrEmpty(tv_otp4.Text) && !string.IsNullOrEmpty(tv_otp5.Text) && !string.IsNullOrEmpty(tv_otp6.Text))
                {
                    Next();
                }
            }
            catch (Exception ex)
            { }
        }
    }
}