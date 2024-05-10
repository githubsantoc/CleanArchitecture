using Domains.Entities;
using Infrastructure.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CQRSApplication.Pages
{
    public class ContactModel(IConfiguration configuration) : PageModel
    {
        [BindProperty]
        public string? ContactDetails { get; set; }
        private readonly IConfiguration _configuration = configuration;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(Contact contact)
        {
            if(ModelState.IsValid is false)
            {
                return Page();
            }

            MailServices mailService = new MailServices(_configuration);
            await mailService.SendEmailAsync(contact.Email, contact.Subject, contact.Comment);
            return RedirectToPage("/Index");
        }
    }
}
