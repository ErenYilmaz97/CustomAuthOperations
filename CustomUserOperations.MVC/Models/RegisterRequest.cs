﻿namespace CustomUserOperations.MVC.Models
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}