using System.Collections.Generic;

namespace StudySharp.ApplicationServices.JwtAuthService.ResultModels
{
    public sealed class LoginResult
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
