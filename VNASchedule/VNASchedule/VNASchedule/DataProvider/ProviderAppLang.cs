using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using VNASchedule.Bean;
using VNASchedule.Class;

namespace VNASchedule.DataProvider
{
    public class ProviderAppLang : ProviderBase
    {
        /// <summary>
        /// Update lai Lang từ server
        /// 
        /// </summary>
        /// <param name="flgChkUpdate">true: nếu muốn kiểm tra trước khi update</param>
        /// <returns></returns>
        public bool UpdateLangData(string langCode, bool flgChkUpdate = true, bool flgResetData = false, CancellationToken token = new CancellationToken())
        {
            Type type = typeof(BeanAppLanguage);
            string ID = type.Name;
            string Modified = "";
            string errMess = "";
            SQLiteConnection con = null;
            SQLiteConnection conLang = null;

            try
            {
                con = new SQLiteConnection(CmmVariable.M_DataPath);
                conLang = new SQLiteConnection(CmmVariable.M_DataLangPath);

                if (!flgResetData)
                {
                    TableQuery<DBVariable> table = con.Table<DBVariable>();
                    var items = from i in table
                                where i.Id == ID
                                select i;
                    if (items.Count() > 0)
                    {
                        Modified = items.First().Value;
                    }
                }

                BeanAppLanguage objMst = new BeanAppLanguage();

                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                lstGet.Add(new KeyValuePair<string, string>("func", "get"));
                lstGet.Add(new KeyValuePair<string, string>("lang", langCode));

                lstGet.Add(new KeyValuePair<string, string>("Modified", Modified));
                JObject retData = GetJsonDataFromAPI(combieUrl + objMst.GetServerUrl(), ref CmmVariable.M_AuthenticatedHttpClient, new PAR(lstGet), token:token);
                if (retData == null) return false;

                string strStatus = retData.Value<string>("status");
                if (strStatus == null || !strStatus.Equals("SUCCESS"))
                {
                    return false;
                }

                List<BeanAppLanguage> lstMstData = retData["data"].ToObject<List<BeanAppLanguage>>();
                if (lstMstData == null || lstMstData.Count == 0) return true;

                if (flgResetData)
                {
                    File.Delete(CmmVariable.M_DataLangPath);
                    using (var connLang = new SQLiteConnection(CmmVariable.M_DataLangPath))
                    {
                        connLang.CreateTable<BeanAppLanguage>();
                    }
                }

                String LocalKeyName = BeanBase.getPriKey(type)[0];
                String serverKeyName = BeanBase.getPriKeyS(type)[0];
                List<BeanAppLanguage> lstInsertItem = new List<BeanAppLanguage>();
                List<BeanAppLanguage> lstUpdateItem = new List<BeanAppLanguage>();
                List<string> lstPriKey = new List<string>();

                if (!flgResetData && flgChkUpdate)
                {
                    foreach (BeanAppLanguage item in lstMstData)
                    {
                        string sqlSel;

                        object serKeyValue = CmmFunction.GetPropertyValueByName(item, serverKeyName);


                        // Kiểm tra nếu tồn tại rồi thì update
                        sqlSel = string.Format("SELECT * FROM {0} WHERE {1} = ?", type.Name, serverKeyName);
                        List<BeanAppLanguage> lstObjChk = conLang.Query<BeanAppLanguage>(sqlSel, serKeyValue);

                        if (lstObjChk.Count > 0)
                        {
                            BeanAppLanguage objChk = lstObjChk[0];
                            CmmFunction.SetPropertyValueByName(item, LocalKeyName, CmmFunction.GetPropertyValueByName(objChk, LocalKeyName));
                            lstUpdateItem.Add(item);
                            // Nếu không tồn tại thì Insert
                        }
                        else
                        {
                            lstInsertItem.Add(item);
                        }
                    }
                }
                else
                {
                    lstInsertItem = lstMstData;
                }

                string sysDateNow = retData.Value<string>("dateNow");
                if (string.IsNullOrEmpty(sysDateNow)) return false;

                conLang.BeginTransaction();
                if (lstInsertItem.Count > 0)
                    conLang.InsertAll(lstInsertItem);
                if (lstUpdateItem.Count > 0)
                    conLang.UpdateAll(lstUpdateItem);

                conLang.Commit();
                

                UpdateDBVariable(new DBVariable(ID, sysDateNow));

                if (flgResetData)
                {
                    CmmVariable.M_LangData = null;
                    CmmEvent.UpdateLangComplete_Performence(null, new CmmEvent.UpdateEventArgs(true, langCode));
                }
                return true;
            }
            catch (Exception ex)
            {
                conLang.Rollback();
                System.Diagnostics.Debug.Write("ERR updateLangData: " + ex.Message);
                errMess = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Close();

                if (conLang != null)
                    conLang.Close();
            }
            CmmEvent.UpdateLangComplete_Performence(null, new CmmEvent.UpdateEventArgs(false, langCode, errMess));
            return false;

        }
    }
}
