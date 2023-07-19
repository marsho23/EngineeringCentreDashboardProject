using System.ComponentModel.DataAnnotations;

namespace EngineeringCentreDashboard.Models.Request
{
    public class ToDoRequest
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        //[Required]
        public int? UserLoginId { get; set; }

        public ToDoRequest(ToDo toDo)
        {
            this.Id = toDo.Id;
            this.DueDate = toDo.DueDate;
            this.Title = toDo.Title;
            this.Description = toDo.Description;
            this.IsCompleted = toDo.IsCompleted;
            this.UserLoginId = toDo.UserLoginId;
        }

        public ToDoRequest()
        {
            
        }
    }
}
