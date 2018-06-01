using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DavidCommon
{
    public class StringCommon
    {
        #region 字符串处理
        /// <summary>
        /// 字符串过滤HTML标记
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string FilterHTML(string Htmlstring)
        {
            string pattern = "http://([^\\s]+)\".+?span.+?\\[(.+?)\\].+?>(.+?)<";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string CutString(string str, int length)
        {
            try
            {
                if (str.Length > length)
                {
                    return str.Substring(0, length);
                }
            }
            catch (Exception) { }
            return str;
        }
        /// <summary>
        /// 截取字符串后在后面或者前面添加其他字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="otherstr">后缀或者字符串,type=0后缀，其他是前缀</param>
        /// <param name="type">默认0后缀，其他是前缀</param>
        /// <returns></returns>
        public static string CutString(string str, int length, string otherstr, int type = 0)
        {
            try
            {
                if (str.Length > length)
                { str = str.Substring(0, length); }
                if (type == 0)
                { str = string.Format("{0}{1}", str, otherstr); }
                else
                { str = string.Format("{0}{1}", otherstr, str); }
            }
            catch (Exception ex) { }
            return str;
        }
        #endregion

        #region 编码解码
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code">utf-8、gbk、gb2312</param>
        /// <returns></returns>
        public static string GetEncode(string str, string code)
        {
            try
            {
                return System.Web.HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding(code));
            }
            catch (Exception ex) { return str; }
        }
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code">utf-8、gbk、gb2312</param>
        /// <returns></returns>
        public static string GetDecode(string str, string code)
        {
            try
            {
                return System.Web.HttpUtility.UrlDecode(str, System.Text.Encoding.GetEncoding(code));
            }
            catch (Exception ex) { return str; }
        }
        #endregion

        #region 加密解密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            try
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
            catch (Exception ex) { return ""; }
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="textData">字符串</param>
        /// <param name="Encryptionkey">秘钥key</param>
        /// <returns></returns>
        public static string EncryptData(string textData, string Encryptionkey)
        {
            try
            {
                RijndaelManaged objrij = new RijndaelManaged();
                objrij.Mode = CipherMode.CBC;
                objrij.Padding = PaddingMode.PKCS7;
                objrij.KeySize = 0x80;
                objrij.BlockSize = 0x80;
                byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
                byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);

                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;
                ICryptoTransform objtransform = objrij.CreateEncryptor();
                byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
                return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
            }
            catch (Exception)
            {

                return textData;
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="EncryptedText">字符串</param>
        /// <param name="Encryptionkey">秘钥key</param>
        /// <returns></returns>
        public static string DecryptData(string EncryptedText, string Encryptionkey)
        {
            try
            {
                RijndaelManaged objrij = new RijndaelManaged();
                objrij.Mode = CipherMode.CBC;
                objrij.Padding = PaddingMode.PKCS7;
                objrij.KeySize = 0x80;
                objrij.BlockSize = 0x80;
                byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
                byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
                byte[] EncryptionkeyBytes = new byte[0x10];
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);
                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;
                byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
                return Encoding.UTF8.GetString(TextByte);
            }
            catch (Exception)
            {

                return EncryptedText;
            }
        }
        #endregion
    }
}
