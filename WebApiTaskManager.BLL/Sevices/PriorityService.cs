using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.BLL.Comunication;
using WebApiTaskManager.DAL.Context;
using WebApiTaskManager.DAL.Repositories;

namespace WebApiTaskManager.BLL.Sevices
{
    public class PriorityService:IServices<PriorityDTO,PriorityResponse>
    {
        PriorityRepository priorityRepository;
        IMapper mapper{get;set;}
        UnitOfWork unitOfWork;
        public PriorityService(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork=unitOfWork;
            this.priorityRepository=unitOfWork.PriorityRepository;
            this.mapper=mapper;
        }

        public async Task<PriorityResponse> AddAsync(PriorityDTO priorityDTO)
        {
            try 
            {
                var priority=mapper.Map<PriorityDTO,Priority>(priorityDTO);
                await priorityRepository.AddAsync(priority);
                await unitOfWork.CompleteAsync();
                var existingPriorityDTO=mapper.Map<Priority,PriorityDTO>(priority);
                
                return new PriorityResponse(existingPriorityDTO);
            }
            catch (Exception ex)
            {
                return new PriorityResponse($"Save priorty error: {ex.Message}");
            }
        }

        public async Task<PriorityResponse> DeleteAsync(int id)
        {
            var existingPriority = await priorityRepository.GetByIdAsync(id);

            if (existingPriority == null)
                return new PriorityResponse("Priority not found");

            try 
            {
                priorityRepository.Delete(existingPriority);
                await unitOfWork.CompleteAsync();
                var existingPriorityDTO=mapper.Map<Priority,PriorityDTO>(existingPriority);
                return new PriorityResponse(existingPriorityDTO);
            }
            catch (Exception ex)
            {
                return new PriorityResponse($"Error when deleting priority: {ex.Message}");
            }
        }

        public async Task<PriorityDTO> GetByIdAsync(int id)
        {
            var priority = await priorityRepository.GetByIdAsync(id);
            var priorityDTO=mapper.Map<Priority,PriorityDTO>(priority);
            return priorityDTO;
        }

        public async Task <IEnumerable<PriorityDTO>> GetAllAsync()
        {
            var prioritys = await priorityRepository.GetAllAsync();
            var priorityDTOs=mapper.Map<IEnumerable<Priority>,IEnumerable<PriorityDTO>>(prioritys);
            return priorityDTOs;
        }

        public async Task<PriorityResponse> UpdateAsync(int id,PriorityDTO priorityDTO)
        {
            var priority= await priorityRepository.GetByIdAsync(id);

            if (priority == null)
                return new PriorityResponse("Priority not found");
            
            priority.PriorityName=priorityDTO.PriorityName;

            try
            {
                priorityRepository.Update(priority);
                await unitOfWork.CompleteAsync();

                var existingPriorityDTO=mapper.Map<Priority,PriorityDTO>(priority);
                return new PriorityResponse(existingPriorityDTO);
            }
            catch (Exception ex)
            {
                return new PriorityResponse($"Error when updating priority: {ex.Message}");
            }
        }
    }
}