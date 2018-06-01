using System;
using System.IO;
using System.Net;
using System.Text;

namespace DavidCommon
{
    public class BaseHttpClient
    {
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="interfaceUrl"></param>
        /// <param name="parameters"></param>
        /// <param name="cookie"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static string RequestGet(string url, string parameters, string sessionID = null)
        {
            HttpWebRequest request = WebRequest.Create(url + "?" + parameters) as HttpWebRequest;
            request.Method = "Get";
            //request.KeepAlive = true;
            //request.AllowAutoRedirect = false;
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            if (!string.IsNullOrEmpty(sessionID))
            {
                CookieContainer cc = new CookieContainer();
                Cookie cookie = new Cookie("SessionId", sessionID);
                cookie.Domain = "localhost";
                cc.Add(cookie);
                request.CookieContainer = cc;
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                return GetResponseContent((HttpWebResponse)ex.Response);
            }
            return GetResponseContent(response);
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="interfaceUrl"></param>
        /// <param name="parameters"></param>
        /// <param name="cookie"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static string RequestPost(string url, string parameters, string sessionID = null)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            //request.KeepAlive = true;
            //request.AllowAutoRedirect = false;
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] postdatabtyes = Encoding.UTF8.GetBytes(parameters);
            request.ContentLength = postdatabtyes.Length;
            if (!string.IsNullOrEmpty(sessionID))
            {
                CookieContainer cc = new CookieContainer();
                Cookie cookie = new Cookie("SessionId", sessionID);
                cookie.Domain = "localhost";
                cc.Add(cookie);
                request.CookieContainer = cc;
            
            }
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabtyes, 0, postdatabtyes.Length);
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                return GetResponseContent((HttpWebResponse)ex.Response);
            }

            return GetResponseContent(response);

        }

        /// <summary>
        /// 读取返回内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string GetResponseContent(HttpWebResponse response)
        {
            string retString = "";
            try
            { 
                Stream myResponseStream = response.GetResponseStream();
                using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
                {
                    retString = myStreamReader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
               
            }
            return retString;
        }
    }
}
