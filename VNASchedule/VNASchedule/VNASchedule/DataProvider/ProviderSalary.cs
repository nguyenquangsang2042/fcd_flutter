using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.DataProvider
{
    public class ProviderSalary : ProviderBase
    {
        //https://pilot.vuthao.com/API/ApiPublic.ashx?func=get&bname=BeanSalary&FromDate=2021-1-20&ToDate=2021-3-24&isFirst=1&enc=RSA 
        public List<BeanSalary> GetSalaryFromServer(string todate, string fromdate, CancellationToken token = new CancellationToken())
        {
            List<BeanSalary> lst = new List<BeanSalary>();
            try
            {
                JsonSerializerSettings JsonDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                PAR par = new PAR(null, null, null);



                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                JObject retData = GetJsonDataFromAPI(combieUrl + "/ApiPublic.ashx?func=get&bname=BeanSalary&FromDate=" + fromdate + "&ToDate=" + todate, ref CmmVariable.M_AuthenticatedHttpClient, par, false, token:token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                if (strStatus.Equals("SUCCESS"))
                    lst = retData["data"].ToObject<List<BeanSalary>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - GetWorkersRoundLocation  - Err:" + ex.ToString());
            }
            return lst;
        }
    }
}
