using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Helpers
{
    public static class EmailMessages
    {
        public static EmailMessage ActivationAccountEmail(string email, string username, string callbackUrl)
         => new EmailMessage(
             Email: email,
             Subject: "MyVinted - activate your account",
             Message: $"<p>Hi <strong>{username}</strong>!</p>" +
                 $"<p>In order to activate your account on MyVinted, click link below.<br><br>" +
                 $"Activation account link: <a href='{callbackUrl}'>LINK</a></p>" +
                 "<p>Best regards,<br>" +
                 "MyVinted team</p>"
        );

        public static EmailMessage ResetPasswordEmail(string email, string username, string callbackUrl)
           => new EmailMessage(
               Email: email,
               Subject: "MyVinted - reset password",
               Message: $"<p>Hi <strong>{username}</strong>!</p>" +
                   $"<p>In order to reset your password on MyVinted, click link below.<br><br>" +
                   $"Reset password link: <a href='{callbackUrl}'>LINK</a></p>" +
                   "<p>Best regards,<br>" +
                   "MyVinted team</p>"
       );

        public static EmailMessage EmailChangeEmail(string email, string callbackUrl)
          => new EmailMessage(
              Email: email,
              Subject: "MyVinted - change email",
              Message: $"<p>Hi <strong>{email}</strong>!</p><br><br>" +
                  $"<p>In order to change your email on MyVinted, click link below.<br><br>" +
                  $"Change email link: <a href='{callbackUrl}'>LINK</a></p>" +
                  "<p>Best regards,<br>" +
                  "MyVinted team</p>"
       );
    }
}
