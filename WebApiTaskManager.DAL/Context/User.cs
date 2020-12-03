using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTaskManager.DAL.Context
{
    public class User
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Lastname {get;set;}
        public string Email {get;set;}
        public DateTime Birthday {get;set;}
        public string Phone {get;set;}
        public string PhotoPath {get;set;}
        public string Login {get;set;}
        public string Password {get;set;}
        [NotMapped]
        public string Token {get;set;}
        public ICollection<UserRole> UserRoles {get;set;} = new List<UserRole>();
    }
}