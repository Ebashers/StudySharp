namespace StudySharp.Shared.Constants
{
    public static class AuthorizationPolicies
    {
        public static string TeacherPolicy = $"{nameof(DomainRoles.Teacher)}Policy";
        public static string StudentPolicy = $"{nameof(DomainRoles.Student)}Policy";
    }
}
