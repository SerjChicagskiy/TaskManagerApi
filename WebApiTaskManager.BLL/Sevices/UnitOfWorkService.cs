
namespace WebApiTaskManager.BLL.Sevices
{
     public class UnitOfWorkService
    {
        public TaskService TaskService { get; set; }
        public PriorityService PriorityService { get; set; }
        public UserService UserService { get; set; }
        public UnitOfWorkService(TaskService TaskService,
                                    PriorityService PriorityService,
                                        UserService UserService)
        {
            this.TaskService = TaskService;
            this.PriorityService = PriorityService;
            this.UserService=UserService;
        }
    }
}