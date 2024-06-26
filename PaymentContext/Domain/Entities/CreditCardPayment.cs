
using System.Reflection.Metadata;
using PaymentContext.Domain.ValueObjects;
using Document = PaymentContext.Domain.ValueObjects.Document;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Address address, Document document, string owner, Email email) : base(paidDate, expireDate, total, totalPaid, address, document, owner, email)
        {
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}