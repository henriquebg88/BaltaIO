namespace PaymentContext.Domain.Entities
{
    public class Student
    {
        private IList<Subscription> _subscriptions;
        public Student(string firstName, string lastName, string document, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }

        // Evitar que use diretamente metodos por fora da classe como .add do List, 
        // não passando pela regra de negocio
        public IReadOnlyCollection<Subscription> Subscriptions { get => _subscriptions.ToArray(); }
        public string Address { get; private set; }
        
        public void AddSubscription(Subscription subscription){
            /// Cancelar todas as assinaturas existentes e manter a nova como ativa
            foreach (var sub in Subscriptions)
            {
                sub.Inactivate();
            }

            _subscriptions.Add(subscription);
        }
    }
}