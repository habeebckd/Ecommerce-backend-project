﻿namespace E_Commerce.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? _User { get; set; }
        public virtual ICollection<CartItems> _Items { get; set; }
    }
}
