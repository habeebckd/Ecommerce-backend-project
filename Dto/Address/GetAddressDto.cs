﻿namespace E_Commerce.Dto.Address
{
    public class GetAddressDto
    {
        public int? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? HomeAddress { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PostalCode { get; set; }
    }
}
