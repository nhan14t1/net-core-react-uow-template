using System.ComponentModel.DataAnnotations;

namespace R.Entities.Entities.InputModels
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
