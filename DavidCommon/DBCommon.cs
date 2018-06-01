using System;
using System.Data;
using System.Data.SqlClient;

namespace DavidCommon
{
    public class DBCommon
    {
        #region SQLServer
        /// <summary>
        /// 获得DataTable数据，SqlParameter[] ps = { new SqlParameter("@DealerType",""),}
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, string connStr = "", params SqlParameter[] pars)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                    {
                        if (pars != null)
                        {
                            apter.SelectCommand.Parameters.AddRange(pars);
                        }
                        DataTable da = new DataTable();
                        apter.Fill(da);
                        return da;
                    }
                }
            }
            catch (Exception ex) { return null; }
        }
        /// <summary>
        /// 返回受影响的行数，SqlParameter[] ps = { new SqlParameter("@DealerType",""),}
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteNonquery(string sql, string connStr = "", params SqlParameter[] pars)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (pars != null)
                        {
                            cmd.Parameters.AddRange(pars);
                        }
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { return 0; }
        }
        /// <summary>
        /// 返回第一行第一列，SqlParameter[] ps = { new SqlParameter("@DealerType",""),}
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connStr"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, string connStr = "", params SqlParameter[] pars)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (pars != null)
                        {
                            cmd.Parameters.AddRange(pars);
                        }
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex) { return null; }
        }

        #region Test
        //SqlParameter[] ps = { new SqlParameter("@DealerType",""),
        //                       };
        #endregion

        #endregion

    }
}
