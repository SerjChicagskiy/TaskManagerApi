using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.DAL.Repositories
{
    public class UserRepository: GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }


      

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbSet.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                            .ToListAsync();
        }
        public async Task<User> FindByLoginAsync(string login)
        {
            return await dbSet.Include(x => x.UserRoles)
                                .ThenInclude(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Login == login);
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await dbSet.Include(x=>x.UserRoles)
                                .ThenInclude(x => x.Role)
                                    .FirstOrDefaultAsync(x=>x.Id==id);
        
        }
    }
}