using DaLatBooking.Application.Services.Interface;

namespace DaLatBooking.Application.Services.Implementation
{
    public class EmailService : IEmailService
    {
        public Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
