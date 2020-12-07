using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTaskManager.BLL.Comunication;
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
        private readonly RoleService roleService;
        public UserRoleController(UserRoleService userRoleService,RoleService roleService)
        {
            this.userRoleService = userRoleService;
            this.roleService=roleService;
        }

        public async Task<IEnumerable<RoleDTO>> GetAll()
        {
            return await roleService.GetAllAsync();
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