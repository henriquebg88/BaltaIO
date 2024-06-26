using Flunt.Validations;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, int number, string neighbborhood, string city, string state, string country, string zipCode)
        {
            Street = street;
            Number = number;
            Neighbborhood = neighbborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(Street, 3, "Address.Street", "A rua deve conter pelo menos 3 caracteres.")
            );
        }

        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Neighbborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}