using DaLatBooking.Domain.Entities;
using Stripe.Checkout;

namespace DaLatBooking.Application.Services.Interface
{
    public interface IPaymentService
    {
        SessionCreateOptions CreateStripeSessionOptions(Booking booking, Villa villa, string domain);
        Session CreateStripeSession(SessionCreateOptions options);

    }
}
