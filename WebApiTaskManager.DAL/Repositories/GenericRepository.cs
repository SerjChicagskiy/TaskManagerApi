using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiTaskManager.DAL.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, new() 
    {
        protected DbContext context;
        protected DbSet<T> dbSet;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
       
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

         public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        
       
        ~GenericRepository()
        {
            context.Dispose();
        }

    }
}