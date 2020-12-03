using System;
namespace WebApiTaskManager.DAL.Context
{
    public class TaskReminder
    {
        public int TaskId{get;set;}
        public string Title{get;set;}
        public string Description{get;set;}
        public int PriorityId{get;set;}
        public DateTime DateTime{get;set;}
        public bool IsDone{get;set;}
        public bool IsArhive{get;set;}
        public Priority Priority{get;set;}
    }
}