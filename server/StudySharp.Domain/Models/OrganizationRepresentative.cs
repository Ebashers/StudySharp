namespace StudySharp.Domain.Models
{
    public sealed class OrganizationRepresentative
    {
        public int OrganizationId { get; set; }
        public int MemberId { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool IsOrganizationManager { get; set; }
    }
}
