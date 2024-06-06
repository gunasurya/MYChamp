using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MYChamp.Pages.Consultee_Pages
{
    public class examModel : PageModel
    {
        // Property to store the current value
        [BindProperty]
        public int Value { get; set; }

        // Constructor to initialize the value
        public examModel()
        {
            // Initialize value to 0
            Value = 0;
        }

        // Method to handle incrementing the value
        public IActionResult OnPostIncrement()
        {
            Value++;
            return new JsonResult(new { value = Value, message = "Value incremented successfully." });
        }

        // Method to handle decrementing the value
        public IActionResult OnPostDecrement()
        {
            Value--;
            return new JsonResult(new { value = Value, message = "Value decremented successfully." });
        }

        // This method will be called when the page is requested via HTTP GET
        public void OnGet()
        {
            // You can perform additional logic here if needed
        }
    }
}
