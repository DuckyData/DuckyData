using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace DuckyData1._0._0Alpha.Service.EmailService
{
    public class AppEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }

        public Task SendActivationAsync(string dest, string callbackUrl)
        {
            string domain = System.Environment.UserDomainName;
            SmtpClient client = new SmtpClient();
            string body = "<table border=0 cellspacing=0 cellpadding=0 style=max-width:600px>" +
"<tbody><tr><td><table bgcolor=#26c6da width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #e0e0e0;border-bottom:0;border-top-left-radius:3px;border-top-right-radius:3px>" +
"<tbody><tr><td height=22px colspan=3>&nbsp;</td></tr><tr><td width=32px></td><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:24px;color:#ffffff;padding-left:15px;line-height:1.25>DuckyDate Account Activation</td>" +
"<td width=42px></td></tr><tr><td height=18px colspan=3></td></tr></tbody></table></td></tr><tr style=padding:15px; bgcolor=#FAFAFA ><td><table bgcolor=#FAFAFA width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #f0f0f0;border-bottom:1px solid #c0c0c0;border-top:0;border-bottom-left-radius:3px;border-bottom-right-radius:3px>" +
"<tbody><tr height=16px style=15px;><td width=32px rowspan=3></td><td></td><td width=32px rowspan=3></td></tr><tr><td><table style=min-width:300px padding:15px; border=0 cellspacing=0 cellpadding=0><tbody><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Hi there,</td>" +
"</tr><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>You are receiving this email because we got a account activation request from:" +
"<b> " + dest + " </b><br><br>To activate your account,please click the link below<br><br> This message comes from an unmonitored mailbox.Please do not reply to this message.<br><br></td></tr><tr><td>" +
"<a href=" + callbackUrl + " target=\"_blank\"> " + callbackUrl + " </a></td></tr>" +
"<tr height=32px></tr><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Cheers,<br>DuckyData Customer Support.</td>" +
"</tr><trheight=16px></tr></tbody></table></td></tr><tr height=32px></tr></tbody></table></td></tr></tbody></table>";
            MailMessage mail = new MailMessage("duckydata@gmail.com", dest, "Activate Account", body);
            mail.IsBodyHtml = true;
            return client.SendMailAsync(mail);
        }

        public Task SendResetPasswordAsync(string dest, string callbackUrl)
        {
            string domain = HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
            SmtpClient client = new SmtpClient();
            string body = "<table border=0 cellspacing=0 cellpadding=0 style=max-width:600px>" +
"<tbody><tr><td><table bgcolor=#26c6da width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #e0e0e0;border-bottom:0;border-top-left-radius:3px;border-top-right-radius:3px>" +
"<tbody><tr><td height=22px colspan=3>&nbsp;</td></tr><tr><td width=32px></td><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:24px;color:#ffffff;padding-left:15px;line-height:1.25>DuckyDate Reset Password</td>" +
"<td width=42px></td></tr><tr><td height=18px colspan=3></td></tr></tbody></table></td></tr><tr style=padding:15px; bgcolor=#FAFAFA ><td><table bgcolor=#FAFAFA width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #f0f0f0;border-bottom:1px solid #c0c0c0;border-top:0;border-bottom-left-radius:3px;border-bottom-right-radius:3px>" +
"<tbody><tr height=16px style=15px;><td width=32px rowspan=3></td><td></td><td width=32px rowspan=3></td></tr><tr><td><table style=min-width:300px padding:15px; border=0 cellspacing=0 cellpadding=0><tbody><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Hi there,</td>" +
"</tr><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>You are receiving this email because we got a reset password request from:" +
"<b> " + dest + " </b><br><br>To reset your password, please click the link below<br><br> This message comes from an unmonitored mailbox.Please do not reply to this message.<br><br></td></tr><tr><td>" +
"<a href=" + callbackUrl + " target=\"_blank\">" + callbackUrl + "</a></td></tr>" +
"<tr height=32px></tr><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Cheers,<br>DuckyData Customer Support.</td>" +
"</tr><trheight=16px></tr></tbody></table></td></tr><tr height=32px></tr></tbody></table></td></tr></tbody></table>";

            MailMessage mail = new MailMessage("duckydata@gmail.com", dest, "Reset Password", body);
            mail.IsBodyHtml = true;
            return client.SendMailAsync(mail);
        }

        public Task SendBugReportCreated(string dest)
        {
            string domain = HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
            SmtpClient client = new SmtpClient();
            string body = 
                "<table border=0 cellspacing=0 cellpadding=0 style=max-width:600px>" +
                    "<tbody>" + 
                        "<tr>" +
                            "<td>" +
                                "<table bgcolor=#26c6da width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #e0e0e0;border-bottom:0;border-top-left-radius:3px;border-top-right-radius:3px>" +
                                    "<tbody>" +
                                        "<tr>" +
                                            "<td height=22px colspan=3>&nbsp;</td></tr><tr><td width=32px></td><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:24px;color:#ffffff;padding-left:15px;line-height:1.25>DuckyDate Reset Password</td>" +
                                            "<td width=42px></td></tr><tr><td height=18px colspan=3></td></tr></tbody></table></td></tr><tr style=padding:15px; bgcolor=#FAFAFA ><td><table bgcolor=#FAFAFA width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #f0f0f0;border-bottom:1px solid #c0c0c0;border-top:0;border-bottom-left-radius:3px;border-bottom-right-radius:3px>" +
                                            "<tbody><tr height=16px style=15px;><td width=32px rowspan=3></td><td></td><td width=32px rowspan=3></td></tr><tr><td><table style=min-width:300px padding:15px; border=0 cellspacing=0 cellpadding=0><tbody><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Hi there,</td>" +
                                        "</tr>" + 
                                        "<tr>" +
                                            "<td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Thanks for submitting a bug report " +
                                            "<b> " + dest + " </b>.<br><br><br><br> This message is a confirmation that we have received your feedback and are looking into it. Please do not reply to this message.<br><br></td></tr><tr><td>" +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr height=32px></tr><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Cheers,<br>DuckyData Customer Support.</td>" +
                                        "</tr>" +
                                        "<trheight=16px></tr>" +
                                    "</tbody>" +
                                "</table>" + 
                            "</td>" +
                        "</tr><tr height=32px></tr>" +
                    "</tbody>" + 
                "</table>";

            MailMessage mail = new MailMessage("duckydata@gmail.com", dest, "Thanks for submitting a bug report.", body);
            mail.IsBodyHtml = true;
            return client.SendMailAsync(mail);
        }

        public Task SendEditBugReportStatus(string dest)
        {
            string domain = HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
            SmtpClient client = new SmtpClient();
            string body =
                "<table border=0 cellspacing=0 cellpadding=0 style=max-width:600px>" +
                    "<tbody>" +
                        "<tr>" +
                            "<td>" +
                                "<table bgcolor=#26c6da width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #e0e0e0;border-bottom:0;border-top-left-radius:3px;border-top-right-radius:3px>" +
                                    "<tbody>" +
                                        "<tr>" +
                                            "<td height=22px colspan=3>&nbsp;</td></tr><tr><td width=32px></td><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:24px;color:#ffffff;padding-left:15px;line-height:1.25>" +
                                                "Bug Report Fixed" +
                                            "</td>" +
                                            "<td width=42px></td></tr><tr><td height=18px colspan=3></td></tr></tbody></table></td></tr><tr style=padding:15px; bgcolor=#FAFAFA ><td><table bgcolor=#FAFAFA width=100% border=0 cellspacing=0 cellpadding=0 style=min-width:332px;max-width:600px;border:1px solid #f0f0f0;border-bottom:1px solid #c0c0c0;border-top:0;border-bottom-left-radius:3px;border-bottom-right-radius:3px>" +
                                            "<tbody><tr height=16px style=15px;><td width=32px rowspan=3></td><td></td><td width=32px rowspan=3></td></tr><tr><td><table style=min-width:300px padding:15px; border=0 cellspacing=0 cellpadding=0><tbody><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Hi there,</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>" +
                                                "Thanks for a reported us a bug " +
                                            "<b> " + dest + " </b>.<br><br><br><br> " +
                                                "We send this email to inform you that we have fixed the bug you reported, sorry for the inconvenient.<br><br></td></tr><tr><td>" +
                                            "</td>" +
                                        "</tr>" +
                                        "<tr height=32px></tr><tr><td style=font-family:Roboto-Regular,Helvetica,Arial,sans-serif;font-size:13px;color:#202020;line-height:1.5>Cheers,<br>DuckyData Customer Support.</td>" +
                                        "</tr>" +
                                        "<trheight=16px></tr>" +
                                    "</tbody>" +
                                "</table>" +
                            "</td>" +
                        "</tr><tr height=32px></tr>" +
                    "</tbody>" +
                "</table>";

            MailMessage mail = new MailMessage("duckydata@gmail.com", dest, "Bug fixed confirmation.", body);
            mail.IsBodyHtml = true;
            return client.SendMailAsync(mail);
        }
    }
}