﻿namespace E_Commerce.Dto.user
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Role { get; set; }
        public bool isBlocked { get; set; }
    }
}
