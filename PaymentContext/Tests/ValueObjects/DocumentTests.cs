using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void CNPJ_Invalido_Erro()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            var doc2 = new Document("31873696000194", EDocumentType.CPF);

            Assert.IsTrue(doc.Invalid);
            Assert.IsTrue(doc2.Invalid);
        }

        [TestMethod]
        public void CNPJ_Valido_Success()
        {
            var doc = new Document("31873696000194", EDocumentType.CNPJ);

            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("65447986060", EDocumentType.CNPJ)]
        [DataRow("6544798606", EDocumentType.CPF)]
        [DataRow("", EDocumentType.CPF)]
        public void CPF_Invalido_Erro(string numero, EDocumentType tipo)
        {
            var doc = new Document(numero, tipo);

            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void CPF_Valido_Success()
        {
            var doc = new Document("65447986060", EDocumentType.CPF);

            Assert.IsTrue(doc.Valid);
        }
    }
}