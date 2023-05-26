using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.DataProvider
{
    public class ProviderNotify : ProviderBase
    {
        public List<BeanNotify> GetnotifyFromServer(CancellationToken token = new CancellationToken())
        {
            List<BeanNotify> lst = new List<BeanNotify>();
            try
            {
                List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                JsonSerializerSettings JsonDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                lstGet.Add(new KeyValuePair<string, string>("func", "get"));
                lstGet.Add(new KeyValuePair<string, string>("bname", "BeanNotify"));
                PAR par = new PAR(lstGet, null, null);

                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                JObject retData = GetJsonDataFromAPI(combieUrl + "/ApiPublic.ashx", ref CmmVariable.M_AuthenticatedHttpClient, par, false, token:token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                if (strStatus.Equals("SUCCESS"))
                    lst = retData["data"].ToObject<List<BeanNotify>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - GetWorkersRoundLocation  - Err:" + ex.ToString());
            }
            return lst;
       }
    }
}
