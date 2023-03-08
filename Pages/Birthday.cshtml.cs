using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventPlannerApp.Pages
{
    public class BirthdayModel : PageModel
    {
        private readonly ILogger<BirthdayModel> _logger;

        public BirthdayModel(ILogger<BirthdayModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
