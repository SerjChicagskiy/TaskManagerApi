using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.DAL.Repositories
{
    public class RoleRepository:GenericRepository<Role>
    {
        public RoleRepository(DbContext context):base(context)
        {
        }
        public async Task <IEnumerable<Role>> GetAllAsync()
        {
            return await dbSet.Include(x=>x.UserRoles).ToListAsync();
        }
    }
}