using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using VNASchedule.Bean;
using VNASchedule.DataProvider;
using System.Text.RegularExpressions;
using System.Globalization;
using static VNASchedule.DataProvider.ProviderBase;
using System.Linq;

namespace VNASchedule.Class
{
    public class CmmFunction
    {
        public static string GetTitle(string fieldId, string defaultValue = "")
        {
            string retValue = defaultValue;
            try
            {
                Dictionary<string, string> a = CmmVariable.M_LangData;

                if (CmmVariable.M_LangData == null)
                {
                    CmmVariable.M_LangData = new Dictionary<string, string>();
                    if (File.Exists(CmmVariable.M_DataLangPath))
                    {
                        using (var conn = new SQLiteConnection(CmmVariable.M_DataLangPath))
                        {
                            List<BeanAppLanguage> lstLang = conn.Query<BeanAppLanguage>("SELECT * FROM BeanAppLanguage");
                            foreach (BeanAppLanguage langItem in lstLang)
                            {
                                CmmVariable.M_LangData.Add(langItem.Key, langItem.Value);
                            }
                        }
                    }
                }

                //Nếu trong dictionary không có thì lấy từ dư liệu sqlLite
                string outValue = "";
                if (CmmVariable.M_LangData.TryGetValue(fieldId, out outValue))
                {
                    retValue = outValue;
                }

            }
            catch (Exception ex)
            {

            }


            return retValue;
        }

        public static string GetTitle(string fieldId)
        {
            string retValue = "";
            try
            {
                switch (fieldId)
                {
                    case "cmm_title_notify":
                        retValue = "Notify";
                        break;
                    case "cmm_title_warning":
                        retValue = "Warning";
                        break;
                    case "cmm_title_error":
                        retValue = "Error";
                        break;
                    case "cmm_Contacking_server_wait":
                        retValue = "Server connecting. Please wait....";
                        break;
                    case "cmm_please_wait":
                        retValue = "Please wait....";
                        break;
                    case "cmm_TrackingInfomation":
                        retValue = "Action history";
                        break;
                    case "cmm_BtnClose":
                        retValue = "Close";
                        break;
                    case "mess_PleaseEnterYourLoginNameAndPass":
                        retValue = "Please enter your Account";
                        break;
                    case "mess_PleaseEnterYourLoginFailed":
                        retValue = "Login failed";
                        break;
                    case "mess_PleaseEnterYourIdea":
                        retValue = "Please enter your comments";
                        break;
                    case "mess_ConnectServerFailed":
                        retValue = "Connect server failed";
                        break;
                    case "mess_ConnectInternetailed":
                        retValue = "Connect internet failed";
                        break;
                    case "mess_UpdateFailed":
                        retValue = "Update failed";
                        break;
                    case "btn_Approve":
                        retValue = "Approve";
                        break;
                    case "btn_Deny":
                        retValue = "Reject";
                        break;
                    case "lb_TotalCost":
                        retValue = "Total cost";
                        break;
                    case "lb_Department":
                        retValue = "DEPARTMENT";
                        break;
                    case "lb_AccountCode":
                        retValue = "ACCOUNT CODE";
                        break;
                    case "lb_RequestedBy":
                        retValue = "Requested by";
                        break;
                    case "lb_DateRequested":
                        retValue = "Date requested";
                        break;
                    case "lb_requester":
                        retValue = "Requester";
                        break;
                    case "lb_Quantity":
                        retValue = "Quantity";
                        break;
                    case "lb_ListOrderDetail":
                        retValue = "List order detail";
                        break;
                    case "dlg_ApproveTitle":
                        retValue = "Approve";
                        break;
                    case "dlg_DenyTitle":
                        retValue = "Reject";
                        break;
                }
                return retValue;

                if (CmmVariable.M_LangData == null)
                {
                    CmmVariable.M_LangData = new Dictionary<string, string>();
                }

                //Nếu trong dictionary không có thì lấy từ dư liệu sqlLit
                if (CmmVariable.M_LangData.TryGetValue(fieldId, out retValue))
                {
                    //

                }
            }
            catch (Exception) { }


            return retValue;
        }

        public static bool IsUnicode(string input)
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(input);
            return asciiBytesCount != unicodBytesCount;
        }

        public static string GetAppSetting(string key)
        {
            string retValue = "";
            SQLiteConnection con = new SQLiteConnection(CmmVariable.M_DataPath);
            try
            {
                string sqlSel = "SELECT VALUE FROM BeanSettings WHERE KEY = ?";
                //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";
                List<BeanSettings> lstObjChk = con.Query<BeanSettings>(sqlSel, key);
                if (lstObjChk != null && lstObjChk.Count > 0)
                    retValue = lstObjChk[0].VALUE;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return retValue;
        }
        public static List<BeanWard> getElementAddess_Ward(string key, bool toSelect)
        {
            List<BeanWard> lstObjChk = new List<BeanWard>();
            using (SQLiteConnection con = new SQLiteConnection(CmmVariable.M_DataPath))
            {
                if (!toSelect)
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        try
                        {
                            string sqlSel = "SELECT * FROM BeanWard";
                            lstObjChk = con.Query<BeanWard>(sqlSel);
                            //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                            return null;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                    else
                    {
                        try
                        {
                            string sqlSel = "SELECT * FROM BeanWard where ID = ? ";
                            lstObjChk = con.Query<BeanWard>(sqlSel, key);
                            //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                            return null;
                        }
                    }
                }
                else
                {
                    try
                    {
                        string sqlSel = "SELECT * FROM BeanWard where DistrictID = ? ";
                        lstObjChk = con.Query<BeanWard>(sqlSel, key);
                        //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                        return null;
                    }
                }
                return lstObjChk;
            }
        }
        public static List<BeanDistrict> getElementAddess_District(string key, bool toSelect)
        {
            List<BeanDistrict> lstObjChk = new List<BeanDistrict>();
            using (SQLiteConnection con = new SQLiteConnection(CmmVariable.M_DataPath))
            {
                if (!toSelect)
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        try
                        {
                            string sqlSel = "SELECT * FROM BeanDistrict";
                            lstObjChk = con.Query<BeanDistrict>(sqlSel);
                            //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                            return null;
                        }
                    }
                    else
                    {
                        try
                        {
                            string sqlSel = "SELECT * FROM BeanDistrict where ID = ? ";
                            lstObjChk = con.Query<BeanDistrict>(sqlSel, key);
                            //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                            return null;
                        }
                    }
                }
                else
                {
                    try
                    {
                        string sqlSel = "SELECT * FROM BeanDistrict where ProvinceID = ? ";
                        lstObjChk = con.Query<BeanDistrict>(sqlSel, key);
                        //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                        return null;
                    }
                }
                return lstObjChk;
            }
        }
        public static List<BeanProvince> getElementAddess_Province(string key, bool ToSelect)
        {
            List<BeanProvince> lstObjChk = new List<BeanProvince>();
            using (SQLiteConnection con = new SQLiteConnection(CmmVariable.M_DataPath))
            {
                if (!ToSelect)
                {
                    if (string.IsNullOrEmpty(key)) // get all
                    {
                        try
                        {
                            string sqlSel = "SELECT * FROM BeanProvince";
                            lstObjChk = con.Query<BeanProvince>(sqlSel);
                            //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                            return null;
                        }
                    }
                    else //=> get init 
                    {
                        try
                        {
                            string sqlSel = "SELECT * FROM BeanProvince where ID = ? ";
                            lstObjChk = con.Query<BeanProvince>(sqlSel, key);
                            //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                            return null;
                        }
                    }
                }
                else //=> get by NationID
                {
                    try
                    {
                        string sqlSel = "SELECT * FROM BeanProvince where NationID = ? ";
                        lstObjChk = con.Query<BeanProvince>(sqlSel, key);
                        //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                        return null;
                    }
                }
                return lstObjChk;
            }
        }
        public static List<BeanNation> getElementAddess_Nation(string key)
        {
            List<BeanNation> lstObjChk = new List<BeanNation>();
            SQLiteConnection con = new SQLiteConnection(CmmVariable.M_DataPath);
            if (string.IsNullOrEmpty(key))
            {
                try
                {
                    string sqlSel = "SELECT * FROM BeanNation where id = 1";
                    lstObjChk = con.Query<BeanNation>(sqlSel);
                    //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                }
                catch (Exception ex)
                {
                    Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                    return null;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                try
                {
                    string sqlSel = "SELECT * FROM BeanNation where  id = 1 ";
                    lstObjChk = con.Query<BeanNation>(sqlSel);
                    //string sqlSelAll = "SELECT VALUE FROM BeanSettings ";

                }
                catch (Exception ex)
                {
                    Console.WriteLine("CmmFunction - GetAppSetting - ERROR: " + ex.ToString());
                    return null;
                }
                finally
                {
                    con.Close();
                }
            }
            return lstObjChk;
        }
        public static int GetWeekOfYear(DateTime time)
        {
            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            // Cách tuần bắt đầu từ Chủ nhật
            /*if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            else
            {
                time = time.AddDays(1);
            }*/

            // Cách tuần bắt đầu từ Thứ 2
            if (day == DayOfWeek.Monday)
            {
                time = time.AddDays(7);
            }
            else
            {
                time = time.AddDays(0);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }


        /// <summary>
        /// Login tới server
        /// </summary>
        /// <param name="loginUrl">Địa chỉ Url API thực hiện login</param>
        /// <param name="Mobile">Số điện thoại</param>
        /// <param name="OTP">Pass hoặc mã OTP</param>
        /// <param name="loginType">Phân loại login: 1: login thông thường, 2: auto login, 3: login ghi đè DeviceInfo</param>
        public static HttpClient Login(string loginUrl, string mobile, string OTP, bool flgTickEventLogin = false, int loginType = 1)
        {
            //if (CmmVariable.M_AuthenticatedHttpClient == null)
            //{
                CmmVariable.M_AuthenticatedHttpClient = InstanceHttpClient();
            //}

            HttpClient M_AuthenticatedHttpClient = Login(CmmVariable.M_AuthenticatedHttpClient, loginUrl, mobile, OTP, flgTickEventLogin, loginType);
            if (M_AuthenticatedHttpClient != null)
            {
                CmmVariable.M_AuthenticatedHttpClient = M_AuthenticatedHttpClient;
            }
            return M_AuthenticatedHttpClient;
        }

        public static HttpClient InstanceHttpClient()
        {
            try
            {
                if (CmmVariable.M_AuthenticatedCookie == null)
                {
                    CmmVariable.M_AuthenticatedCookie = new CookieContainer();
                }
                HttpClientHandler handler = new HttpClientHandler();
                //Thuốc đặc trị site UAT bypass ssl
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                //bypass certificate - dùng trường hợp lỗi certificates
                //handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                //{
                //    //if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                //    //    return true;
                //    //else
                //    //    return false;
                //    return true;
                //};
                handler.CookieContainer = CmmVariable.M_AuthenticatedCookie;
                handler.AllowAutoRedirect = true;
                handler.UseProxy = false;
                HttpClient client = new HttpClient(handler);
                client.BaseAddress = new Uri(CmmVariable.M_Domain, UriKind.RelativeOrAbsolute);
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("ie");
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Zune 4.0; InfoPath.3; MS-RTC LM 8; .NET4.0C; .NET4.0E)");
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("CERN-LineMode/2.15 libwww/2.17b3");

                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CmmFunction - Login - ERROR: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Login tới server
        /// </summary>
        /// <param name="loginUrl">Địa chỉ Url API thực hiện login</param>
        /// <param name="Mobile">Số điện thoại</param>
        /// <param name="OTP">Pass hoặc mã OTP</param>
        /// <param name="loginType">Phân loại login: 1: login thông thường, 2: auto login, 3: login ghi đè DeviceInfo</param>
        public static HttpClient Login(HttpClient client, string loginUrl, string loginname, string pass, bool flgTickEventLogin = false, int loginType = 1)
        {
            try
            {
                List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                lstGet.Add(new KeyValuePair<string, string>("func", "login"));
                lstGet.Add(new KeyValuePair<string, string>("reLogin", "1"));
                string s = CmmVariable.SysConfig.DeviceInfo;
                var userInfor = new
                {
                    LoginName = loginname,
                    Password = pass,
                    deviceInfo = CmmVariable.SysConfig.DeviceInfo,
                    loginType = loginType.ToString(),
                    userTypeLogin = "1"
                };
                List<KeyValuePair<string, string>> lstPost = new List<KeyValuePair<string, string>>();
                lstPost.Add(new KeyValuePair<string, string>("data", JsonConvert.SerializeObject(userInfor)));
                //lstPost.Add(new KeyValuePair<string, string>("deviceInfo", CmmVariable.SysConfig.DeviceInfo));
                //lstPost.Add(new KeyValuePair<string, string>("loginType", loginType.ToString()));
                //lstPost.Add(new KeyValuePair<string, string>("userTypeLogin", "1"));// default: 1

                ProviderBase pro = new ProviderBase();
                JObject retData = pro.GetJsonDataFromAPI(loginUrl, ref client, new ProviderBase.PAR(lstGet, lstPost), false);
                if (retData == null)
                {
                    if (flgTickEventLogin)
                        CmmEvent.ReloginRequest_Performence(null, null);

                    return null;
                }

                string strStatus = retData.Value<string>("status");
                if (strStatus == null)
                {
                    if (flgTickEventLogin)
                        CmmEvent.ReloginRequest_Performence(null, null);

                    return null;
                }

                if (strStatus.Equals("SUCCESS"))
                {
                    //CmmVariable.M_AuthenticatedHttpClient = client;
                    BeanUser userInfo = retData["data"].ToObject<BeanUser>();
                    // Reset số lần login lỗi về 0
                    CmmVariable.M_AutoReLoginNum = 0;

                    if (flgTickEventLogin)
                    {
                        CmmEvent.ReloginRequest_Performence(null, new CmmEvent.LoginEventArgs(true, loginname, pass, userInfo));
                    }
                    return client;
                }
                else if (strStatus.Equals("ERR"))
                {
                    KeyValuePair<string, string> mess = retData["mess"].ToObject<KeyValuePair<string, string>>();

                    // Nếu hệ thống yêu cầu tự động login và số lần login lỗi nhỏ hơn Max thì tiếp thực hiện lại relogin
                    if (mess.Key.Equals("997") || mess.Key.Equals("195"))
                    {
                        if (mess.Key.Equals("997"))
                        {
                            CmmVariable.M_AutoReLoginNum += 1;
                        }

                        if (flgTickEventLogin)
                        {
                            CmmEvent.ReloginRequest_Performence(null, new CmmEvent.LoginEventArgs(false, loginname, pass, null, "997"));
                        }
                    }

                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CmmFunction - Login - ERROR: " + ex.ToString());
                if (flgTickEventLogin)
                    CmmEvent.ReloginRequest_Performence(null, null);

                return null;
            }
        }

        public static bool UploadImage(List<KeyValuePair<string, string>> lstFile, string url)
        {
            bool retValue = false;
            try
            {
                string combieUrl = CmmVariable.M_Domain;
                PAR par = new PAR(null, null, lstFile);
                JObject retData = new ProviderBase().GetJsonDataFromAPI(combieUrl + url, ref CmmVariable.M_AuthenticatedHttpClient, par, false);
                if (retData == null)
                    return retValue;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    retValue = false;
                else
                    retValue = true;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("ERR " + ex.Message);
#endif
            }
            return retValue;
        }

        public static string GetMd5Hash(string input)
        {
            string retValue = "";
            using (MD5 md5Hash = MD5.Create())
            {
                retValue = GetMd5Hash(md5Hash, input);
            }
            return retValue;
        }

        /// <summary>
        /// Mã hóa dữ liệu theo MD5 
        /// </summary>
        /// <param name="md5Hash">Đối tượng MD5</param>
        /// <param name="input">Dữ liệu cần mã hóa</param>
        /// <returns></returns>
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Convert Attach file thành chuỗi Json
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string ConvertAttachFileToStrJson(string strInput)
        {
            if (string.IsNullOrEmpty(strInput)) return "";
            List<BeanAttachFile> lstAttach = new List<BeanAttachFile>();
            string[] arrItem = strInput.Split(new string[] { ";#" }, StringSplitOptions.None);
            if (arrItem.Length > 0)
            {
                foreach (string strItem in arrItem)
                {
                    string[] itemDetail = strItem.Split('|');
                    if (itemDetail.Length > 2)
                    {
                        BeanAttachFile bAttach = new BeanAttachFile();
                        bAttach.Path = itemDetail[0];
                        bAttach.Title = itemDetail[1];
                        bAttach.CategoryName = itemDetail[2];

                        lstAttach.Add(bAttach);
                    }
                }

            }
            return JsonConvert.SerializeObject(lstAttach);

        }

        public static Cookie GetAuthCookie(String Url, String uname, String pswd)
        {
            string envelope =
                        "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"
                        + "<soap:Body>"
                        + "<Login xmlns=\"http://schemas.microsoft.com/sharepoint/soap/\">"
                        + "<username>{0}</username>"
                        + "<password>{1}</password>"
                        + "</Login>" + "</soap:Body>"
                        + "</soap:Envelope>";

            CookieContainer CookieJar = new CookieContainer();
            Uri authServiceUri = new Uri(Url + "/_vti_bin/authentication.asmx");
            HttpWebRequest spAuthReq = HttpWebRequest.Create(authServiceUri) as HttpWebRequest;
            spAuthReq.CookieContainer = CookieJar;
            spAuthReq.Headers["SOAPAction"] = "http://schemas.microsoft.com/sharepoint/soap/Login";
            spAuthReq.ContentType = "text/xml; charset=utf-8";
            spAuthReq.Method = "POST";

            string userName = uname;
            string password = pswd;
            envelope = string.Format(envelope, userName, password);
            StreamWriter streamWriter = new StreamWriter(spAuthReq.GetRequestStream());
            streamWriter.Write(envelope);
            streamWriter.Close();
            HttpWebResponse response = spAuthReq.GetResponse() as HttpWebResponse;
            Cookie returnValue = response.Cookies[0];
            response.Close();
            return returnValue;
        }

        /// <summary>
        /// Khởi tạo dữ liệu ban đầu hoặc khi chọn lại ngôn ngữ
        /// </summary>
        public static void InstantiateLang()
        {
            try
            {
                CmmVariable.M_LangData = new Dictionary<string, string>();

            }
            catch (Exception) { }
        }

        /// <summary>
        /// Khởi tạo Dữ liệu ban đâu khi chạy lần đầu tiên
        /// </summary>
        /// <param name="dataFilePath">Đường dẫn file DataBase sqlite</param>
        /// <returns></returns>
        public static bool InstantiateDB(string dataFilePath, Type type = null)
        {
            try
            {
                if (!File.Exists(dataFilePath))
                {
                    Console.WriteLine("Database does not exist, greate new data");
                    using (var conn = new SQLiteConnection(dataFilePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex))
                    {
                        conn.CreateTable<DBVariable>();
                        conn.CreateTable<BeanMailTemplate>();
                        conn.CreateTable<BeanPilotSchedule>();
                        conn.CreateTable<BeanSettings>();
                        conn.CreateTable<BeanPilotScheduleDaily>();
                        conn.CreateTable<BeanUser>();
                        conn.CreateTable<BeanAirport>();
                        conn.CreateTable<BeanNotify>();
                        conn.CreateTable<BeanEventReminder>();
                        conn.CreateTable<BeanAnnouncement>();
                        conn.CreateTable<BeanUserLicense>();
                        conn.CreateTable<BeanUserTicket>();
                        conn.CreateTable<BeanUserTicketDetail>();
                        conn.CreateTable<BeanUserRelationship>();
                        conn.CreateTable<BeanRelationshipType>();
                        conn.CreateTable<BeanStudent>();
                        conn.CreateTable<BeanUserTicketStatus>();
                        conn.CreateTable<BeanUserTicketCategory>();
                        conn.CreateTable<BeanSalary>();
                        conn.CreateTable<BeanSurveyTable>();
                        conn.CreateTable<BeanFAQs>();
                        conn.CreateTable<BeanHelpDeskCategory>();
                        conn.CreateTable<BeanHelpDesk>();
                        conn.CreateTable<BeanPilotScheduleAll>();
                        conn.CreateTable<BeanDepartment>();
                        conn.CreateTable<BeanPilotSchedulePdf>();
                        conn.CreateTable<BeanScheduleWeekWorking>();
                        conn.CreateTable<BeanScheduleWeekWorkingDetail>();
                        conn.CreateTable<BeanAnnouncementCategory>();
                        conn.CreateTable<BeanSurvey>();
                        conn.CreateTable<BeanScheduleUser>();
                        //thuyngo add
                        conn.CreateTable<BeanLibrary>();
                        // address 
                        conn.CreateTable<BeanNation>();
                        conn.CreateTable<BeanWard>();
                        conn.CreateTable<BeanDistrict>();
                        conn.CreateTable<BeanProvince>();

                        conn.CreateTable<BeanBanner>();
                        conn.CreateTable<BeanMenuApp>();
                        conn.CreateTable<BeanMenuHome>();
                    }
                }
                else if (type != null)
                {
                    Console.WriteLine("Extent table :" + type.Name);
                    using (var conn = new SQLiteConnection(dataFilePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex))
                    {
                        conn.CreateTable(type);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - CmmFunction - instanceDB{(type == null ? "" : " - " + type.Name)}: " + ex.ToString());
            }
            return false;
        }

        /// <summary>
        /// Lấy biến từ DB
        /// </summary>
        /// <param name="ID">Id field muốn lấy</param>
        /// <param name="con">connect nếu đã có trước</param>
        /// <returns></returns>
        public static DBVariable GetVariableFromDB(string ID, SQLiteConnection con = null)
        {
            DBVariable retValue = null;
            try
            {
                if (con == null)
                {
                    con = new SQLiteConnection(CmmVariable.M_DataPath);
                }
                TableQuery<DBVariable> table = con.Table<DBVariable>();
                var items = from i in table
                            where i.Id == ID
                            select i;
                if (items.Count() > 0)
                {
                    return items.First();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("ERR getVariableFromDB: " + ex.Message);
            }
            return retValue;
        }

        /// <summary>
        /// Cập nhật lại biến trong Table DBVariable
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="con"></param>
        /// <returns></returns>

        public static bool UpdateDBVariable(DBVariable variable, SQLiteConnection con = null)
        {
            try
            {
                con = con ?? new SQLiteConnection(CmmVariable.M_DataPath);

                TableQuery<DBVariable> table = con.Table<DBVariable>();
                var items = from i in table
                            where i.Id == variable.Id
                            select i;
                if (items.Count() > 0)
                    con.Update(variable);
                else
                    con.Insert(variable);

                return true;

            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"UpdateDBVariable - {variable.Id} Err: {ex.ToString()}");
#endif
            }
            return false;
        }

        /// <summary>
        /// Lấy Danh sách các văn bản hiện có trên App
        /// </summary>
        /// <param name="con">connection Sqlite nếu có</param>
        /// <returns></returns>
        public static string GetListIdVanBanDen(SQLiteConnection con = null)
        {
            if (con == null)
            {
                con = new SQLiteConnection(CmmVariable.M_DataPath);
            }
            con = new SQLiteConnection(CmmVariable.M_DataPath);
            TableQuery<DBVariable> table = con.Table<DBVariable>();
            var items = from i in table
                        where i.Id == "VBDListID"
                        select i;
            if (items.Count() > 0)
            {
                return items.First().Value;
            }
            return "";
        }

        /// <summary>
        /// Thêm biến ID vào trong danh sách id văn bản
        /// </summary>
        /// <param name="strLstIDVBD"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public static bool ExtentListIdVBD(string strLstIDVBD, SQLiteConnection con = null)
        {
            if (string.IsNullOrEmpty(strLstIDVBD)) return true;
            if (con == null)
            {
                con = new SQLiteConnection(CmmVariable.M_DataPath);
            }
            TableQuery<DBVariable> table = con.Table<DBVariable>();
            var items = from i in table
                        where i.Id == "VBDListID"
                        select i;
            string sql = "UPDATE DBVariable Set Value = (Value || ?) WHERE ID = ?";
            con.Execute(sql, strLstIDVBD, "VBDListID");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_application_Ver"></param>
        /// <param name="_configVariable_Ver"></param>
        /// <returns></returns>
        public static bool CheckIsNewVer(string _application_Ver, string _configVariable_Ver)
        {
            bool res = false;
            string configVariable_Ver = _configVariable_Ver;
            string application_Ver = _application_Ver;

            var version_config = new Version(configVariable_Ver);
            var version_application = new Version(application_Ver);

            var result = version_application.CompareTo(version_config);
            if (result > 0)
            {
                Console.WriteLine("version_application is greater");
                res = true;
            }
            else if (result < 0)
            {
                Console.WriteLine("version_config is greater");
                res = false;
            }
            else
            {
                Console.WriteLine("versions are equal");
                res = false;
            }

            return res;
        }

        public static object GetPropertyValueByName(object obj, string key)
        {
            object retValue = null;
            Type type = obj.GetType();
            System.Reflection.PropertyInfo perInfo = type.GetProperty(key);
            if (perInfo != null)
            {
                retValue = perInfo.GetValue(obj);
            }
            return retValue;
        }

        public static object GetPropertyValue(object obj, PropertyInfo perInfo)
        {
            object retValue = null;
            retValue = perInfo.GetValue(obj);

            return retValue;
        }

        public static PropertyInfo[] GetPropertysWithType(Type objType, Type proType)
        {
            PropertyInfo[] retValue = null;
            PropertyInfo[] arrPro = objType.GetProperties();
            List<PropertyInfo> lstPro = new List<PropertyInfo>();
            foreach (PropertyInfo pro in arrPro)
            {
                if (pro.PropertyType == proType)
                {
                    lstPro.Add(pro);
                }
            }

            if (lstPro.Count > 0) return lstPro.ToArray();

            return retValue;
        }

        /// <summary>
        /// Lấy giá trị trong String nếu Null thì trả về Empty ""
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string GetTextFromStr(string strInput)
        {
            if (string.IsNullOrEmpty(strInput)) return "";

            return strInput;
        }

        /// <summary>
        /// Kiểm tra hành động được phép trên văn bản không
        /// </summary>
        /// <param name="btnIdAction">Id của action muốn kiểm tra</param>
        /// <param name="vanbanBtnAction"> thuộc tính BtnAction trong đối tượng văn bản</param>
        /// <returns>true: được phép, false: không được phép</returns>
        public static bool CheckActionAllowance(int btnIdAction, int vanbanBtnAction)
        {
            return ((vanbanBtnAction & btnIdAction) > 0);
        }

        //public static bool GetDataMstDataFromServer(bool flgFirst = false)
        //{
        //    try
        //    {
        //        /*
        //        //get Department
        //        DepartmentProvider getDepartment = new DepartmentProvider();
        //        getDepartment.getDepartment(CmmVariable.M_getDepartmentLastTime.Value, !flgFirst);

        //        // get User/Group
        //        ContactProvider getUserGroup = new ContactProvider();
        //        getUserGroup.getContactInfolist(CmmVariable.M_getUserGroupLastTime.Value, !flgFirst);

        //        // get Document Data
        //        DocumentProvider getDoc = new DocumentProvider();
        //        GetDataStatus docGetResult = getDoc.getVBDFromList(CmmVariable.M_getDocumentLastTime.Value, !flgFirst);

        //        if (docGetResult == GetDataStatus.SUCC) { 
        //            System.Diagnostics.Debug.Write("MESS: Láy dữ liệu văn bản thành công - Có dữ liệu");
        //        }else if(docGetResult == GetDataStatus.SUCC_NO_DATA){
        //            System.Diagnostics.Debug.Write("MESS: Láy dữ liệu văn bản thành công - Không có dữ liệu");
        //        }else {
        //            System.Diagnostics.Debug.Write("ERR: Láy dữ liệu văn bản không thành công");
        //        }
        //        */
        //        return true;

        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return false;
        //}

        public static bool GetDBtime(string dataFilePath)
        {
            try
            {

                ProviderBase basePrd = new ProviderBase();
                using (var conn = new SQLiteConnection(dataFilePath))
                {
                    //getValue = basePrd.getVariableFromDB(CmmVariable.M_getBinUser.Id, conn);
                    //if (getValue != null) CmmVariable.M_getBinUser = getValue;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("ERR getDBtime: " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Đọc config của chương trình từ file config lên
        /// </summary>
        /// <returns></returns>
        public static bool ReadSetting()
        {
            try
            {
                if (File.Exists(CmmVariable.M_settingFileName))
                {
                    FileStream strm = new FileStream(CmmVariable.M_settingFileName, FileMode.Open, FileAccess.Read);
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter biforInfor = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    CmmVariable.SysConfig = (ConfigVariable)biforInfor.Deserialize(strm);
                    strm.Close();
                    return true;
                }
                else
                {
                    string dirPath = Path.GetDirectoryName(CmmVariable.M_settingFileName);
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("ERR readSetting: " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Ghi thông tin config xuống file
        /// </summary>
        /// <returns></returns>
        public static bool WriteSettingToFile()
        {
            try
            {
                FileStream strm = new FileStream(CmmVariable.M_settingFileName, FileMode.OpenOrCreate, FileAccess.Write);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter biforSetting = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                biforSetting.Serialize(strm, CmmVariable.SysConfig);
                strm.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("ERR readSetting: " + ex.Message);
            }

            return false;
        }

        private static readonly string[] VietnameseSigns = new string[]{
                                                            "aAeEoOuUiIdDyY",
                                                            "áàạảãâấầậẩẫăắằặẳẵ",
                                                            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                                                            "éèẹẻẽêếềệểễ",
                                                            "ÉÈẸẺẼÊẾỀỆỂỄ",
                                                            "óòọỏõôốồộổỗơớờợởỡ",
                                                            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                                                            "úùụủũưứừựửữ",
                                                            "ÚÙỤỦŨƯỨỪỰỬỮ",
                                                            "íìịỉĩ",
                                                            "ÍÌỊỈĨ",
                                                            "đ",
                                                            "Đ",
                                                            "ýỳỵỷỹ",
                                                            "ÝỲỴỶỸ"
                                                            };


        /// <summary>
        /// Bỏ dấu tiếng việt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSignVietnamese(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }
            return str;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strPropName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValueByName(object obj, string strPropName, object value)
        {
            PropertyInfo propInfo = GetProperty(obj, strPropName);
            if (propInfo != null)
            {
                //Nullable<System.DateTime>
                Type t = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                propInfo.SetValue(obj, safeValue, null);
            }

        }

        /// <summary>
        ///  Set giá trị cho thuộc tính của Object
        /// </summary>
        /// <param name="obj">Object muốn set giá trị</param>
        /// <param name="propInfo">Thuộc tính propertyInfo thuộc Class Object</param>
        /// <param name="value">Giá trị muốn set</param>
        /// <returns></returns>
        public static void SetPropertyValue(object obj, PropertyInfo propInfo, object value)
        {
            Type t = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
            object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
            propInfo.SetValue(obj, safeValue, null);


        }

        /// <summary>
        /// Giải mã chuỗi Json thành Object và bỏ qua Catch
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng muốn chuyển đổi thành</typeparam>
        /// <param name="value">Giá trị chuỗi Json</param>
        /// <returns></returns>
        public static T TryDeserializeObject<T>(string value)
        {
            T retValue = default(T);
            try
            {
                retValue = JsonConvert.DeserializeObject<T>(value);
            }
            catch { }
            return retValue;
        }

        /// <summary>
        /// Map phần Data field giữa 2 object
        /// </summary>
        /// <param name="objFrom">object chứa dữ liệu nguồn</param>
        /// <param name="objTo">object muốn map dữ liệu tới</param>
        /// <param name="lstColsFilter">Danh sách các cột muốn lọc lấy dự liệu</param>
        /// <returns></returns>
        public static object MapData(object objFrom, object objTo, List<string> lstColsFilter = null)
        {

            Type objFromType = objFrom.GetType();
            PropertyInfo[] arrProperty = objFromType.GetProperties();
            foreach (PropertyInfo prop in arrProperty)
            {

                // Nếu Property không tồn lại trong List lọc thì bỏ qua
                if (lstColsFilter != null && !lstColsFilter.Contains(prop.Name)) continue;

                object fieldValue = prop.GetValue(objFrom);
                SetPropertyValueByName(objTo, prop.Name, fieldValue);

            }

            return objTo;
        }

        public static PropertyInfo GetProperty(object obj, string strPropName)
        {
            Type type = obj.GetType();
            return type.GetProperty(strPropName);
        }

        public static object GetPropertyValue(object obj, string strPropName)
        {
            Type type = obj.GetType();
            return type.GetProperty(strPropName).GetValue(obj, null);
        }

        public static T ChangeToRealType<T>(object readData)
        {
            if (readData is T)
            {
                return (T)readData;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(readData, typeof(T));
                }
                catch (InvalidCastException ex)
                {
                    return default(T);
                }
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success;
        }


        /// <summary>
        /// Mã hóa dữ liệu bàng Base64
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        /// <summary>
        /// Mã hóa dữ liệu bàng Base64
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Encode(string strInput)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(strInput);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DisplayUnreadNews(int unreadNews)
        {
            if (unreadNews > 99)
                return "99+";
            else
                return unreadNews.ToString();
        }


    }
}
