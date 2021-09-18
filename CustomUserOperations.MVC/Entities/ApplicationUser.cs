using System.Collections.Generic;

namespace CustomUserOperations.MVC.Entities
{
    public class ApplicationUser : EntityBase<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        //NAV PROPS
        public virtual ConfirmEmailOperation ConfirmEmailOperation { get; set; }
        public virtual IList<ResetPasswordOperation> ResetPasswordOperations { get; set; }


        public ApplicationUser():base()
        {
            
        }
    }
}