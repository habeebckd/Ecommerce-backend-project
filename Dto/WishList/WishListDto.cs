using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dto.WishList
{
    public class WishListDto
    {
        [Required]
        public int pro_id {  get; set; }
        
        [Required]
        public int user_id {  get; set; }
    }
}
