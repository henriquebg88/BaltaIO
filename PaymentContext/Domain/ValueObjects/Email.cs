
using Flunt.Validations;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract().Requires().IsEmail(Address, "Email.Address", "Email inválido"));
        }

        public string Address { get; private set; }

        public static implicit operator Email(string v)
        {
            throw new NotImplementedException();
        }
    }
}