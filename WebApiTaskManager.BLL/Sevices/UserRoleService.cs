using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTaskManager.BLL.Comunication;
using WebApiTaskManager.DAL.Context;
using WebApiTaskManager.DAL.Repositories;
using AutoMapper;
using WebApiTaskManager.BLL.DTO;

namespace WebApiTaskManager.BLL.Sevices
{
    public class UserRoleService
    {
        
        
        IMapper mapper{get;set;}
        private readonly RoleRepository roleRepository;
        private readonly UserRepository userRepository;
        private readonly UnitOfWork unitOfWork;
        public UserRoleService(UnitOfWork unitOfWork,
                               IMapper mapper)
        { 
            this.unitOfWork = unitOfWork;
            this.roleRepository = unitOfWork.RoleRepository;
            this.userRepository = unitOfWork.UserRepository;
           
            this.mapper=mapper;
        }
        public async Task<UserResponse> DeleteRoleAsync(int userId, int roleId)
        {
            try 
            {
                User user=await userRepository.GetByIdAsync(userId);
                user.UserRoles.Remove(user.UserRoles.SingleOrDefault(x=>x.RoleId==roleId));
                await unitOfWork.CompleteAsync();
                var userDTO=mapper.Map<User,UserDTO>(user);
                return new UserResponse(userDTO);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when deleting the role: {ex.Message}");
            }
        }
        public async Task<IEnumerable<User>> ListUsersByRoleAsync(int roleId)
        {
            var users = await userRepository.GetAllAsync();
            var usersInRole = users.Where(x => x.UserRoles.Contains(x.UserRoles.SingleOrDefault(y => y.RoleId == roleId)));
            return usersInRole;
        }
        public async Task<UserResponse> SetUserRoleAsync(int userId, int roleId)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(userId);
                user.UserRoles.Add(new UserRole {UserId = userId, RoleId = roleId});
                await unitOfWork.CompleteAsync();
                var userDTO=mapper.Map<User,UserDTO>(user);
                userDTO.Password=null;
                return new UserResponse(userDTO);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when setting the role: {ex.Message}");
            }
        }
    }
}