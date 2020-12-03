using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.DAL.Repositories
{
    public class TaskRepository:GenericRepository<TaskReminder>
    {
        public TaskRepository(DbContext context):base(context)
        {
        }

        public async Task <IEnumerable<TaskReminder>> GetAllAsync()
        {
            return await dbSet.Include(x=>x.Priority).ToListAsync();
        }

        public async Task<TaskReminder> GetByIdAsync(int id)
        {
            return await dbSet.Include(x=>x.Priority).FirstOrDefaultAsync(x=>x.TaskId==id);
        
        }
        

        public async Task<IEnumerable<TaskReminder>> FindByPredicateASync(Expression<Func<TaskReminder, bool>> predicate)
        {
            return await dbSet.Where(predicate).Include(x=>x.Priority).ToListAsync();
        }

        public IEnumerable<TaskReminder> FindByPage(int page,int qt)
        {
            var result= dbSet.Include(x=>x.Priority).ToList()
                                    .Skip((page - 1) * qt)
                                        .Take(qt);
            return result;
        }

    }
}