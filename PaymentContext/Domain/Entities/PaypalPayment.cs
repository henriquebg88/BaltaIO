
namespace PaymentContext.Domain.Entities
{
    public class PaypalPayment : Payment
    {
        public PaypalPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string address, string document, string owner, string email) : base(paidDate, expireDate, total, totalPaid, address, document, owner, email)
        {
        }

        public string TransactionCode { get; private set; }
    }
}