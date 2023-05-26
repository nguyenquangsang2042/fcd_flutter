using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content.PM;
using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Content;
using SQLite;
//using ZXing;
using System.Web;
using VNASchedule.Class;
using VNASchedule.Bean;
using Refractored.Controls;

namespace VNASchedule.Droid.Code.Fragment
{
    class CheckQRCodeFragment : Android.App.Fragment, ISurfaceHolderCallback, Detector.IProcessor
    {
        private MainActivity _mainAct;
        private View _rootView;
        private ImageView _imgBack;
        private CircleImageView _imgQRBack;
        private SurfaceView _surfaceViewFull;
        const int RequestCameraPermisionID = 1001;
        private BarcodeDetector barcodeDetector;
        private CameraSource cameraSource;
        private bool CheckQRCode = true;
        private int width = 640, height = 480;
        private Android.Support.V7.App.AlertDialog.Builder Alert = null;
        private bool _flagDialog = false;
        public override void OnResume()
        {
            base.OnResume();
            CheckQRCode = true;
        }
        public CheckQRCodeFragment() { }
        [Obsolete]
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _mainAct = (MainActivity)this.Activity;
            if (_rootView == null)
            {
                _rootView = inflater.Inflate(Resource.Layout.CheckQRCode, null);
                //_imgBack = _rootView.FindViewById<ImageView>(Resource.Id.img_CheckQRCode_Back);
                _imgQRBack = _rootView.FindViewById<CircleImageView>(Resource.Id.img_QRCode_Logo_Back);
                _surfaceViewFull = _rootView.FindViewById<SurfaceView>(Resource.Id.cameraViewFull);
                SetSizeCamera();
                barcodeDetector = new BarcodeDetector.Builder(_mainAct).SetBarcodeFormats(BarcodeFormat.QrCode).Build();
                cameraSource = new CameraSource.Builder(_mainAct, barcodeDetector)
                    .SetAutoFocusEnabled(true)
                    .SetRequestedPreviewSize(width, height)
                    .SetRequestedFps(30.0f)
                    .Build();
                _surfaceViewFull.Holder.AddCallback(this);
                barcodeDetector.SetProcessor(this);
                _imgQRBack.Click += ClickBack;
                RequestRead();
            }
            return _rootView;
        }
        [Obsolete]
        private void SetSizeCamera()
        {
            try
            {
                int numCameras = Android.Hardware.Camera.NumberOfCameras;
                for (int i = 0; i < numCameras; i++)
                {
                    Android.Hardware.Camera.CameraInfo cameraInfo = new Android.Hardware.Camera.CameraInfo();
                    Android.Hardware.Camera.GetCameraInfo(i, cameraInfo);
                    if (cameraInfo.Facing == Android.Hardware.Camera.CameraInfo.CameraFacingBack)
                    {
                        Android.Hardware.Camera camera = Android.Hardware.Camera.Open(i);
                        Android.Hardware.Camera.Parameters cameraParams = camera.GetParameters();
                        List<Android.Hardware.Camera.Size> sizes = cameraParams.SupportedPreviewSizes.ToList();
                        width = sizes[0].Width;
                        height = sizes[0].Height;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("Author: khoahd - SetSizeCamera - Error: " + ex);
#endif
                width = 640;
                height = 480;
            }
        }
        private void ClickBack(object sender, EventArgs e)
        {
            _mainAct.BackFragment();
        }
        private void RequestRead()
        {
            try
            {
                //if (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.Camera) != Permission.Granted)
                //{
                ActivityCompat.RequestPermissions(_mainAct, new[] { Manifest.Permission.Camera }, RequestCameraPermisionID);
                //}
                //else
                //{
                //    cameraSource.Start(_surfaceViewFull.Holder);
                //}
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("CheckQRCodeFragment - RequestRead - Error : " + ex.Message + ex.StackTrace);
#endif
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermisionID:
                    {
                        if (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.Camera) != Permission.Granted)
                        {
                            ActivityCompat.RequestPermissions(_mainAct, new[] { Manifest.Permission.Camera }, RequestCameraPermisionID);
                            return;
                        }
                        try
                        {
                            cameraSource.Start(_surfaceViewFull.Holder);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    break;
            }
        }
        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
        }
        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (ActivityCompat.CheckSelfPermission(_rootView.Context, Manifest.Permission.Camera) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(_mainAct, new[]
                {
                    Manifest.Permission.Camera
                }, RequestCameraPermisionID);
                return;
            }
            try
            {
                cameraSource.Start(_surfaceViewFull.Holder);
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("CheckQRCodeFragment - SurfaceCreated - Error : " + ex);
#endif
            }
        }
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            cameraSource.Stop();
        }
        public void ReceiveDetections(Detector.Detections detections)
        {
            try
            {
                SparseArray qrcodes = detections.DetectedItems;
                if (qrcodes.Size() != 0 && CheckQRCode) // new quet trung QR code
                {
                    CheckQRCode = false;
                    string email = ((Barcode)qrcodes.ValueAt(0)).RawValue;
                    if (!string.IsNullOrEmpty(email))
                    {
                        ShowPilotContactByQRCode(email.Split(':')[email.Split(':').Length - 1].Trim());
                    }
                    else
                    {
                        _imgQRBack.Post(() =>
                        {
                            cameraSource.Stop();
                            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(_mainAct);
                            alert.SetTitle("Thông báo");
                            alert.SetMessage("QRCode không hợp lệ. Vui lòng thử lại.");
                            alert.SetPositiveButton("Đóng",
                                (senderAlert, args) =>
                                {
                                    CheckQRCode = true;
                                    cameraSource.Start(_surfaceViewFull.Holder);
                                    alert.Dispose();
                                });
                            Dialog dialog = alert.Create();
                            dialog.SetCanceledOnTouchOutside(false);
                            dialog.Show();
                        });
                    }
                }
            }
            catch (Exception e)
            {
                _imgQRBack.Post(() =>
                {
                    cameraSource.Stop();
                    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(_mainAct);
                    alert.SetTitle("Thông báo");
                    alert.SetMessage("QRCode không hợp lệ. Vui lòng thử lại.");
                    alert.SetPositiveButton("Đóng",
                        (senderAlert, args) =>
                        {
                            CheckQRCode = true;
                            cameraSource.Start(_surfaceViewFull.Holder);
                            alert.Dispose();
                        });
                    Dialog dialog = alert.Create();
                    dialog.SetCanceledOnTouchOutside(false);
                    dialog.Show();
                });
#if DEBUG
                Console.WriteLine("CheckQRCodeFragment - ReceiveDetections - Error : " + e);
#endif
            }
        }

        private async void ShowPilotContactByQRCode(string pilotEmail)
        {
            if (!string.IsNullOrEmpty(pilotEmail))
            {
                var conn = new SQLite.SQLiteConnection(CmmVariable.M_DataPath);
                string query = "SELECT * FROM BeanUser WHERE Email = ? ";
                var lst_user = conn.Query<BeanUser>(query, pilotEmail);
                conn.Close();
                if (lst_user != null && lst_user.Count > 0)
                {
                    DetailContact fragment = new DetailContact(lst_user[0], _rootView);
                    //_mainAct.AddFragment(_mainAct.FragmentManager, fragment, "DetailFragment", 1);
                    _mainAct.ShowFragment(_mainAct.FragmentManager, fragment, "DetailFragment");
                }
                else
                {
                    _imgQRBack.Post(() =>
                    {
                        cameraSource.Stop();
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(_mainAct);
                        alert.SetTitle("Thông báo");
                        alert.SetMessage("Không tìm thấy người dùng.");
//#if DEBUG
//                        alert.SetMessage("Email người dùng: " + pilotEmail);
//#endif
                        alert.SetPositiveButton("Đóng",
                            (senderAlert, args) =>
                            {
                                CheckQRCode = true;
                                cameraSource.Start(_surfaceViewFull.Holder);
                                alert.Dispose();
                            });
                        Dialog dialog = alert.Create();
                        dialog.SetCanceledOnTouchOutside(false);
                        dialog.Show();
                    });
                }
            }
        }

        public void Release()
        {

        }
        //private void ShowAlertDialog_QR(MainActivity mainAct, string title, string mess, string positive, string negative)
        //{
        //    Alert = new Android.Support.V7.App.AlertDialog.Builder(mainAct);
        //    Alert.SetTitle(title);
        //    Alert.SetMessage(mess);
        //    Alert.SetNegativeButton(negative, (senderAlert, args) =>
        //    {
        //        _flagDialog = false;
        //        Alert.Dispose();
        //    });
        //    Dialog dialog = Alert.Create();
        //    dialog.SetCanceledOnTouchOutside(false);
        //    dialog.Show();
        //}
    }
}