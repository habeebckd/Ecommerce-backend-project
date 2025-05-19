using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dto.product
{
    public class ProductWithCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string ?ProductName {  get; set; }
        [Required]
        public string ?ProductDescription {  get; set; }
        [Required]
        public decimal ProductPrize {  get; set; }
        [Required]
        public decimal offerPrice {  get; set; }
        [Required]
        public decimal Rating {  get; set; }
        [Required]
        [Url]
        public string ?ImageUrl { get; set; }

        [Required]
        public int StockId { get; set; }
        public string? CategoryName { get; set; }
    }
}
