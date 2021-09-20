﻿namespace StudySharp.ApplicationServices.JwtService
{
    public sealed class LoginResult
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
