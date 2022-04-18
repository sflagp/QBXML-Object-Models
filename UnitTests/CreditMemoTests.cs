using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class CreditMemoTests
    {
        [TestMethod]
        public void TestCreditMemoQueryRq()
        {
            CreditMemoQueryRq creditMemoRq = new();
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            creditMemoRq.TxnID = new() { "CreditMemoQueryRq.TxnID" };
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            creditMemoRq.TxnID = null;
            creditMemoRq.RefNumber = new() { "CreditMemoQueryRq.FullName" };
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            creditMemoRq.TxnDateRangeFilter = new();
            creditMemoRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            creditMemoRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            creditMemoRq.ModifiedDateRangeFilter = new();
            creditMemoRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            creditMemoRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(creditMemoRq.IsEntityValid());

            creditMemoRq.TxnDateRangeFilter = null;
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            var model = new QryRqModel<CreditMemoQueryRq>();
            model.SetRequest(creditMemoRq, "QryRq");
            Assert.IsTrue(creditMemoRq.ToString().Contains("<CreditMemoQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditMemoQueryRq>"));
        }

        [TestMethod]
        public void TestCreditMemoAddRq()
        {
            CreditMemoAddRq creditMemoRq = new();
            Assert.IsFalse(creditMemoRq.IsEntityValid());

            creditMemoRq.Customer = new() { FullName = "CreditMemoAddRq.Customer.FullName" };
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            creditMemoRq.CreditMemoLine = new();
            creditMemoRq.CreditMemoLineGroup = new();
            Assert.IsFalse(creditMemoRq.IsEntityValid());

            creditMemoRq.CreditMemoLineGroup = null;
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            var model = new AddRqModel<CreditMemoAddRq>("CreditMemoAdd");
            model.SetRequest(creditMemoRq, "AddRq");
            Assert.IsTrue(creditMemoRq.ToString().Contains("<CreditMemoAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditMemoAddRq>"));
        }

        [TestMethod]
        public void TestCreditMemoModRq()
        {
            CreditMemoModRq creditMemoRq = new();
            Assert.IsFalse(creditMemoRq.IsEntityValid());

            creditMemoRq.TxnID = "CreditMemoModRq.TxnID";
            creditMemoRq.EditSequence = "CreditMemoModRq.EditSequence";
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            creditMemoRq.TxnDate = DateTime.Now;
            creditMemoRq.Customer = new();
            Assert.IsTrue(creditMemoRq.IsEntityValid());

            var model = new ModRqModel<CreditMemoModRq>("CreditMemoMod");
            model.SetRequest(creditMemoRq, "ModRq");
            Assert.IsTrue(creditMemoRq.ToString().Contains("<CreditMemoModRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditMemoModRq>"));
        }
    }
}