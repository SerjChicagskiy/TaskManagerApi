using System.Collections.Generic;

namespace WebApiTaskManager.DAL.Context
{
    public class Priority
    {
        public int PriorityId{get;set;}
        public string PriorityName{get;set;}
        public IList<TaskReminder> Tasks{get;set;}=new List<TaskReminder>();
    }
}