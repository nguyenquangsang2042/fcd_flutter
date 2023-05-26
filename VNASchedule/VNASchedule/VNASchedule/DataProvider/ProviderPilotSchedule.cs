using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using VNASchedule.Bean;
using VNASchedule.Class;


namespace VNASchedule.DataProvider
{
    //
    public class ProviderPilotSchedule : ProviderBase
    {
        public bool UpdateSchedule(bool flgChkUpdate = true, int dataLimitDay = 1000, CancellationToken token = new CancellationToken())
        {
            bool retValue = false;
            retValue = UpdateMasterData<BeanPilotSchedule>(flgChkUpdate, dataLimitDay, token: token);
            UpdateMasterData<BeanItemDeleted>(flgChkUpdate, dataLimitDay, token: token);
            return retValue;
        }

        public List<BeanPilotScheduleClone> GetScheduleFromServer(CancellationToken token = new CancellationToken())
        {
            List<BeanPilotScheduleClone> lst = new List<BeanPilotScheduleClone>();
            try
            {
                List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                JsonSerializerSettings JsonDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                lstGet.Add(new KeyValuePair<string, string>("func", "get"));
                PAR par = new PAR(lstGet, null, null);
                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                JObject retData = GetJsonDataFromAPI(combieUrl + "/Schedule.ashx", ref CmmVariable.M_AuthenticatedHttpClient, par, false, token: token);
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                if (strStatus.Equals("SUCCESS"))
                    lst = retData["data"].ToObject<List<BeanPilotScheduleClone>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - GetWorkersRoundLocation  - Err:" + ex.ToString());
            }
            return lst;

        }
        public List<BeanPilotScheduleDaily> GetDailyFlightFromServer(DateTime dateSelected, bool isUpdate, CancellationToken token = new CancellationToken())
        {
            SQLiteConnection con = null;
            List<BeanPilotScheduleDaily> retValue = new List<BeanPilotScheduleDaily>();
            //var a = dateSelected.Date.AddDays(-7);
            try
            {
                con = new SQLiteConnection(CmmVariable.M_DataPath);
                SQLite3.BusyTimeout(con.Handle, 60000);

                con.BeginTransaction();

                Type type = typeof(BeanPilotScheduleDaily);
                string IdSchedule = type.Name;
                Type typePilot = typeof(BeanScheduleUser);
                string IdPilot = typePilot.Name;
                string Modified = "";
                TableQuery<DBVariable> table = con.Table<DBVariable>();
                var items = from i in table
                            where i.Id == IdSchedule
                            select i;
                if (items.Count() > 0)
                {
                    DateTime ModifiedDate = new DateTime();
                    Modified = items.First().Value;
                    if (!string.IsNullOrEmpty(Modified))
                        ModifiedDate = Convert.ToDateTime(Modified);

                    if (ModifiedDate.Date != dateSelected.Date)
                        Modified = DateTime.Now.AddDays(CmmVariable.SysConfig.DataLimitDay * -1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                    Modified = DateTime.Now.AddDays(CmmVariable.SysConfig.DataLimitDay * -1).ToString("yyyy-MM-dd HH:mm:ss");

                List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
                JsonSerializerSettings JsonDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                //lstGet.Add(new KeyValuePair<string, string>("rid", DateTime.UtcNow.Date.ToString("yyyy-MM-dd")));
                lstGet.Add(new KeyValuePair<string, string>("rid", dateSelected.ToString("yyyy-MM-dd  HH:mm:ss")));
                lstGet.Add(new KeyValuePair<string, string>("Modified", Modified));
                lstGet.Add(new KeyValuePair<string, string>("obj", "false"));
                PAR par = new PAR(lstGet, null, null);
                string combieUrl = CmmVariable.M_Domain;
                if (!string.IsNullOrEmpty(CmmVariable.SysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.SysConfig.Subsite;
                combieUrl += CmmVariable.M_ApiPath;
                BeanPilotScheduleDaily bCurrent = new BeanPilotScheduleDaily();
                JObject retData = GetJsonDataFromAPI(combieUrl + bCurrent.GetServerUrl(), ref CmmVariable.M_AuthenticatedHttpClient, par, true, token: token);
                if (isUpdate == true)
                {
                    CmmEvent.StartInsertDB(null, null);
                }
                if (retData == null) return null;
                string strStatus = retData.Value<string>("status");
                if (strStatus.Equals("ERR"))
                    return null;
                if (strStatus.Equals("SUCCESS"))
                {
                    retValue = retData["data"].ToObject<List<BeanPilotScheduleDaily>>();
                    string sysDateNow = "";
                    List<BeanScheduleUser> lst_ScheduleUsers = new List<BeanScheduleUser>();
                    sysDateNow = retData.Value<string>("dateNow");

                    if (retValue != null && retValue.Count > 0)
                    {
                        if (isUpdate == false)
                        {
                            var sqlSel = string.Format("DELETE  FROM BeanPilotScheduleDaily Where DepartureTime < ?");
                            //con.Execute(sqlSel, dateSelected.AddDays(-6));
                            con.CreateCommand(sqlSel, dateSelected.AddDays(-6)).ExecuteNonQuery();
                            var sqlSel2 = string.Format("DELETE  FROM BeanScheduleUser Where  DepartureTime < ?");
                            //con.Execute(sqlSel2, dateSelected.AddDays(-6));
                            con.CreateCommand(sqlSel2, dateSelected.AddDays(-6)).ExecuteNonQuery();
                            retValue = retData["data"].ToObject<List<BeanPilotScheduleDaily>>();
                            for (int i = 0; i < retValue.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(retValue[i].LstAllPersonal))
                                {
                                    var result = JsonConvert.DeserializeObject<List<BeanScheduleUser>>(retValue[i].LstAllPersonal);
                                    for (int x = 0; x < result.Count; x++)
                                    {
                                        result[x].ScheduleId = retValue[i].ID;
                                        result[x].id = Guid.NewGuid() + string.Empty;
                                        result[x].DepartureTime = retValue[i].DepartureTime;
                                    }
                                    lst_ScheduleUsers.AddRange(result);
                                }
                            }
                            con.InsertAll(lst_ScheduleUsers);
                            con.InsertAll(retValue);
                        }
                        else
                        {
                            var sqlSel = string.Format("DELETE FROM BeanPilotScheduleDaily Where DepartureTime >= ? AND DepartureTime < ?");
                            //con.Execute(sqlSel, dateSelected, dateSelected.AddDays(1));
                            con.CreateCommand(sqlSel, dateSelected, dateSelected.AddDays(1)).ExecuteNonQuery();

                            for (int i = 0; i < retValue.Count; i++)
                            {
                                var sqlSel2 = string.Format("DELETE  FROM BeanScheduleUser Where  ScheduleId = ?");
                                con.CreateCommand(sqlSel2, retValue[i].ID).ExecuteNonQuery();
                                //var sqlSel3 = string.Format("SELECT *    FROM BeanScheduleUser Where  ScheduleId = ?");
                                //var cl = con.Execute(sqlSel3, retValue[i].ID);
                                if (!string.IsNullOrEmpty(retValue[i].LstAllPersonal))
                                {
                                    var result = JsonConvert.DeserializeObject<List<BeanScheduleUser>>(retValue[i].LstAllPersonal);
                                    for (int x = 0; x < result.Count; x++)
                                    {
                                        result[x].ScheduleId = retValue[i].ID;
                                        result[x].id = Guid.NewGuid() + string.Empty;
                                        result[x].DepartureTime = retValue[i].DepartureTime;
                                    }
                                    lst_ScheduleUsers.AddRange(result);
                                }
                            }
                            con.InsertAll(lst_ScheduleUsers);
                            con.InsertAll(retValue);

                        }
                        //var sqlSelextAll = string.Format("SELECT * FROM BeanPilotScheduleDaily WHERE DepartureTime >= ? AND DepartureTime < ? ");
                        //var lst_noti_all = con.Query<BeanPilotScheduleDaily>(sqlSelextAll, selectedDate.Date, nextSelectedDate.Date);
                        //var a =selectedDate.ToString("yyyy-MM-dd HH:mm:ss");  
                        //var sqlSel = string.Format("DELETE FROM BeanPilotScheduleDaily WHERE DepartureTime >= ? AND DepartureTime < ? ");
                        //var sqlSel = string.Format("DELETE FROM BeanPilotScheduleDaily WHERE DepartureTime >= ? AND DepartureTime < ? ");
                        //con.Execute(sqlSel, selectedDate.Date, nextSelectedDate.Date);
                        //con.commit();
                        //if (isUpdate == true)
                        //{
                        //    CmmEvent.FinishInsertDB(null, null);
                        //}               
                    }
                    con.Commit();
                    CmmFunction.UpdateDBVariable(new DBVariable(IdPilot, sysDateNow));
                    CmmFunction.UpdateDBVariable(new DBVariable(IdSchedule, sysDateNow));

                }

                if (isUpdate == true)
                {
                    CmmEvent.FinishInsertDB(null, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                if (con != null)
                    con.Rollback();
                Console.WriteLine("Error - GetWorkersRoundLocation  - Err:" + ex.ToString());
                if (ex.ToString().Contains("Busy"))
                {
                    GetDailyFlightFromServer(dateSelected, isUpdate);
                }
                CmmEvent.FinishInsertDB(null, null);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return retValue;
        }

        //public List<BeanScheduleUser> GetDailyPilot(DateTime dtSelected,bool isUpdate)
        //{
        //    SQLiteConnection con = new SQLiteConnection(CmmVariable.M_DataPath);
        //    DateTime selectedDate = new DateTime();
        //    DateTime nextSelectedDate = new DateTime();
        //    selectedDate = dtSelected;
        //    nextSelectedDate = selectedDate.AddDays(1);
        //    List<BeanScheduleUser> retValue = new List<BeanScheduleUser>();
        //    try
        //    {
        //        List<KeyValuePair<string, string>> lstGet = new List<KeyValuePair<string, string>>();
        //        JsonSerializerSettings JsonDateFormatSettings = new JsonSerializerSettings
        //        {
        //            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        //        };
        //        lstGet.Add(new KeyValuePair<string, string>("rid", dtSelected.ToString("yyyy-MM-dd")));
        //        lstGet.Add(new KeyValuePair<string, string>("obj", "false"));
        //        PAR par = new PAR(lstGet, null, null);

        //        string combieUrl = CmmVariable.M_Domain;
        //        if (!string.IsNullOrEmpty(CmmVariable.sysConfig.Subsite)) combieUrl = combieUrl.TrimEnd('/') + "/" + CmmVariable.sysConfig.Subsite;
        //        combieUrl += CmmVariable.M_ApiPath;

        //        BeanScheduleUser bCurrent = new BeanScheduleUser();

        //        JObject retData = GetJsonDataFromAPI(combieUrl + bCurrent.GetServerUrl(), ref CmmVariable.M_AuthenticatedHttpClient, par, false);
        //        if (retData == null) return null;
        //        string strStatus = retData.Value<string>("status");
        //        if (strStatus.Equals("ERR"))
        //            return null;
        //        //if (strStatus.Equals("SUCCESS"))
        //        //    retValue = retData["data"].ToObject<List<BeanPilotScheduleDaily>>();
        //        if (strStatus.Equals("SUCCESS"))
        //        {
        //            //retValue = retData["data"].ToObject<List<BeanScheduleUser>>();
        //            //var sqlSel = string.Format("DELETE FROM BeanScheduleUser");
        //            //con.Execute(sqlSel, selectedDate.Date, nextSelectedDate.Date);
        //            //con.InsertAll(retValue);
        //        }
        //        if (isUpdate == true)
        //        {
        //            CmmEvent.FinishInsertDB(null, null);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        con.Rollback();
        //        con.Commit();
        //        con.Close();
        //        Console.WriteLine("Error - GetWorkersRoundLocation  - Err:" + ex.ToString());
        //        if (isUpdate == true)
        //        {
        //            CmmEvent.FinishInsertDB(null, null);
        //        }
        //    }
        //    return retValue;

        //}
    }
}
