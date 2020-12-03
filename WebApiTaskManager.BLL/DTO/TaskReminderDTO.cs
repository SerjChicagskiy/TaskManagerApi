using System;
using WebApiTaskManager.DAL.Context;

namespace WebApiTaskManager.BLL.DTO
{
    public class TaskReminderDTO
    {
        public int TaskId{get;set;}
        public string Title{get;set;}
        public string Description{get;set;}
        public int PriorityId{get;set;}
        public DateTime DateTime{get;set;}=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
        public bool IsDone{get;set;}
        public bool IsArhive{get;set;}
        public PriorityDTO Priority{get;set;}
    }
}