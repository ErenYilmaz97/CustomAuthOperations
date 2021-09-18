using CustomUserOperations.MVC.Entities;

namespace CustomUserOperations.MVC.Helpers
{
    public interface IEmailHelper
    {
        public bool SendConfirmAccountMail(ApplicationUser user);
        public bool SendResetPasswordMail(ApplicationUser user);
    }
}