
using System.Reflection.Metadata;
using PaymentContext.Domain.ValueObjects;
using Document = PaymentContext.Domain.ValueObjects.Document;

namespace PaymentContext.Domain.Entities
{
    public class PaypalPayment : Payment
    {
        public PaypalPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Address address, Document document, string owner, Email email) : base(paidDate, expireDate, total, totalPaid, address, document, owner, email)
        {
        }

        public string TransactionCode { get; private set; }
    }
}