using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace Tests;

[TestClass]
public class StudentTests
{
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests(){
        _subscription = new Subscription(null);

        _student = new Student(
            new Name(
                "Henrique",
                "Gonzaga"
            ),
            new Document("73794619153", PaymentContext.Domain.Enums.EDocumentType.CPF), 
            new Email("henriquebg88@gmail.com")
        );
        
    }


    [TestMethod]
    public void ActiveSubscription_Ativa_Erro()
    {
        var payment = new PaypalPayment(
            DateTime.Now, 
            DateTime.Now.AddDays(5), 
            10, 10,
            new Address("rua teste", 4, "teste", "cidade", "estado", "pais", "717171-000"),
            _student.Document,
            "Piroman",
            _student.Email 
        );

        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void AddSubscriptionSemPagamento_Erro()
    {
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void AddSubscriptionComPagamento_Sucesso()
    {
        var payment = new PaypalPayment(
            DateTime.Now, 
            DateTime.Now.AddDays(5), 
            10, 10,
            new Address("rua teste", 4, "teste", "cidade", "estado", "pais", "717171-000"),
            _student.Document,
            "Piroman",
            _student.Email 
        );

        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Valid);
    }
}