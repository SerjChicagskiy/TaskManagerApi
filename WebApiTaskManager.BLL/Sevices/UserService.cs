using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApiTaskManager.BLL.Comunication;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.DAL.Context;
using WebApiTaskManager.DAL.Repositories;

namespace WebApiTaskManager.BLL.Sevices
{
    public class UserService:IServices<UserDTO,UserResponse>
    {
        UserRepository userRepository;
        IMapper mapper{get;set;}
        UnitOfWork unitOfWork;
        
        public UserService(UnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork=unitOfWork;
            userRepository=unitOfWork.UserRepository;
            this.mapper=mapper;
        }

        public async Task<UserResponse> AddAsync(UserDTO userDTO)
        {
            try 
            {
                var user=mapper.Map<UserDTO,User>(userDTO);
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();
                return new UserResponse(userDTO);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Save user error: {ex.Message}");
            }
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindByIdAsync(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            try 
            {
                userRepository.Delete(existingUser);
                await unitOfWork.CompleteAsync();
                var existingUserDTO=mapper.Map<User,UserDTO>(existingUser);
                return new UserResponse(existingUserDTO);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when deleting user: {ex.Message}");
            }
        }
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users=await userRepository.GetAllAsync(); 
            var usersDTO=mapper.Map<IEnumerable<User>,IEnumerable<UserDTO>>(users)
            .Select(x=>{
                x.Password=null;
                return x;
            });
            return usersDTO;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user=await userRepository.GetByIdAsync(id);
            var userDTO=mapper.Map<User,UserDTO>(user);
            return userDTO;
        }

         public async Task<UserResponse> UpdateAsync(int id, UserDTO userDTO)
        {
            var user= await userRepository.GetByIdAsync(id);

            if (user == null)
                return new UserResponse("User not found");
            
            user.Name=userDTO.Name;
            user.Lastname=userDTO.Lastname;
            user.Login=userDTO.Login;
            user.Phone=userDTO.Phone;
            user.PhotoPath=userDTO.PhotoPath;
            user.Birthday=userDTO.Birthday;
            user.Email=userDTO.Email;
            user.Password=userDTO.Password;

            try
            {
                userRepository.Update(user);
                await unitOfWork.CompleteAsync();

                return new UserResponse(userDTO);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when updating task: {ex.Message}");
            }
        }
    }
}