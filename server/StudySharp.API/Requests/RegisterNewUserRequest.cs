namespace StudySharp.API.Requests
{
    public sealed class RegisterNewUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
