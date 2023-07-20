using EngineeringCentreDashboard.Models.Request;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace EngineeringCentreDashboard.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<ToDo> ToDos { get; set; }

        public UserLogin()
        {
            ToDos = new List<ToDo>();
        }

        public UserLogin(UserLoginRequest userLoginRequest)
        {
            this.Id = userLoginRequest.Id;
            this.Username = userLoginRequest.Username;
            this.Password = userLoginRequest.Password;
            this.Email = userLoginRequest.Email;
        }

        public UserLogin(int id, string username, string password, string email)
        {
            this.Id = id;
            this.Username = username;    
            this.Password = password;
            this.Email = email;
            ToDos = new List<ToDo>();
        }
    }

    
}
