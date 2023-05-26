using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.DataProvider
{
    public class ProviderLicense : ProviderBase
    {
        public List<BeanUserLicense> GetlicenseFromServer(CancellationToken token = new CancellationToken())
        {
            List<BeanUserLicense> lst = new List<BeanUserLicense>();
            try
            {
                List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                JsonSerializerSettings JsonDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                //lstGet.Add(new KeyValuePair<string, string>("func", "get"));
                //lstGet.Add(new KeyValuePair<string, string>("bname", "BeanNotify"));
                PAR par = new PAR(null, null, null);

                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                ///api/User.ashx?func=GetUserLicense&uid= +UserID
                JObject retData = GetJsonDataFromAPI(combieUrl + "/User.ashx?func=GetListUserLicense&uid=" + CmmVariable.SysConfig.UserId, ref CmmVariable.M_AuthenticatedHttpClient, par, true, token:token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                else if (strStatus.Equals("SUCCESS"))
                    lst = retData["data"].ToObject<List<BeanUserLicense>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - getlicenseFromServer  - Err:" + ex.ToString());
            }
            return lst;

        }
       

        /// <summary>
        /// get list library
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BeanLibrary> GetLibraryFromServer(int id, CancellationToken token = new CancellationToken())
        {
            List<BeanLibrary> lst = new List<BeanLibrary>();
            try
            {
                PAR par = new PAR(null, null, null);

                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;

                JObject retData = GetJsonDataFromAPI(combieUrl + "/ApiLibrary.ashx?func=GetFolderItem&fid=" + id, ref CmmVariable.M_AuthenticatedHttpClient, par, true, token:token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                else if (strStatus.Equals("SUCCESS"))
                    lst = retData["data"].ToObject<List<BeanLibrary>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - getLibraryFromServer  - Err:" + ex.ToString());
            }
            return lst;

        }
        /// <summary>
        /// search list library
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<BeanLibrary> SearchLibraryFromServer(string key, CancellationToken token = new CancellationToken())
        {
            List<BeanLibrary> lst = new List<BeanLibrary>();
            try
            {
                PAR par = new PAR(null, null, null);

                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;

                JObject retData = GetJsonDataFromAPI(combieUrl + "/ApiLibrary.ashx?func=FindFolderItem&filter=" + key, ref CmmVariable.M_AuthenticatedHttpClient, par, false, token:token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                if (strStatus.Equals("SUCCESS"))
                    lst = retData["data"].ToObject<List<BeanLibrary>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - searchLibraryFromServer  - Err:" + ex.ToString());
            }
            return lst;
        }
    }
}
