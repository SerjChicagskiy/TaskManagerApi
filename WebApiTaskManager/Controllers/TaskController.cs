using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.BLL.Extension;
using WebApiTaskManager.BLL.Sevices;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebApiTaskManager.Controllers
{
    
    [Authorize(Roles="User,Admin")]
    [ApiController]
    public class TaskController:ControllerBase
    {
        
        TaskService taskService;
        public TaskController(TaskService taskService)
        {
            this.taskService=taskService;
        }


        [Route("api/{controller}")]
        public async Task<IEnumerable<TaskReminderDTO>> GetAllTasksAsync()
        {
            return await taskService.GetAllAsync();            
        }


        [Route("api/{controller}/Page/{page}/Qt/{num}")]
        public IEnumerable<TaskReminderDTO> GetTasksAsync(int page,int num)
        {
            return taskService.FindByPage(page,num);          
        }


        [Route("api/{controller}/{id}")]
        public async Task <IActionResult> GetTasksByIdAsync(int id)
        {
            var taskReminder =await taskService.GetByIdAsync(id);
            if (taskReminder == null)
                return BadRequest("Task by id not found"); 
            return Ok(taskReminder);
        }
        

        [Route("api/{controller}/searchByTitle/{title}")]
        public async Task<IActionResult> GetFindByTitleAsync(string title)
        {
            var taskReminder=await taskService.FindByTitleAsync(title);
            if (taskReminder.Count() == 0)
                return BadRequest("Task(s) by title not found"); 
            return Ok(taskReminder);
        }


        [Route("api/{controller}/searchByDate/{date}")]
        public async Task<IActionResult> GetFindByDate(string date)
        {
            var taskReminder=await taskService.FindByDateAsync(date);
            if (taskReminder == null)
                return BadRequest("Date ERROR! Date format example: \"01.01.2001\""); 
            if(taskReminder.Count() == 0)
                return BadRequest("Task(s) by date not found"); 
            return Ok(taskReminder);
        }


        [Route("api/{controller}/searchByDateRange/{dateFrom}/{dateTo}")]
        public async Task<IActionResult> GetFindByDateRangeAsync(string datefrom,string dateTo)
        {
            var taskReminder=await taskService.FindByDateRangeAsync(datefrom,dateTo);
            if (taskReminder == null)
                return BadRequest("Date ERROR! Date format example: \"01.01.2001\""); 
            if(taskReminder.Count() == 0)
                return BadRequest("Task(s) in range dates not found");
            return Ok(taskReminder);
        }

        
        [Route("api/{controller}/searchArhiveByDateRange/{dateFrom}/{dateTo}")]
        public async Task<IActionResult> GetFindArhivByDateRangeAsync(string datefrom,string dateTo)
        {
            var taskReminder=await taskService.FindArhivByDateRangeAsync(datefrom,dateTo);
            if (taskReminder == null)
                return BadRequest("Date ERROR! Date format example: \"01.01.2001\""); 
            if(taskReminder.Count() == 0)
                return BadRequest("Arhiv task(s) in range dates not found");
            return Ok(taskReminder);
        }


        [HttpPut]
        [Route("api/{controller}/IsDone/{id}")]
        public async Task<IActionResult> PutTaskIsDone(int id)
        {
            var result = await taskService.TaskIsDoneAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPut]
        [Route("api/{controller}/setPriority/{idTask}")]
        public async Task<IActionResult> PutSetTaskPriority(int idTask,PriorityDTO priority)
        {
            var result = await taskService.SetTaskPriorityAsync(idTask,priority);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPut]
        [Route("api/{controller}/SetDate/{id}")]
        public async Task<IActionResult> PutSetDateAsync(int id,TaskReminderDTO taskReminderDTO)
        {
            var result = await taskService.SetDateAsync(id,taskReminderDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPost]
        [Route("api/{controller}")]
        public async Task <IActionResult> PostAddTaskAsync(TaskReminderDTO taskReminderDTO)
        {
             if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var result = await taskService.AddAsync(taskReminderDTO);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.TaskReminderDTO);
        }


        [HttpDelete("api/{controller}/{id}")]
        public async Task<IActionResult> DeleteTaskAsync(int id)
        {
            var result = await taskService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        
        [HttpPut("api/{controller}/ToArhive/{id}")]
        public async Task<IActionResult> PutTaskToArhiveAsync(int id)
        {
            var result = await taskService.ToArhiveAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPut("api/{controller}/{id}")]
        public async Task<IActionResult> PutEditTaskAsync(int id, TaskReminderDTO taskReminderDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await taskService.UpdateAsync(id, taskReminderDTO);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.TaskReminderDTO);
        }
    }
}