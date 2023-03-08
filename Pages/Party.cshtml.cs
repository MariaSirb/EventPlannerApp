using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventPlannerApp.Pages
{
    public class PartyModel : PageModel
    {
        private readonly ILogger<PartyModel> _logger;

        public PartyModel(ILogger<PartyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
