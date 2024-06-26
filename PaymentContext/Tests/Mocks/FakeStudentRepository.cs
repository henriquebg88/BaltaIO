using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks;

[TestClass]
public class FakeStudentRepository : IStudentRepository
{
    public void CreateSubscription(Student student)
    {
        
    }

    public bool ExistsDocument(string document)
    {
        if(document == "99999999999") return true;
        return false;
    }

    public bool ExistsEmail(string email)
    {
        if(email == "teste@teste.com") return true;
        return false;
    }

    public void ShouldReturErrorWhenDocumentExists(){
        
    }
}