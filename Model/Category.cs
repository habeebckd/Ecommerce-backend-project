namespace E_Commerce.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string ?CategoryName {  get; set; }
        public virtual ICollection<Product>?_Products { get; set; }    
    }
}
