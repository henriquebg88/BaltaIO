using Flunt.Validations;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "O Nome deve conter pelo menos 3 caracteres.")
                .HasMinLen(LastName, 3, "Name.LastName", "O Sobrenome deve conter pelo menos 3 caracteres.")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "O Nome deve conter no máximo 40 caracteres.")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}