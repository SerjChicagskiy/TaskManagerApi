using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.DAL.Context;
using WebApiTaskManager.DAL.Repositories;
using AutoMapper;
using System;
using WebApiTaskManager.BLL.Comunication;
using LinqKit;

namespace WebApiTaskManager.BLL.Sevices
{
    public class TaskService:IServices<TaskReminderDTO,TaskResponse>
    {
        TaskRepository taskReminderRepository;
        PriorityRepository priorityRepository;
        IMapper mapper{get;set;}
        UnitOfWork unitOfWork;
        ExpressionStarter<TaskReminder> predicate;
        
        public TaskService(UnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork=unitOfWork;
            taskReminderRepository=unitOfWork.TaskRepository;
            priorityRepository=unitOfWork.PriorityRepository;
            this.mapper=mapper;
        }

        public async Task<TaskResponse> AddAsync(TaskReminderDTO taskReminderDTO)
        {
            try 
            {
                var taskReminder=mapper.Map<TaskReminderDTO,TaskReminder>(taskReminderDTO);
                await taskReminderRepository.AddAsync(taskReminder);
                await unitOfWork.CompleteAsync();
                var existingtaskReminderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(taskReminder);
                return new TaskResponse(existingtaskReminderDTO);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"Save task error: {ex.Message}");
            }
        }

        public async Task<TaskResponse> DeleteAsync(int id)
        {
            var existingTaskRemider = await taskReminderRepository.GetByIdAsync(id);

            if (existingTaskRemider == null)
                return new TaskResponse("Task not found");

            try 
            {
                taskReminderRepository.Delete(existingTaskRemider);
                await unitOfWork.CompleteAsync();
                var existingTaskRemiderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(existingTaskRemider);
                return new TaskResponse(existingTaskRemiderDTO);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"Error when deleting task: {ex.Message}");
            }
        }
        public async Task<TaskResponse> ToArhiveAsync(int id)
        {
            var existingTaskRemider = await taskReminderRepository.GetByIdAsync(id);

            if (existingTaskRemider == null)
                return new TaskResponse("Task not found");

            try 
            {
                existingTaskRemider.IsArhive=true;
                taskReminderRepository.Update(existingTaskRemider);
                await unitOfWork.CompleteAsync();

                var existingTaskRemiderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(existingTaskRemider);
                return new TaskResponse(existingTaskRemiderDTO);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"Error when move to arhive task: {ex.Message}");
            }
        }

        public async Task<TaskResponse> SetDateAsync(int id,TaskReminderDTO taskReminderDTO)
        {
            var existingTaskRemider = await taskReminderRepository.GetByIdAsync(id);

            if (existingTaskRemider == null)
                return new TaskResponse("Task not found");

            try 
            {
                existingTaskRemider.DateTime=taskReminderDTO.DateTime;
                taskReminderRepository.Update(existingTaskRemider);
                await unitOfWork.CompleteAsync();

                var existingTaskRemiderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(existingTaskRemider);
                return new TaskResponse(existingTaskRemiderDTO);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"Error when change date task: {ex.Message}");
            }
        }
        public async Task<TaskResponse> TaskIsDoneAsync(int id)
        {
            var existingTaskRemider = await taskReminderRepository.GetByIdAsync(id);

            if (existingTaskRemider == null)
                return new TaskResponse("Task not found");

            try 
            {
                existingTaskRemider.IsDone=true;
                taskReminderRepository.Update(existingTaskRemider);
                await unitOfWork.CompleteAsync();

                var existingTaskRemiderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(existingTaskRemider);
                return new TaskResponse(existingTaskRemiderDTO);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"Error whet set task as done: {ex.Message}");
            }
        }
        public async Task<TaskResponse> SetTaskPriorityAsync(int idTask, PriorityDTO priorityDTO)
        {
            var existingTaskRemider = await taskReminderRepository.GetByIdAsync(idTask);

            if (existingTaskRemider == null)
                return new TaskResponse("Task not found");

            try 
            {
                var priority=await priorityRepository.GetByIdAsync(priorityDTO.PriorityId);
                if(priority==null)
                    return new TaskResponse($"Error when set priority. Priority not found");
                existingTaskRemider.Priority=priority;
                existingTaskRemider.PriorityId=priority.PriorityId;
                taskReminderRepository.Update(existingTaskRemider);
                await unitOfWork.CompleteAsync();

                var existingTaskRemiderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(existingTaskRemider);
                return new TaskResponse(existingTaskRemiderDTO);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"Error when set priority to task: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TaskReminderDTO>> FindByTitleAsync(string title)
        {
            
            predicate = PredicateBuilder.New<TaskReminder>();
            var pred = PredicateBuilder.New<TaskReminder>();
            pred.And(x => x.Title == title);
            predicate.Extend(pred, PredicateOperator.Or);
            var taskRemiders = await taskReminderRepository.FindByPredicateASync(predicate);
            var taskReminderDTOs=mapper.Map<IEnumerable<TaskReminder>,IEnumerable<TaskReminderDTO>>(taskRemiders);
            
            return taskReminderDTOs;
        }
        public async Task<IEnumerable<TaskReminderDTO>> FindByDateAsync(string date)
        {
            
            IEnumerable<TaskReminderDTO> taskReminderDTOs=null;
            predicate = PredicateBuilder.New<TaskReminder>();
            var pred = PredicateBuilder.New<TaskReminder>();
            try
            {
                pred.And(x => x.DateTime == Convert.ToDateTime(date));
                predicate.Extend(pred, PredicateOperator.Or);
                var taskRemiders = await taskReminderRepository.FindByPredicateASync(predicate);
                taskReminderDTOs=mapper.Map<IEnumerable<TaskReminder>,IEnumerable<TaskReminderDTO>>(taskRemiders);
            }
            catch (System.Exception){}
            return taskReminderDTOs;
        }
        public async Task<IEnumerable<TaskReminderDTO>> FindByDateRangeAsync(string dateFrom,string dateTo)
        {
            
            IEnumerable<TaskReminderDTO> taskReminderDTOs=null;
            predicate = PredicateBuilder.New<TaskReminder>();
            var pred = PredicateBuilder.New<TaskReminder>();
            try
            {
                pred.And(x => x.DateTime >= Convert.ToDateTime(dateFrom));
                pred.And(x => x.DateTime <= Convert.ToDateTime(dateTo));
                predicate.Extend(pred, PredicateOperator.Or);
                var taskRemiders = await taskReminderRepository.FindByPredicateASync(predicate);
                taskReminderDTOs=mapper.Map<IEnumerable<TaskReminder>,IEnumerable<TaskReminderDTO>>(taskRemiders);
            }
            catch (System.Exception){}
            return taskReminderDTOs;
        }
        public async Task<IEnumerable<TaskReminderDTO>> FindArhivByDateRangeAsync(string dateFrom,string dateTo)
        {
            
            IEnumerable<TaskReminderDTO> taskReminderDTOs=null;
            predicate = PredicateBuilder.New<TaskReminder>();
            var pred = PredicateBuilder.New<TaskReminder>();
            try
            {
                pred.And(x => x.DateTime >= Convert.ToDateTime(dateFrom));
                pred.And(x => x.DateTime <= Convert.ToDateTime(dateTo));
                pred.And(x => x.IsArhive == true);
                predicate.Extend(pred, PredicateOperator.Or);
                var taskRemiders = await taskReminderRepository.FindByPredicateASync(predicate);
                taskReminderDTOs=mapper.Map<IEnumerable<TaskReminder>,IEnumerable<TaskReminderDTO>>(taskRemiders);
            }
            catch (System.Exception){}
            return taskReminderDTOs;
        }

        public async Task<TaskReminderDTO> GetByIdAsync(int id)
        {
            var taskReminder=await taskReminderRepository.GetByIdAsync(id);
            var taskReminderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(taskReminder);
            return taskReminderDTO;
        }

        public async Task<IEnumerable<TaskReminderDTO>> GetAllAsync()
        {
            var taskReminders=await taskReminderRepository.GetAllAsync(); 
            var taskRemindersDTO=mapper.Map<IEnumerable<TaskReminder>,IEnumerable<TaskReminderDTO>>(taskReminders);
            return taskRemindersDTO;
        }
        public IEnumerable<TaskReminderDTO> FindByPage(int page,int qt)
        {
            var taskReminders=taskReminderRepository.FindByPage(page,qt); 
            var taskRemindersDTO=mapper.Map<IEnumerable<TaskReminder>,IEnumerable<TaskReminderDTO>>(taskReminders);
            return taskRemindersDTO;
        }

        public async Task<TaskResponse> UpdateAsync(int id, TaskReminderDTO taskReminderDTO)
        {
            var taskReminder= await taskReminderRepository.GetByIdAsync(id);

            if (taskReminder == null)
                return new TaskResponse("Task not found");
            
            taskReminder.Title=taskReminderDTO.Title;
            taskReminder.Description=taskReminderDTO.Description;
            taskReminder.PriorityId=taskReminderDTO.PriorityId;

            try
            {
                taskReminderRepository.Update(taskReminder);
                await unitOfWork.CompleteAsync();

                var existingTaskRemiderDTO=mapper.Map<TaskReminder,TaskReminderDTO>(taskReminder);
                return new TaskResponse(existingTaskRemiderDTO);
            }
            catch (Exception ex)
            {
                return new TaskResponse($"Error when updating task: {ex.Message}");
            }
        }
    }
}