using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApiTaskManager.BLL.Sevices
{
    public interface IServices<T1,T2>
    {
        Task<T1> GetByIdAsync(int id);
        Task <IEnumerable<T1>> GetAllAsync();
        Task<T2> AddAsync(T1 entity);
        Task<T2> UpdateAsync(int id,T1 entity);
        Task<T2> DeleteAsync(int id);
    }
}