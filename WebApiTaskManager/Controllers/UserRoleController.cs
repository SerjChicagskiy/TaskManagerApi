using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.BLL.Extension;
using WebApiTaskManager.BLL.Sevices;

namespace WebApiTaskManager.Controllers
{
    //[Authorize (Roles="admin")]
    [ApiController]
    [Route("/api/[controller]")]
    public class UserRoleController: ControllerBase
    {
        private readonly UserRoleService userRoleService;
        public UserRoleController(UserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }

        [HttpPost]
        [Route("setrole")]
        public async Task<IActionResult> SetRole([FromBody]UserRoleDTO userRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userResponse = await userRoleService.SetUserRoleAsync(userRoleDTO.UserId, userRoleDTO.RoleId);
            return Ok(userResponse);
        }

        [HttpDelete]
        [Route("deleterole/{id}")]
        public async Task<IActionResult> DeleteRole(int id, [FromBody]UserRoleDTO userRoleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userResponse = await userRoleService.DeleteRoleAsync(id, userRoleDTO.RoleId);
            return Ok(userResponse);
        }
    }
}