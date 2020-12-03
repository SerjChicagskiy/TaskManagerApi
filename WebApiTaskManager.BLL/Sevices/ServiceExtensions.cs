using WebApiTaskManager.DAL.Context;
using WebApiTaskManager.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace WebApiTaskManager.BLL.Sevices
{
   public static class ServiceExtensions
    {
        public static void AddMyServices(this IServiceCollection services,string connectionString)
        { 
            services.AddDbContext<TaskManagerDbContext>(options=>options.UseSqlServer(connectionString));
            
            services.AddTransient<UnitOfWork,UnitOfWork>();
            services.AddTransient<TaskService,TaskService>();
            services.AddTransient<UserService,UserService>();
            services.AddTransient<UserRoleService,UserRoleService>();
            services.AddTransient<RoleService,RoleService>();
            services.AddTransient<UnitOfWorkService,UnitOfWorkService>();
            services.AddTransient<PriorityService,PriorityService>();
            services.AddTransient<IAuthenticationService,AuthenticationService>();
        }
    }
}