using System.ComponentModel.DataAnnotations;

namespace EngineeringCentreDashboard.Models.Request
{
    public class UserLoginRequest
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

        public UserLoginRequest()
        {
                
        }

        public UserLoginRequest(UserLogin userLogin)
        {
            this.Id = userLogin.Id;
            this.Username = userLogin.Username; 
            this.Password = userLogin.Password; 
            this.Email = userLogin.Email;   
        }

    }
}
