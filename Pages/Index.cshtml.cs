using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CQRSApplication.Pages
{
    public class IndexModel : PageModel
    {
        public string? Message { get; set; }
        public void OnGet()
        {
            Message = "Hello World!! This is my First Project";
        }
    }
}
