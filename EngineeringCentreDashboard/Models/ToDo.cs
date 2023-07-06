using System.ComponentModel.DataAnnotations;

namespace EngineeringCentreDashboard.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public ToDo(int id, string title, string description, DateTime dueDate)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
        }

        public ToDo(int id, string title, string description, DateTime dueDate, bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = isCompleted;
        }
    }
}
