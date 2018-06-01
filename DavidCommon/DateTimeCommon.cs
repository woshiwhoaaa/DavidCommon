using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DavidCommon
{
    public class DateTimeCommon
    {
        /// <summary>
        /// 获取当前时间字符串,yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <returns></returns>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
