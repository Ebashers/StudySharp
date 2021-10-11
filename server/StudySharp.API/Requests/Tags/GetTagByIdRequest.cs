using Microsoft.AspNetCore.Mvc;

namespace StudySharp.API.Requests.Tags
{
    public class GetTagByIdRequest
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
    }
}