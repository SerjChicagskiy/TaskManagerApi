using System.Threading.Tasks;
using WebApiTaskManager.BLL.Comunication;

namespace WebApiTaskManager.BLL.Sevices
{
    public interface IAuthenticationService
    {
         Task<UserResponse> AuthenticateAsync(string login, string password);
    }
}