using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace DavidCommon
{
    public class IPCommon
    {
        /// <summary>
        /// 判断所给IP是否在IP范围内
        /// </summary>
        /// <param name="strStartIP">起始IP地址</param>
        /// <param name="strEndIP">结束IP地址</param>
        /// <param name="strHostIP">要判断的IP地址</param>
        /// <returns>在IP范围内返回true，否则返回false</returns>
        public static bool IsInIPRange(string strStartIP, string strEndIP, string strHostIP)
        {
            if (strStartIP == null || strStartIP == "" || strEndIP == null || strEndIP == "" || strHostIP == null || strHostIP == "")
            {
                return false;
            }
            //以前的方法，这样比较IP是错误的 yhl20100708
            //long iStartIP = (long)IPAddress.Parse(strStartIP).Address;
            //long iEndIP = (long)IPAddress.Parse(strEndIP).Address;
            //long iHostIP = (long)IPAddress.Parse(strHostIP).Address;

            long iStartIP = GetIPValue(strStartIP);
            long iEndIP = GetIPValue(strEndIP);
            long iHostIP = GetIPValue(strHostIP);
            if (iHostIP >= iStartIP && iHostIP <= iEndIP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 计算出IP具体的值
        /// </summary>
        /// <param name="strHostIP"></param>
        /// <returns></returns>
        private static long GetIPValue(string strHostIP)
        {
            long nRetValue = 0;
            long nCurValue = 0;

            try
            {
                IPAddress userIP = System.Net.IPAddress.Parse(strHostIP);

                Byte[] bytes = userIP.GetAddressBytes();
                for (int i = 0; i < bytes.Length; i++)
                {
                    nCurValue = bytes[i];
                    if (i == 0)
                        nCurValue = nCurValue << 24;
                    else
                        if (i == 1)
                        nCurValue = nCurValue << 16;
                    else
                            if (i == 2)
                        nCurValue = nCurValue << 8;

                    nRetValue += nCurValue;
                }
            }
            catch
            {
            }

            return nRetValue;
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["IP"] != null)
            {
                result = HttpContext.Current.Session["IP"].ToString();
            }
            else
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (null == result || result == String.Empty)
                {
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (null == result || result == String.Empty)
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }

                if (null == result || result == String.Empty || !IsIP(result))
                {
                    return "0.0.0.0";
                }
            }
            return result;
        }

        public static string GetIP(HttpRequestBase request)
        {
            string result = String.Empty;

            result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = request.UserHostAddress;
            }

            if (null == result || result == String.Empty || !IsIP(result))
            {
                return "0.0.0.0";
            }

            return result;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
