﻿namespace Application.Model.Respone.ResponseAccount
{
    public class ResponseAccountAdmin
    {

        public Guid AccountId { get; set; }
        public Guid AdminId { get; set; }
        public string UserName { get; set; } 
        public string Role { get; set; } 
        public string FullName { get; set; } 
        public string Phone { get; set; } 
        public string Address { get; set; }
        public string Email { get; set; } 
        public string Status { get; set; }
    }
}
