using System;
using System.Web;

namespace DavidCommon
{
    public class CookieCommon
    {
        /// <summary>
        /// 写cookie
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="key"></param>
        /// <param name="strValue"></param>
        /// <param name="expires"></param>
        public static void WriteCookie(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = HttpUtility.UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 读cookie
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());
            return "";
        }
        // <summary>  
        /// 删除Cookies  
        /// </summary>  
        /// <param name="cookieName"></param>  
        /// <param name="key"></param>  
        public static void RemoveCookie(string cookieName, string key)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key))
                        cookie.Values.Remove(key);
                    else
                        response.Cookies.Remove(cookieName);
                }
            }

        }
    }
}
