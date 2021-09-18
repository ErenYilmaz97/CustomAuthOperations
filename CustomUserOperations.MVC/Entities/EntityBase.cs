using System;

namespace CustomUserOperations.MVC.Entities
{
    public class EntityBase<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }


        public EntityBase()
        {
            Created = DateTime.Now;;
        }
    }
}