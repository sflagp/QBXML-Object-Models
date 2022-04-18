using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class DepositTests
    {
        [TestMethod]
        public void TestDepositQueryRq()
        {
            DepositQueryRq depositRq = new();
            Assert.IsTrue(depositRq.IsEntityValid());

            depositRq.TxnID = new() { "DepositQueryRq.TxnID" };
            Assert.IsTrue(depositRq.IsEntityValid());

            depositRq.TxnID = null;
            depositRq.RefNumber = new() { "DepositQueryRq.FullName" };
            Assert.IsTrue(depositRq.IsEntityValid());

            depositRq.TxnDateRangeFilter = new();
            depositRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            depositRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(depositRq.IsEntityValid());

            depositRq.ModifiedDateRangeFilter = new();
            depositRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            depositRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(depositRq.IsEntityValid());

            depositRq.TxnDateRangeFilter = null;
            Assert.IsTrue(depositRq.IsEntityValid());

            var model = new QryRqModel<DepositQueryRq>();
            model.SetRequest(depositRq, "QryRq");
            Assert.IsTrue(depositRq.ToString().Contains("<DepositQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<DepositQueryRq>"));
        }

        [TestMethod]
        public void TestDepositAddRq()
        {
            DepositAddRq depositRq = new();
            Assert.IsFalse(depositRq.IsEntityValid());

            depositRq.DepositToAccount = new();
            Assert.IsTrue(depositRq.IsEntityValid());

            depositRq.DepositLine = new();
            depositRq.DepositLine.Add(new());
            Assert.IsFalse(depositRq.IsEntityValid());

            depositRq.DepositLine[0].PaymentTxnID = "DepositAddRq.DepositLine.PaymentTxnID";
            depositRq.DepositLine[0].Account = new();
            Assert.IsFalse(depositRq.IsEntityValid());

            depositRq.DepositLine[0].PaymentTxnID = null;
            var model = new AddRqModel<DepositAddRq>("DepositAdd");
            model.SetRequest(depositRq, "AddRq");
            Assert.IsTrue(depositRq.ToString().Contains("<DepositAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<DepositAddRq>"));
        }

        [TestMethod]
        public void TestDepositModRq()
        {
            DepositModRq depositRq = new();
            Assert.IsFalse(depositRq.IsEntityValid());

            depositRq.TxnID = "DepositModRq.TxnID";
            depositRq.EditSequence = "DepositModRq.EditSequence";
            depositRq.TxnDate = DateTime.Now;
            Assert.IsTrue(depositRq.IsEntityValid());

            depositRq.DepositLine = new();
            depositRq.DepositLine.Add(new());
            Assert.IsFalse(depositRq.IsEntityValid());

            depositRq.DepositLine[0].TxnLineID = "DepositModRq.DepositLine.TxnLineID";
            depositRq.DepositLine[0].PaymentTxnID = "DepositModRq.DepositLine.PaymentTxnID";
            depositRq.DepositLine[0].Entity = new();
            Assert.IsFalse(depositRq.IsEntityValid());

            depositRq.DepositLine[0].PaymentTxnID = null;
            var model = new ModRqModel<DepositModRq>("DepositMod");
            model.SetRequest(depositRq, "ModRq");
            Assert.IsTrue(depositRq.ToString().Contains("<DepositModRq>"));
            Assert.IsTrue(model.ToString().Contains("<DepositModRq>"));
        }
    }
}