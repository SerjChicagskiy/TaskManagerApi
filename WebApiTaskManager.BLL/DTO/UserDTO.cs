using System;
using System.Collections.Generic;

namespace WebApiTaskManager.BLL.DTO
{
    public class UserDTO
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Lastname {get;set;}
        public string Email {get;set;}
        public DateTime Birthday {get;set;}
        public string Phone {get;set;}
        public string PhotoPath {get;set;}
        public string Login {get;set;}
        public string Token {get;set;}
        public string Password {get;set;}
        public ICollection<UserRoleDTO> UserRoles {get;set;} = new List<UserRoleDTO>();
    }
}