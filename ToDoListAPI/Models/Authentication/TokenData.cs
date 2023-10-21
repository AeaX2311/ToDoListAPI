﻿namespace ToDoListAPI.Models.Authentication
{
    public class TokenData
    {
        public bool HasError { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string InnerException { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
