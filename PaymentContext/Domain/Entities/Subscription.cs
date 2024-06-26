using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDte)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDte = expireDte;
            Active = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDte { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get => _payments.ToArray(); }

        public void AddPayment(Payment payment){

            AddNotifications(
                new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura.")
            );

            if(Valid) _payments.Add(payment);
        }

        public void Activate(){
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Inactivate(){
            Active = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}