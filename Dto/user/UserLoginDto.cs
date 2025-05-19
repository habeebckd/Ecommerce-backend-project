using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dto.user
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string ?UserEmail {  get; set; }
        
        
        [Required]
        public string ?Passoword {  get; set; }
    }
}
