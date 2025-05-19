using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dto.user
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(25,ErrorMessage ="Name Should Not Execeed 30 Character")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [MinLength(8,ErrorMessage = "Password must be at least 8 characters")]
        public string Passoword { get; set; }


    }
}
