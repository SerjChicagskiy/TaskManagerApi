using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApiTaskManager.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}