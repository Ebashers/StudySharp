using System.Collections.Generic;

namespace StudySharp.API.Responses.Auth
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
