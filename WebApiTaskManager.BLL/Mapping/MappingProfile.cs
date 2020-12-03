using System.Collections.Generic;
using AutoMapper;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.BLL.Mapping
{
     public class MappingProfile : Profile {
     public MappingProfile() {
         CreateMap<TaskReminder, TaskReminderDTO>();
         CreateMap<TaskReminderDTO, TaskReminder>();
         CreateMap<Priority, PriorityDTO>();
         CreateMap<PriorityDTO, Priority>();
         CreateMap<User, UserDTO>();
         CreateMap<UserDTO, User>();
         CreateMap<Role, RoleDTO>();
         CreateMap<RoleDTO, Role>();
         CreateMap<UserRole, UserRoleDTO>();
         CreateMap<UserRoleDTO, UserRole>();
     }
 }
}