using WebApiTaskManager.DAL.Context;
using WebApiTaskManager.BLL.DTO;

namespace WebApiTaskManager.BLL.Comunication
{
    public class UserResponse: BaseResponse
    {
        public UserDTO User {get;set;}
        public UserResponse(bool success, string message, UserDTO user):base(success, message)
        {
            User = user;
        }

        public UserResponse(UserDTO user): this(true, string.Empty, user){}
        public UserResponse(string message): this(false, message, null) {}
    }
}