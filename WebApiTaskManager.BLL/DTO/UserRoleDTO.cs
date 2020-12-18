namespace WebApiTaskManager.BLL.DTO
{
    public class UserRoleDTO
    {
        public int UserId {get;set;}
        public int RoleId {get;set;}
        public RoleDTO Role {get;set;}
        public UserDTO User {get;set;}
    }
}