using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventPlannerApp.Pages
{
    public class CorporateEventModel : PageModel
    {
        private readonly ILogger<CorporateEventModel> _logger;

        public CorporateEventModel(ILogger<CorporateEventModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
