using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionHanlerTest
{
    public void ShouldReturErrorWhenDocumentExists(){
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService() );
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "Henrique";
        command.LastName = "Balduino Gonzaga";
        command.Document = "99999999999";
        command.Email = "henriquebg@gmail.com";
        command.BarCode ="123456789";
        command.BoletoNumber ="1234567";     
        command.PaymentNumber = "123121";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 60;
        command.TotalPaid = 60;
        command.PayerEmail = "hehehe@gmail.com";  
        command.Street = "rua do caraio";
        command.AddressNumber = 4;
        command.Neighbborhood = "Logo ali";
        command.City = "Brasilia";
        command.State = "DF";
        command.Country = "Brasil";
        command.ZipCode = "1111111";
        command.PayerDocument = "12345678911";
        command.DocumentType = Domain.Enums.EDocumentType.CPF;
        command.Payer = "Henrique Gonzaga";
        
        handler.Handle(command);

        Assert.IsFalse(handler.Valid);
    }
}