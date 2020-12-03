using WebApiTaskManager.BLL.DTO;

namespace WebApiTaskManager.BLL.Comunication
{
    public class TaskResponse : BaseResponse
    {
        public TaskReminderDTO TaskReminderDTO {get;set;}
        public TaskResponse(bool success, string message, TaskReminderDTO taskReminderDTO):base(success, message)
        {
            TaskReminderDTO = taskReminderDTO;
        }

        public TaskResponse(TaskReminderDTO taskReminderDTO): this(true, string.Empty, taskReminderDTO){}
        public TaskResponse(string message): this(false, message, null) {}
    }
}