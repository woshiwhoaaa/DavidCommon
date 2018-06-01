using System;
using System.Linq;
using Dapper;

namespace DavidCommon
{
    public class DapperCommon
    {
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="obj">匿名函数new{}</param>
        /// <param name="connString">链接字符串</param>
        /// <returns></returns>
        public static int GetCount<T>(string sql, object obj, string connString)
        {
            int count = 0;
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    count = conn.Query<T>(sql, obj).Count();
                }
            }
            catch { }
            return count;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="obj">匿名函数new{}</param>
        /// <param name="connString">链接字符串</param>
        /// <returns></returns>
        public static bool Add<T>(string sql, object obj, string connString)
        {
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    var result = conn.Execute(sql, obj);
                    if (result > 0)
                        return true;
                    return false;
                }
            }
            catch (Exception e) { return false; }
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="obj">匿名函数new{}</param>
        /// <param name="connString">链接字符串</param>
        /// <returns></returns>
        public static T GetModel<T>(string sql, object obj, string connString)
        {
            T t;
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    t = conn.Query<T>(sql, obj).FirstOrDefault();
                }
            }
            catch (Exception e) { t = default(T); }
            return t;
        }
        /// <summary>
        ///  更新
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="obj">匿名函数new{}</param>
        /// <param name="connString">链接字符串</param>
        /// <returns></returns>
        public static bool Update(string sql, object obj, string connString)
        {
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    var result = conn.Execute(sql, obj);
                    if (result > 0)
                        return true;
                    return false;
                }
            }
            catch { return false; }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="obj">匿名函数new{}</param>
        /// <param name="connString">链接字符串</param>
        /// <returns></returns>
        public static bool Delete(string sql, object obj, string connString)
        {
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connString))
                {
                    var result = conn.Execute(sql, obj);
                    if (result > 0)
                        return true;
                    return false;
                }
            }
            catch { return false; }
        }
    }
}