using Microsoft.AspNetCore.Identity;

namespace StudySharp.DomainServices
{
    public sealed class ApplicationUser : IdentityUser<int>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
