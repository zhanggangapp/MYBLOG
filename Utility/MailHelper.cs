// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MailHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   邮件发送助手类  0.10
//   Verion:0.10
//   Description:通过SMTP发送邮件
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Utility
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// 邮件发送助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:56 Created By iceStone
    ///  
    /// 1. 注册完成过后拿到用户邮箱
    //2. 生成一个随机的字符串GUID
    //3. 将这个用户的ID和GUID保存到验证码表中
    //4. 将这个GUID拼接成一个链接地址 http://itcast.cn/user_auth.ashx?token=[GUID]，发送到用户的邮箱
    //5. 在处理程序接收客户端传递过来的token，根据token查到是哪个用户在激活邮箱
    /// </remarks>
    public static class MailHelper
    {
        private readonly static string SmtpServer = "smtp.qq.com";
        private readonly static int SmtpServerPort = 25;
        private readonly static bool SmtpEnableSsl = false;
        private readonly static string SmtpUsername = "175582488@qq.com";
        private readonly static string SmtpDisplayName = "测试邮箱1111";
        private readonly static string SmtpPassword = "xxx";

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:55 Crea
        /// </remarks>
        /// <param name="to">收件人地址</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容(支持HTML)</param>
        /// <param name="copyTos">抄送地址列表</param>
        /// <returns>是否发送成功</returns>
        public static bool Send(string to, string subject, string mailBody, params string[] copyTos)
        {
            return Send(new[] { to }, subject, mailBody, copyTos, new string[] { }, MailPriority.Normal);
        }

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:55 Created By iceStone
        /// </remarks>
        /// <param name="tos">收件人地址列表</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容(支持HTML)</param>
        /// <param name="ccs">抄送地址列表</param>
        /// <param name="bccs">密件抄送地址列表</param>
        /// <param name="priority">此邮件的优先级</param>
        /// <param name="attachments">附件列表</param>
        /// <returns>是否发送成功</returns>
        /// <exception cref="System.ArgumentNullException">attachments</exception>
        public static bool Send(string[] tos, string subject, string mailBody, string[] ccs, string[] bccs, MailPriority priority, params Attachment[] attachments)
        {
            if (attachments == null) throw new ArgumentNullException("attachments");
            if (tos.Length == 0) return false;
            //创建Email实体
            var message = new MailMessage();
            message.From = new MailAddress(SmtpUsername, SmtpDisplayName);
            message.Subject = subject;
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = priority;
            //插入附件
            foreach (var attachment in attachments)
            {
                message.Attachments.Add(attachment);
            }
            //插入收件人地址,抄送地址和密件抄送地址
            foreach (var to in tos.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.To.Add(new MailAddress(to));
            }
            foreach (var cc in ccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(cc));
            }
            foreach (var bcc in bccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(bcc));
            }
            //创建SMTP客户端
            var client = new SmtpClient
            {
                Host = SmtpServer,
                Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = SmtpEnableSsl,
                Port = SmtpServerPort
            };
            //client.SendCompleted += Client_SendCompleted;
            //try
            //{
            //发送邮件
            client.Send(message);
            //client.SendAsync(message,DateTime.Now.ToString());

            //client.Dispose();
            //message.Dispose();
            return true;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}
