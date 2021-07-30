using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudySharp.DomainServices.JwtService;
using StudySharp.Shared.Constants;
using System;

namespace StudySharp.Teacher.Pages.Teacher
{
    public class IndexModel : PageModel
    {
        private readonly IJwtAuthManager _jwtAuthManager;
        public IndexModel(IJwtAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }
        public void OnGet([FromQuery] string token)
        {
            var res = _jwtAuthManager.DecodeJwtToken(token);
            var it1 = res.Item1;
            var it2 = res.Item2;
            if (it1.IsInRole(DomainRoles.Teacher.ToString()))
            {
                Console.WriteLine("Done");
            }
        }
    }
}
