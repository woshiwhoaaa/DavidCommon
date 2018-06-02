using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace DavidCommon
{
    public class MailCommon
    {
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="FromAddress">发送账号</param>
        /// <param name="pwd">密码</param>
        /// <param name="host">用于SMTP事务的主机名或ip地址</param>
        /// <param name="FromDisplayName">发送人昵称</param>
        /// <param name="ToAddress">接收地址</param>
        /// <param name="Title">标题</param>
        /// <param name="Content">内容</param>
        /// <param name="IsHtml">是否以Html格式发送</param>
        /// <returns></returns>
        public static bool SendMailM(string FromAddress, string pwd, string host, string FromDisplayName, string ToAddress, string Title, string Content, bool IsHtml)
        {
            try
            {
                MailAddress from = new MailAddress(FromAddress, FromDisplayName, System.Text.Encoding.GetEncoding("gb2312"));
                MailAddress to = new MailAddress(ToAddress);
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = Title;
                msg.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");//标题所使用的编码集
                msg.Body = Content;
                msg.IsBodyHtml = IsHtml;//设置正文是否为html格式的值
                msg.Priority = MailPriority.High;//设置此邮件具有高优先级
                SmtpClient smtp = new SmtpClient();//允许应用程序使用SMTP发邮件
                smtp.Credentials = new System.Net.NetworkCredential(FromAddress, pwd);//设置验证发件人的凭据（邮件服务器需要身份验证）
                smtp.Host = host;//设置用于SMTP事务的主机名或ip地址。例如smtp.163.com
                //smtp.EnableSsl = true; //是否使用SSL加密连接（有的服务器不支持此链接）
                try
                {
                    smtp.Send(msg);//发信
                    msg.Dispose();//释放有MailMessage使用的所有资源
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
