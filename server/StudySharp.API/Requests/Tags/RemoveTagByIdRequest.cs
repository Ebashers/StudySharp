using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.Tags
{
    public class RemoveTagByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}