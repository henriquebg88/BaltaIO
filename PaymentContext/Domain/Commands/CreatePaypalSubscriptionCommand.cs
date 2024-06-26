using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePaypalSubscriptionCommand : Notifiable, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string TransactionCode { get; set; }      
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string PayerEmail { get; set; }  
        public string Street { get; set; }
        public int AddressNumber { get; set; }
        public string Neighbborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType DocumentType { get; set; }
        public string Owner { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "O Nome deve conter pelo menos 3 caracteres.")
                .HasMinLen(LastName, 3, "Name.LastName", "O Sobrenome deve conter pelo menos 3 caracteres.")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "O Nome deve conter no máximo 40 caracteres.")
            );
        }
    }
}