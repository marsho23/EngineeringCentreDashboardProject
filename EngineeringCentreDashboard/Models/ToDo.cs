////using System.ComponentModel.DataAnnotations;

////namespace EngineeringCentreDashboard.Models
////{
////    public class ToDo
////    {
////        //public int Id { get; set; }
////        //[Required]
////        //public string Title { get; set; }
////        //public string Description { get; set; }
////        //[Required]
////        //public DateTime DueDate { get; set; }
////        //public bool IsCompleted { get; set; }

////        //public ToDo(int id, string title, string description, DateTime dueDate)
////        //{
////        //    Id = id;
////        //    Title = title;
////        //    Description = description;
////        //    DueDate = dueDate;
////        //    IsCompleted = false;
////        //}

////        //public ToDo(int id, string title, string description, DateTime dueDate, bool isCompleted)
////        //{
////        //    Id = id;
////        //    Title = title;
////        //    Description = description;
////        //    DueDate = dueDate;
////        //    IsCompleted = isCompleted;
////        //}

////        public int Id { get; set; }
////        [Required]
////        public string Title { get; set; }
////        public string Description { get; set; }
////        [Required]
////        public DateTime DueDate { get; set; }
////        public bool IsCompleted { get; set; }

////        [Required]
////        public int UserLoginId { get; set; }

////        public UserLogin UserLogin { get; set; }

////        public ToDo(int id, string title, string description, DateTime dueDate, int userLoginId)
////        {
////            Id = id;
////            Title = title;
////            Description = description;
////            DueDate = dueDate;
////            IsCompleted = false;
////            UserLoginId = userLoginId;
////        }

////        public ToDo(int id, string title, string description, DateTime dueDate, bool isCompleted, int userLoginId)
////        {
////            Id = id;
////            Title = title;
////            Description = description;
////            DueDate = dueDate;
////            IsCompleted = isCompleted;
////            UserLoginId = userLoginId;
////        }

////    }
////}

//using System.ComponentModel.DataAnnotations;

//namespace EngineeringCentreDashboard.Models
//{
//    public class ToDo
//    {
//        public int Id { get; set; }

//        [Required]
//        public string Title { get; set; }

//        public string Description { get; set; }

//        [Required]
//        public DateTime DueDate { get; set; }

//        public bool IsCompleted { get; set; }

//        public int UserLoginId { get; set; }

//        public UserLogin UserLogin { get; set; }

//        public ToDo()
//        {
//        }

//        public ToDo(int id, string title, string description, DateTime dueDate, int userLoginId)
//        {
//            Id = id;
//            Title = title;
//            Description = description;
//            DueDate = dueDate;
//            IsCompleted = false;
//            UserLoginId = userLoginId;
//        }


//        public ToDo(int id, string title, string description, DateTime dueDate, bool isCompleted, int userLoginId)
//        {
//            Id = id;
//            Title = title;
//            Description = description;
//            DueDate = dueDate;
//            IsCompleted = isCompleted;
//            UserLoginId = userLoginId;
//        }
//    }
//}

using EngineeringCentreDashboard.Models.Request;
using ServiceStack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineeringCentreDashboard.Models
{
    public class ToDo
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        //[Required]
        public int? UserLoginId { get; set; }

        public UserLogin UserLogin { get; set; }

        public ToDo()
        {
        }

        public ToDo(ToDoRequest toDoRequest)
        {
            this.Id = toDoRequest.Id.ToLong();
            this.Title = toDoRequest.Title;     
            this.Description = toDoRequest.Description; 
            this.DueDate = toDoRequest.DueDate;
            this.UserLoginId = toDoRequest.UserLoginId;
            this.IsCompleted = toDoRequest.IsCompleted;
        }
        public ToDo(int id, string title, string description, DateTime dueDate, int userLoginId,bool isCompleted=false)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
            UserLoginId = userLoginId;
        }


        //public ToDo(Guid id, string title, string description, DateTime dueDate, bool isCompleted, int userLoginId)
        //{
        //    Id = id.ToString();
        //    Title = title;
        //    Description = description;
        //    DueDate = dueDate;
        //    IsCompleted = isCompleted;
        //    UserLoginId = userLoginId;
        //}
    }
}
