using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.DAL.Repositories
{
    public class PriorityRepository:GenericRepository<Priority>
    {
        public PriorityRepository(DbContext context):base(context)
        {
        }
        public async Task <IEnumerable<Priority>> GetAllAsync()
        {
            return await context.Set<Priority>().ToListAsync();
        }
         public async Task<Priority> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        
        }
    }
}