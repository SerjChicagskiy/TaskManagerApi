using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTaskManager.BLL.Comunication;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.BLL.Sevices;

namespace WebApiTaskManager.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        
        private readonly IAuthenticationService authService;
        private readonly IMapper mapper;

        public AuthController(IAuthenticationService authService,
                              IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserDTO userDTO)
        {
            var authenticatedUser = await authService.AuthenticateAsync(userDTO.Login, userDTO.Password);
            if(authenticatedUser.User==null)
                return BadRequest(authenticatedUser);
            return Ok(authenticatedUser.User);
        }
    }
}