namespace WebApiTaskManager.BLL.DTO
{
    public class UserRoleDTO
    {
        public int UserId {get;set;}
        public int RoleId {get;set;}
        public RoleDTO RoleDTO {get;set;}
        public UserDTO UserDTO {get;set;}
    }
}