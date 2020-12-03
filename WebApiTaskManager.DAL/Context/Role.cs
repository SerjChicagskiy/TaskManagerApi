using System.Collections.Generic;

namespace WebApiTaskManager.DAL.Context
{
     public class Role
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public ICollection<UserRole> UserRoles {get;set;} = new List<UserRole>();
    }
}