using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNASchedule.Class;

namespace VNASchedule.Droid.Code
{
    public class SQLiteHelper
    {
        public class SQLiteResult<T>
        {
            public SQLiteResult(bool Status = false, T Data = default(T), List<T> Datas = null, string Message = "")
            {
                this.Status = Status;
                this.Data = Data;
                this.ListData = Datas;
                this.Message = Message;
            }
            public bool Status { get; set; }
            public T Data { get; set; }
            public List<T> ListData { get; set; }
            public string Message { get; set; }
        }

        static string dbpath = CmmVariable.M_DataPath;

        public static SQLiteResult<T> GetAll<T>()
        {
            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(dbpath);
                List<T> rets = con.CreateCommand($"SELECT * FROM {typeof(T).Name}").ExecuteQuery<T>();
                return new SQLiteResult<T>(Status: true, Datas: rets);
            }
            catch (Exception ex)
            {
                return new SQLiteResult<T>(Message: ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    GC.SuppressFinalize(con);
                }
            }
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<T>> GetAllAsync<T>()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                return GetAll<T>();
            });
        }

        public static SQLiteResult<T> GetList<T>(string query, params object[] args)
        {
            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(dbpath);
                List<T> rets = con.CreateCommand(query, args).ExecuteQuery<T>();
                return new SQLiteResult<T>(Status: true, Datas: rets);
            }
            catch (Exception ex)
            {
                return new SQLiteResult<T>(Message: ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    GC.SuppressFinalize(con);
                }
            }
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<T>> GetListAsync<T>(string query, params object[] args)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                return GetList<T>(query, args);
            });
        }

        public static SQLiteResult<T> GetOne<T>(string query)
        {
            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(dbpath);
                T ret = default(T);
                List<T> rets = con.CreateCommand(query).ExecuteQuery<T>();
                if (rets != null && rets.Count > 0)
                    ret = rets[0];
                return new SQLiteResult<T>(Status: true, Data: ret);
            }
            catch (Exception ex)
            {
                return new SQLiteResult<T>(Message: ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    GC.SuppressFinalize(con);
                }
            }
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<T>> GetOneAsync<T>(string query)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                return GetOne<T>(query);
            });
        }

        public static SQLiteResult<int> NonQuery(string query, params object[] ps)
        {
            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                int ret = con.CreateCommand(query, ps).ExecuteNonQuery();
                return new SQLiteResult<int>(Status: true, Data: ret);
            }
            catch (Exception ex)
            {
                return new SQLiteResult<int>(Message: ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    GC.SuppressFinalize(con);
                }
            }
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<int>> NonQueryAsync(string query, params object[] ps)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                return NonQuery(query, ps);
            });
        }

        public static SQLiteResult<T> Scalar<T>(string query)
        {
            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                T ret = con.CreateCommand(query).ExecuteScalar<T>();
                return new SQLiteResult<T>(Status: true, Data: ret);
            }
            catch (Exception ex)
            {
                return new SQLiteResult<T>(Message: ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    GC.SuppressFinalize(con);
                }
            }
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<T>> ScalarAsync<T>(string query)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                return Scalar<T>(query);
            });
        }

        public static SQLiteResult<int> Insert(object obj)
        {
            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                int ret = con.Insert(obj);
                return new SQLiteResult<int>(Status: true, Data: ret);
            }
            catch (Exception ex)
            {
                return new SQLiteResult<int>(Message: ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    GC.SuppressFinalize(con);
                }
            }
        }

        public static SQLiteResult<int> Update(object obj)
        {
            SQLiteConnection con = null;
            try
            {
                con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                int ret = con.Update(obj);
                return new SQLiteResult<int>(Status: true, Data: ret);
            }
            catch (Exception ex)
            {
                return new SQLiteResult<int>(Message: ex.ToString());
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    GC.SuppressFinalize(con);
                }
            }
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<int>> InsertAllAsync(System.Collections.IEnumerable objects, bool runInTransaction = false)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                SQLiteConnection con = null;
                try
                {
                    con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                    int ret = con.InsertAll(objects, runInTransaction);
                    return new SQLiteResult<int>(Status: true, Data: ret);
                }
                catch (Exception ex)
                {
                    return new SQLiteResult<int>(Message: ex.ToString());
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                        GC.SuppressFinalize(con);
                    }
                }
            });
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<int>> UpdateAllAsync(System.Collections.IEnumerable objects, bool runInTransaction = false)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                SQLiteConnection con = null;
                try
                {
                    con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                    int ret = con.UpdateAll(objects, runInTransaction);
                    return new SQLiteResult<int>(Status: true, Data: ret);
                }
                catch (Exception ex)
                {
                    return new SQLiteResult<int>(Message: ex.ToString());
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                        GC.SuppressFinalize(con);
                    }
                }
            });
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<int>> DeleteAsync<T>(object primaryKey)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                SQLiteConnection con = null;
                try
                {
                    con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                    int ret = con.Delete<T>(primaryKey);
                    return new SQLiteResult<int>(Status: true, Data: ret);
                }
                catch (Exception ex)
                {
                    return new SQLiteResult<int>(Message: ex.ToString());
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                        GC.SuppressFinalize(con);
                    }
                }
            });
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<int>> DeleteAllAsync(TableMapping map)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                SQLiteConnection con = null;
                try
                {
                    con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                    int ret = con.DeleteAll(map);
                    return new SQLiteResult<int>(Status: true, Data: ret);
                }
                catch (Exception ex)
                {
                    return new SQLiteResult<int>(Message: ex.ToString());
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                        GC.SuppressFinalize(con);
                    }
                }
            });
        }

        public static async System.Threading.Tasks.Task<SQLiteResult<int>> DeleteAllAsync<T>()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                SQLiteConnection con = null;
                try
                {
                    con = new SQLiteConnection(dbpath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
                    int ret = con.DeleteAll<T>();
                    return new SQLiteResult<int>(Status: true, Data: ret);
                }
                catch (Exception ex)
                {
                    return new SQLiteResult<int>(Message: ex.ToString());
                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                        GC.SuppressFinalize(con);
                    }
                }
            });
        }
    }
}