using PaymentContext.Domain.Entities;

namespace Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var subscription = new Subscription(null);
        var student = new Student("Henrique", "Gonzaga", "737.946.191-53", "henriquebg88@gmail.com");
    }
}