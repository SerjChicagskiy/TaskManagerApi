using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApiTaskManager.BLL.Comunication;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.DAL.Context;
using WebApiTaskManager.DAL.Repositories;

namespace WebApiTaskManager.BLL.Sevices
{
    public class RoleService : IServices<RoleDTO, RoleResponse>
    {
        RoleRepository roleRepository;
        IMapper mapper{get;set;}
        UnitOfWork unitOfWork;
        
        public RoleService(UnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork=unitOfWork;
            roleRepository=unitOfWork.RoleRepository;
            this.mapper=mapper;
        }
        public Task<RoleResponse> AddAsync(RoleDTO entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<RoleResponse> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            var roles=await roleRepository. GetAllAsync(); 
            var rolesDTO=mapper.Map<IEnumerable<Role>,IEnumerable<RoleDTO>>(roles);
            return rolesDTO;
        }

        public Task<RoleDTO> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<RoleResponse> UpdateAsync(int id, RoleDTO entity)
        {
            throw new System.NotImplementedException();
        }
    }
}