using Microsoft.AspNetCore.Identity.UI.Services;

namespace TeamBugBusters.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Email code
            return Task.CompletedTask;
        }
    }
}
