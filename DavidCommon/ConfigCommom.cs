using System.Configuration;

namespace DavidCommon
{
    public class ConfigCommom
    {
        /// <summary>
        /// 获取appsetting字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingsValue(string key)
        {
            ConfigurationManager.RefreshSection("appSettings");
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnSettingsValue(string key)
        {
            ConfigurationManager.RefreshSection("connectionStrings");
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[key];
            return settings.ConnectionString;
        }
    }
}
