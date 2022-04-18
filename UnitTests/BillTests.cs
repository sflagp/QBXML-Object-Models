using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class BillTests
    {
        [TestMethod]
        public void TestBillQueryRq()
        {
            BillQueryRq billRq = new();
            Assert.IsTrue(billRq.IsEntityValid());

            billRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(billRq.IsEntityValid());

            billRq.TxnID = new() { "InventoryAdjustmentQueryRq.TxnID" };
            Assert.IsTrue(billRq.IsEntityValid());

            billRq.TxnID = null;
            billRq.RefNumber = new() { "InventoryAdjustmentQueryRq.FullName" };
            Assert.IsTrue(billRq.IsEntityValid());

            billRq.TxnDateRangeFilter = new();
            billRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            billRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            billRq.EntityFilter = new() { FullName = new() { "EntityName" } };
            Assert.IsTrue(billRq.IsEntityValid());

            billRq.ModifiedDateRangeFilter = new();
            billRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            billRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(billRq.IsEntityValid());

            billRq.TxnDateRangeFilter = null;
            Assert.IsTrue(billRq.IsEntityValid());

            var model = new QryRqModel<BillQueryRq>();
            model.SetRequest(billRq, "QryRq");
            Assert.IsTrue(billRq.ToString().Contains("<BillQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillQueryRq>"));
        }

        [TestMethod]
        public void TestBillAddRq()
        {
            BillAddRq billRq = new();
            Assert.IsFalse(billRq.IsEntityValid());

            billRq.Vendor = new();
            Assert.IsTrue(billRq.IsEntityValid());

            var model = new AddRqModel<BillAddRq>("BillAdd");
            model.SetRequest(billRq, "AddRq");
            Assert.IsTrue(billRq.ToString().Contains("<BillAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillAddRq>"));
        }

        [TestMethod]
        public void TestBillModRq()
        {
            BillModRq billRq = new();
            Assert.IsFalse(billRq.IsEntityValid());

            billRq.TxnID = "Test TxnID";
            billRq.EditSequence = null;
            billRq.TxnDate = DateTime.Now;
            Assert.IsFalse(billRq.IsEntityValid());

            billRq.TxnID = null;
            billRq.EditSequence = "Test EditSequence";
            Assert.IsFalse(billRq.IsEntityValid());

            billRq.TxnID = "Test TxnID";
            billRq.EditSequence = "Test EditSequence";
            Assert.IsTrue(billRq.IsEntityValid());

            var model = new ModRqModel<BillModRq>("BillMod");
            model.SetRequest(billRq, "ModRq");
            Assert.IsTrue(billRq.ToString().Contains("<BillModRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillModRq>"));
        }

        [TestMethod]
        public void TestBillToPayQueryRq()
        {
            BillToPayQueryRq billToPayRq = new();
            Assert.IsFalse(billToPayRq.IsEntityValid());

            billToPayRq.PayeeEntity = new();
            Assert.IsTrue(billToPayRq.IsEntityValid());

            var model = new QryRqModel<BillToPayQueryRq>();
            model.SetRequest(billToPayRq, "QryRq");
            Assert.IsTrue(billToPayRq.ToString().Contains("<BillToPayQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillToPayQueryRq>"));
        }

        [TestMethod]
        public void TestBillingRateQueryRq()
        {
            BillingRateQueryRq billingRateRq = new();
            Assert.IsTrue(billingRateRq.IsEntityValid());

            billingRateRq.NameFilter = new();
            Assert.IsFalse(billingRateRq.IsEntityValid());

            billingRateRq.NameFilter.MatchCriterion = MatchCriterion.None;
            billingRateRq.NameFilter.Name = "A";
            Assert.IsFalse(billingRateRq.IsEntityValid());

            billingRateRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(billingRateRq.IsEntityValid());

            var model = new QryRqModel<BillingRateQueryRq>();
            model.SetRequest(billingRateRq, "QryRq");
            Assert.IsTrue(billingRateRq.ToString().Contains("<BillingRateQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillingRateQueryRq>"));
        }
    }
}
