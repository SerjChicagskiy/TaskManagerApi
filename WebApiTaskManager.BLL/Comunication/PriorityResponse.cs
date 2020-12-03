using WebApiTaskManager.BLL.DTO;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.BLL.Comunication
{
    public class PriorityResponse:BaseResponse
    {
        public PriorityDTO Priority {get;set;}
        public PriorityResponse(bool success, string message, PriorityDTO priority):base(success, message)
        {
            Priority = priority;
        }

        public PriorityResponse(PriorityDTO priority): this(true, string.Empty, priority){}
        public PriorityResponse(string message): this(false, message, null) {}
    }
}