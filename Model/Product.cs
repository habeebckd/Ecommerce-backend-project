namespace E_Commerce.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string ?ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrize { get; set; }
        public decimal offerPrice { get; set; }
        public decimal Rating { get; set; }
        public string ?ImageUrl { get; set; }
        public int StockId { get; set; }

        public int CategoryId { get; set; }


        public virtual  Category ?_Category { get; set; }
        public virtual ICollection<CartItems>? _CartItems { get; set; }
    }
}
