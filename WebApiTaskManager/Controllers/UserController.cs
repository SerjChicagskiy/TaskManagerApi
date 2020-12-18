using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.BLL.Extension;
using WebApiTaskManager.BLL.Sevices;

namespace WebApiTaskManager.Controllers
{
    //[Authorize (Roles="Admin")]
    [ApiController]
    public class UserController: Controller
    {
        private readonly UserService userService;
        private readonly UserRoleService userRoleService;
        private readonly RoleService roleService;
        public UserController(UserService userService, UserRoleService userRoleService,RoleService roleService)
        {
            this.userService = userService;
            this.userRoleService=userRoleService;
            this.roleService=roleService;
        }

        [HttpGet]
        [Route("api/{controller}")]
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await userService.GetAllAsync();            
        }

        [HttpPost]
        [Route("api/{controller}")]
        public async Task<IActionResult> PostAddUserAsync([FromBody]UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var result = await userService.AddAsync(userDTO);
            var roles= await roleService.GetAllAsync();
            if(result.Success)
            {
                var user=await userService.FindByLoginAsync(result.User.Login);
                result=await userRoleService.SetUserRoleAsync(user.User.Id, roles.FirstOrDefault(x=>x.Name=="User").Id);
            }
            else
                return BadRequest(result);
            return Ok(result.User);
        }

        [HttpPut("api/{controller}/{id}")]
        public async Task<IActionResult> PutAsync(int id, UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await userService.UpdateAsync(id, userDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.User);
        }


        [HttpDelete("api/{controller}/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await userService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [Route("api/{controller}/{id}")]
        public async Task <IActionResult> GetUserByIdAsync(int id)
        {
            var user =await userService.GetByIdAsync(id);
            if (user == null)
                return BadRequest("Task by id not found"); 
            return Ok(user);
        }
    }
}