using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentsQueriesTest
    {
        private IList<Student> _students;

        public StudentsQueriesTest(){
            _students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                _students.Add(
                    new Student(
                        new Name("Aluni", i.ToString()), 
                        new Document("1111111111" + i, EDocumentType.CPF),
                        new Email(i + "@teste.com")));
            }
        }

        [TestMethod]
        public void ShouldReturNullWhenDocumentNotExists()
        {
            var expression = StudentQueries.GetStudentInfo("12345678911");
            var studn = _students.AsQueryable().Where(expression).FirstOrDefault();

            Assert.IsNull(studn);
        }

        [TestMethod]
        public void ShouldotReturNullWhenDocumentExists()
        {
            var expression = StudentQueries.GetStudentInfo("11111111111");
            var studn = _students.AsQueryable().Where(expression).FirstOrDefault();

            Assert.IsNotNull(studn);
        }
    }
}