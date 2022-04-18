using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QbModels.Tests
{
    [TestClass]
    public class CompanyTests
    {
        [TestMethod]
        public void TestCompanyQueryRq()
        {
            CompanyQueryRq cmpyRq = new();
            Assert.IsTrue(cmpyRq.IsEntityValid());

            var model = new QryRqModel<CompanyQueryRq>();
            model.SetRequest(cmpyRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<CompanyQueryRq />"));
            Assert.IsTrue(cmpyRq.ToString().Contains("<CompanyQueryRq />"));
        }
    }
}