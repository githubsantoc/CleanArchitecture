

namespace Application.services
{
    public interface IMailServices
    {
        public Task SendEmailAsync(string receiver, string subject, string body);
    }
}
