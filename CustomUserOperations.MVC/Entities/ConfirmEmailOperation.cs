using System;

namespace CustomUserOperations.MVC.Entities
{
    public class ConfirmEmailOperation : EntityBase<int>
    {
        public string ConfirmEmailToken { get; set; }
        public int ApplicationUserId { get; set; }
        public DateTime ValidityTime { get; set; }
        public bool isValid { get; set; }



        public virtual ApplicationUser ApplicationUser { get; set; }


        public ConfirmEmailOperation():base()
        {
            isValid = true;
        }
    }
}