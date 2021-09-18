using System;
using System.Reflection.Metadata.Ecma335;

namespace CustomUserOperations.MVC.Entities
{
    public class ResetPasswordOperation : EntityBase<int>
    {
        public string ResetPasswordToken { get; set; }
        public int ApplicationUserId { get; set; }
        public DateTime ValidityTime { get; set; }
        public bool isValid { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }


        public ResetPasswordOperation() : base()
        {
            isValid = true;
        }
    }
}