using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class TransferTests
    {
        [TestMethod]
        public void TestTransferQueryRq()
        {
            TransferQueryRq transferRq = new();
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.TxnID = "TransferQueryRq.TxnID";
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.ModifiedDateRangeFilter = new();
            Assert.IsFalse(transferRq.IsEntityValid());

            transferRq.TxnID = null;
            transferRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            transferRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.TxnDateRangeFilter = new();
            transferRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            transferRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsFalse(transferRq.IsEntityValid());

            transferRq.ModifiedDateRangeFilter = null;
            Assert.IsTrue(transferRq.IsEntityValid());

            var model = new QryRqModel<TransferQueryRq>();
            model.SetRequest(transferRq, "QryRq");
            Assert.IsTrue(transferRq.ToString().Contains("<TransferQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferQueryRq>"));
        }

        [TestMethod]
        public void TestTransferAddRq()
        {
            TransferAddRq transferRq = new();
            Assert.IsFalse(transferRq.IsEntityValid());

            transferRq.TxnDate = DateTime.Now;
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.TransferFromAccount = new();
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.TransferToAccount = new();
            Assert.IsTrue(transferRq.IsEntityValid());

            var model = new AddRqModel<TransferAddRq>("TransferAdd");
            model.SetRequest(transferRq, "AddRq");
            Assert.IsTrue(transferRq.ToString().Contains("<TransferAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferAddRq>"));
        }

        [TestMethod]
        public void TestTransferModRq()
        {
            TransferModRq transferRq = new();
            Assert.IsFalse(transferRq.IsEntityValid());

            transferRq.TxnID = "TransferModRq.TxnID";
            transferRq.EditSequence = "TransferModRq.EditSequence";
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.TxnDate = DateTime.Now;
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.TransferFromAccount = new();
            Assert.IsTrue(transferRq.IsEntityValid());

            transferRq.TransferToAccount = new();
            Assert.IsTrue(transferRq.IsEntityValid());

            var model = new ModRqModel<TransferModRq>("TransferMod");
            model.SetRequest(transferRq, "ModRq");
            Assert.IsTrue(transferRq.ToString().Contains("<TransferModRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferModRq>"));
        }
    }
}