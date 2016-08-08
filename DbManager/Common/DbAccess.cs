using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Data;

namespace DbManager.Common
{
    public class DbAccess
    {
        private SqlConnection _conn = null;
        private SqlTransaction _tran = null;

        public void Connect()
        {
            try
            {
                if(_conn == null)
                {
                    _conn = new SqlConnection();
                }

                _conn.ConnectionString = ConfigurationManager.ConnectionStrings["DbManager.Properties.Settings.TestDb"].ConnectionString;
                _conn.Open();

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Close()
        {
            try
            {
                _conn.Close();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// SQLの実行
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="tot">タイムアウト値</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataTable ExecuteSql(String sql, int tot)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sql, _conn, _tran);

                if (tot > -1)
                {
                    sqlCommand.CommandTimeout = tot;
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                adapter.Fill(dt);
                adapter.Dispose();
                sqlCommand.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteSql Error", ex);
            }

            return dt;
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        /// <remarks></remarks>
        public void BeginTransaction()
        {
            try
            {
                _tran = _conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("BeginTransaction Error", ex);
            }
        }

        /// <summary>
        /// コミット
        /// </summary>
        /// <remarks></remarks>
        public void CommitTransaction()
        {
            try
            {
                if (_tran != null)
                {
                    _tran.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CommitTransaction Error", ex);
            }
            finally
            {
                _tran = null;
            }
        }

        /// <summary>
        /// ロールバック
        /// </summary>
        /// <remarks></remarks>
        public void RollbackTransaction()
        {
            try
            {
                if (_tran != null)
                {
                    _tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RollbackTransaction Error", ex);
            }
            finally
            {
                _tran = null;
            }
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        /// <remarks></remarks>
        ~DbAccess()
        {
            Close();
        }
    }
}
