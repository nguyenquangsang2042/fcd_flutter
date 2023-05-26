using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.DataProvider
{
    //
    public class BlogSite
    {
        public List<BeanUserRelationship> ListUserRelation { get; set; }
    }
    public class ProviderUserRelationship : ProviderBase
    {
        public bool UploadCer(List<KeyValuePair<string, string>> lstFile, SQLiteConnection con = null)
        {
            bool retValue = false;
            try
            {
                //List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                //List<KeyValuePair<string, string>> lstPost = new List<KeyValuePair<string, string>>();
                //lstGet.Add(new KeyValuePair<string, string>("func", "upload"));
                //PAR par = new PAR(lstGet, lstPost, lstFile);
                string combieUrl = CmmVariable.M_Domain;

                string ApiServerUrl = "/API/UploadUserImageProfile.ashx?func=uploadCertificate";

                Dictionary<string, object> postParameters = new Dictionary<string, object>();
                foreach (var item in lstFile)
                {
                    FileStream fs = new FileStream(item.Value, FileMode.Open, FileAccess.Read);
                    byte[] dataFlie_01 = new byte[fs.Length];
                    fs.Read(dataFlie_01, 0, dataFlie_01.Length);
                    fs.Close();

                    postParameters.Add(item.Key, new UploadFile.FileParameter(dataFlie_01, Path.GetFileName(item.Value), "image/*"));
                }


                // Create request and receive response
                string userAgent = "VNAPilot";
                HttpWebResponse webResponse = UploadFile.MultipartFormDataPost(combieUrl + ApiServerUrl, userAgent, postParameters, "POST", CmmVariable.M_AuthenticatedCookie);

                // Process response
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string fullResponse = responseReader.ReadToEnd();
                webResponse.Close();

                if (string.IsNullOrEmpty(fullResponse)) return false;

                JObject json = JObject.Parse(fullResponse);
                string strStatus = json.Value<string>("status");
                if (strStatus.Equals("SUCCESS"))
                    retValue = true;

                //JObject retData = GetJsonDataFromAPI(combieUrl + ApiServerUrl, ref CmmVariable.M_AuthenticatedHttpClient, par);
                //if (retData == null) return false;

                //string strStatus = retData.Value<string>("status");
                //if (strStatus.Equals("SUCCESS"))
                //{
                //    retValue = true;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("ProviderRela - Upload Cer - Err:" + ex.ToString());
                retValue = false;
            }
            return retValue;
        }

        public bool AddUserRelationShip(string relationship, CancellationToken token = new CancellationToken())
        {
            try
            {
                List<KeyValuePair<string, string>> lstPost = new List<KeyValuePair<string, string>>();
                List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                lstPost.Add(new KeyValuePair<string, string>("data", relationship.ToString()));
                PAR par = new PAR(null, lstPost, null);
                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                JObject retData = GetJsonDataFromAPI(combieUrl + "/user.ashx?func=UpdateUserRelationShip", ref CmmVariable.M_AuthenticatedHttpClient, par, false, token:token);
                if (retData == null) return false;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return false;
                else if (strStatus.Equals("SUCCESS"))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public List<BeanUserRelationshipDraff> GetInfoRelationshipUpdate(CancellationToken token = new CancellationToken())
        {
            bool checkReturn = false;
            try
            {


                PAR par = new PAR(null, null, null);
                List<BeanUserRelationshipDraff> lst = new List<BeanUserRelationshipDraff>();
                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                JObject retData = GetJsonDataFromAPI(combieUrl + "/user.ashx?func=InfoRelationshipUpdate", ref CmmVariable.M_AuthenticatedHttpClient, par, false, token:token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                else if (strStatus.Equals("SUCCESS"))
                {

                    lst = retData["data"].ToObject<List<BeanUserRelationshipDraff>>();

                }

                else
                    return null;
                return lst;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public BeanUserDraff GetInfoUserUpdate(CancellationToken token = new CancellationToken())
        {
            try
            {
                PAR par = new PAR(null, null, null);
                List<BeanUserDraff> lst = new List<BeanUserDraff>();
                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                JObject retData = GetJsonDataFromAPI(combieUrl + "/user.ashx?func=InfoUserUpdate", ref CmmVariable.M_AuthenticatedHttpClient, par, false, token:token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                else if (strStatus.Equals("SUCCESS"))
                {
                    lst = retData["data"].ToObject<List<BeanUserDraff>>();
                }

                else
                    return null;
                if (lst != null && lst.Count > 0)
                    return lst[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
