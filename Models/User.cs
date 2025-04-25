﻿namespace MiPrimerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Avatar { get; set; }
    }

    public class UserResponse
    {
        public List<User> Data { get; set; }
    }
}
