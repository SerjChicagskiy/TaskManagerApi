using System.Threading.Tasks;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.DAL.Repositories
{
    public class UnitOfWork
    {
        public TaskRepository TaskRepository { get; private set; }
        public PriorityRepository PriorityRepository { get; private set; }
        public UserRepository UserRepository { get; private set; }
        public RoleRepository RoleRepository { get; private set; }

        private readonly TaskManagerDbContext context;
        public UnitOfWork(TaskManagerDbContext context)
        {
            this.context = context;
            TaskRepository = new TaskRepository(context);
            PriorityRepository = new PriorityRepository(context);
            UserRepository=new UserRepository(context);
            RoleRepository=new RoleRepository(context);
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}