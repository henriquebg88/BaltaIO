using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        bool ExistsDocument(string document);
        bool ExistsEmail(string email);
        void CreateSubscription(Student student);
    } 
}