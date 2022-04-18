using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class ChargeTests
    {
        [TestMethod]
        public void TestChargeQueryRq()
        {
            ChargeQueryRq chargeRq = new();
            Assert.IsTrue(chargeRq.IsEntityValid());

            chargeRq.TxnID = new() { "InventoryAdjustmentQueryRq.TxnID" };
            Assert.IsTrue(chargeRq.IsEntityValid());

            chargeRq.TxnID = null;
            chargeRq.RefNumber = new() { "InventoryAdjustmentQueryRq.FullName" };
            Assert.IsTrue(chargeRq.IsEntityValid());

            chargeRq.TxnDateRangeFilter = new();
            chargeRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            chargeRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(chargeRq.IsEntityValid());

            chargeRq.ModifiedDateRangeFilter = new();
            chargeRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            chargeRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(chargeRq.IsEntityValid());

            chargeRq.TxnDateRangeFilter = null;
            Assert.IsTrue(chargeRq.IsEntityValid());

            var model = new QryRqModel<ChargeQueryRq>();
            model.SetRequest(chargeRq, "QryRq");
            Assert.IsTrue(chargeRq.ToString().Contains("<ChargeQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<ChargeQueryRq>"));
        }

        [TestMethod]
        public void TestChargeAddRq()
        {
            ChargeAddRq chargeRq = new();
            Assert.IsFalse(chargeRq.IsEntityValid());

            chargeRq.Customer = new();
            Assert.IsTrue(chargeRq.IsEntityValid());

            chargeRq.Customer.ListID = "ChargeQueryRq.ListID";
            Assert.IsTrue(chargeRq.IsEntityValid());

            var model = new AddRqModel<ChargeAddRq>("ChargeAdd");
            model.SetRequest(chargeRq, "AddRq");
            Assert.IsTrue(chargeRq.ToString().Contains("<ChargeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<ChargeAddRq>"));
        }

        [TestMethod]
        public void TestChargeModRq()
        {
            ChargeModRq chargeRq = new();
            Assert.IsFalse(chargeRq.IsEntityValid());

            chargeRq.TxnID = "ChargeQueryRq.TxnID";
            chargeRq.EditSequence = "ChargeQueryRq.EditSequence";
            chargeRq.TxnDate = DateTime.Now;
            Assert.IsTrue(chargeRq.IsEntityValid());

            chargeRq.Customer = new();
            Assert.IsTrue(chargeRq.IsEntityValid());

            var model = new ModRqModel<ChargeModRq>("ChargeMod");
            model.SetRequest(chargeRq, "ModRq");
            Assert.IsTrue(chargeRq.ToString().Contains("<ChargeModRq>"));
            Assert.IsTrue(model.ToString().Contains("<ChargeModRq>"));
        }
    }
}