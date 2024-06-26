namespace PaymentContext.Domain.Services
{
    public interface IEmailServices
    {
        public void Send(string to, string email, string subject, string body);
    } 
}