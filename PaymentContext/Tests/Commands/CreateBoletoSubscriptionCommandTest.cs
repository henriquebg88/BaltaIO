using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace Tests;

[TestClass]
public class CreateBoletoSubscriptionCommandTest
{
    public void ShouldReturErrorWhenNameInvalid(){
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "";
        command.Validate();

        Assert.IsTrue(command.Invalid);
    }
}