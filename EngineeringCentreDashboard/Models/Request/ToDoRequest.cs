using System.ComponentModel.DataAnnotations;

namespace EngineeringCentreDashboard.Models.Request
{
    public class ToDoRequest
    {
        public string? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        //[Required]
        public int? UserLoginId { get; set; }
        //public string UserLoginEmail { get; set; }
        public ToDoRequest(ToDo toDo)
        {
            this.Id = toDo.Id.ToString();
            this.DueDate = toDo.DueDate;
            this.Title = toDo.Title;
            this.Description = toDo.Description;
            this.IsCompleted = toDo.IsCompleted;
            this.UserLoginId = toDo.UserLoginId;
            //this.UserLoginEmail = toDo.UserLoginEmail;
        }

        public ToDoRequest()
        {
            
        }
    }
}
