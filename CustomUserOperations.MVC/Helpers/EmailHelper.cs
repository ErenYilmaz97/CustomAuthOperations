using System.Net;
using System.Net.Mail;
using CustomUserOperations.MVC.Entities;

namespace CustomUserOperations.MVC.Helpers
{
    public class EmailHelper : IEmailHelper
    {

        private SmtpClient _client;

        public EmailHelper()
        {
            _client = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("erenyilmazgazi@hotmail.com", "03123897461eren"),
                Port = 587,
                Host = "smtp.live.com",
                EnableSsl = true
            };
        }


        public bool SendConfirmAccountMail(ApplicationUser user)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("erenyilmazgazi@hotmail.com"),
                Subject = "Hesap Onayı",
                Body =
                    $"<h4>Merhaba {user.Name} {user.LastName},</h4>"+
                    $"<h4>Hesabınız Başarıyla Oluşturuldu. Giriş Yapabilmek İçin Mail Adresinizi Onaylamanız Gerekmektedir.</h4>" +
                    $"<h4>Mail Adresinizi Onaylamak İçin Aşağıdaki Linke Tıklayınız.</h4>" +
                    $"link",

                IsBodyHtml = true

            };

            mail.To.Add(user.Email);

            try
            {
                _client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendResetPasswordMail(ApplicationUser user)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("erenyilmazgazi@hotmail.com"),
                Subject = "Hesap Onayı",
                Body =
                    $"Merhaba {user.Name} {user.LastName},"+
                    $"<h3>Şifrenizi Aşağıdaki Linkten Sıfırlayabilirsiniz.</h3> link",
                IsBodyHtml = true
            };

            mail.To.Add(user.Email);


            try
            {
                _client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}