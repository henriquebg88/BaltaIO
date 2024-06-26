using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }

        // Evitar que use diretamente metodos por fora da classe como .add do List, 
        // não passando pela regra de negocio
        public IReadOnlyCollection<Subscription> Subscriptions { get => _subscriptions.ToArray(); }
        public Address Address { get; private set; }
        
        public void AddSubscription(Subscription subscription){
            
            var hasActiveSubscripton = false;

            foreach (var sub in Subscriptions)
                if(sub.Active) hasActiveSubscripton = true;

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasActiveSubscripton, "Student.Subscription", "Ja possui inscrição ativa")
                .IsGreaterThan(subscription.Payments.Count, 0, "Student.Subscription.Payments", "Náo possui pagamento" )    
            );

            _subscriptions.Add(subscription);

            // if(hasActiveSubscripton)
            //     AddNotification("Student.Subscriptions", "Voce já possui uma inscrição ativa.");
            
        }
    }
}