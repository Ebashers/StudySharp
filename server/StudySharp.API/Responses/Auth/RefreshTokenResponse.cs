namespace StudySharp.API.Responses.Auth
{
    public sealed class RefreshTokenResponse
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
