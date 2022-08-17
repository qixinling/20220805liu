using MimeKit;
using MailKit.Net.Smtp;
using Server.Models.DataBaseModels;
using Server.Models;
using System;
using Server.Logs;

namespace Server.Utils.Email_Utils
{
	public static class EmailUtils
	{
        private static string _username = "KYCD5";  //邮箱昵称
        private static string _account = "k5group@163.com"; //邮箱账号
        private static string _password = "QQWEBQPDDDBWDXEL";   //邮箱密码
        private static string _host = "smtp.163.com";   //smtp服务器

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="toAddress">收件人邮箱</param>
        /// <param name="nickname">收件人昵称</param>
        /// <returns></returns>
        public static bool SendCode(string toAddress, string nickname)
        {
            bool done = false;
            try
            {
                using DbConnect dbConnect = DbConnectUtils.GetDbContext();

                Random rnd = new Random();
                int code = rnd.Next(1000, 9999);

                string title = "Welcome to KYCD5";
                //string msg = "温馨提醒：您的验证码为" + code + "，为了您的权益，请勿向他人透漏验证码";
                string msg = string.Format("Your TAC no. is （{0}） for kycd5 on {1}", code, DateTime.Now);

                done = Send(toAddress, nickname, title, msg);

                if (done)
                {
                    DbCheckcode cc = new DbCheckcode();
                    cc.Code = code.ToString();
                    cc.Usertel = toAddress;
                    cc.Cdate = DateTime.Now;
                    dbConnect.DbCheckcode.Add(cc);
                    dbConnect.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                done = false;
                NLogHelper._.Error("Email发送验证码出错", ex);
            }
            return done;
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="toAddress">收件人地址</param>
        /// <param name="nickname">收件人昵称</param>
        /// <param name="title">邮件标题</param>
        /// <param name="content">邮件内容</param>
        public static bool Send(string toAddress, string nickname, string title, string content)
		{
            bool done = false;
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_username, _account));
                message.To.Add(new MailboxAddress(nickname, toAddress));

                message.Subject = title;

                message.Body = new TextPart("plain") { Text = content };

                //如果你要发送的 Body 内容是 HTML 的话，你可以使用下面这种：
                /*var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = @"<b>This is bold and this is <i>italic</i></b>";
                message.Body = bodyBuilder.ToMessageBody();*/

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(_host);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_account, _password);

                    client.Send(message);
                    client.Disconnect(true);
                }

                done = true;
            }
            catch (Exception ex)
            {
                done = false;
                NLogHelper._.Error("发送Email出错", ex);
            }
            return done;
		}
	}
}
