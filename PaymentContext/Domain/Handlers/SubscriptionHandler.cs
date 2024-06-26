using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailServices _EmailService;
        public SubscriptionHandler(IStudentRepository StudentRepository, IEmailServices EmailService)
        {
            this._studentRepository = StudentRepository;
            this._EmailService = EmailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fast fail validation
            command.Validate();
            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            } 

            // Verifiar se Docmento esta cadastrado
            if(_studentRepository.ExistsDocument(command.Document)) AddNotification("Document", "Este CPF ja está em uso.");

            // Verificar se email esta cadastrado
            if(_studentRepository.ExistsEmail(command.Email)) AddNotification("Email", "Este email ja está em uso.");

            // Gerar VOs
            var name = new Name(command.FirstName, command.LastName);
            var documento = new Document(command.Document, command.DocumentType);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.AddressNumber, command.Neighbborhood, command.City, command.State, command.Country, command.ZipCode);


            // Gerar entidades
            var student = new Student(name, documento, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, address, documento, command.Payer, email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Aplicar validações
            AddNotifications(name, documento, email, address, student, subscription, payment);

            //Checar validações
            if(Invalid) return new CommandResult(false, "Náo foi possível realizar sua assinatura");

            // Salvar as informações
            _studentRepository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _EmailService.Send(student.Name, student.Email, "Incrição realizada", );

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}