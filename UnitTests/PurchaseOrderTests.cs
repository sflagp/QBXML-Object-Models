using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class PurchaseOrderTests
    {
        [TestMethod]
        public void TestPurchaseOrderQueryRq()
        {
            PurchaseOrderQueryRq purchaseOrderRq = new();
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.TxnID = new() { "PurchaseOrderQueryRq.TxnID" };
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.TxnID = null;
            purchaseOrderRq.RefNumber = new() { "PurchaseOrderQueryRq.FullName" };
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.TxnDateRangeFilter = new();
            purchaseOrderRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            purchaseOrderRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.ModifiedDateRangeFilter = new();
            purchaseOrderRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            purchaseOrderRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.TxnDateRangeFilter = null;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.RefNumberFilter = new() { MatchCriterion = MatchCriterion.Contains, RefNumber = "RefNumberFilter.RefNumber" };
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.RefNumberRangeFilter = new() { FromRefNumber = "1", ToRefNumber = "10" };
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.RefNumberFilter = null;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            var model = new QryRqModel<PurchaseOrderQueryRq>();
            model.SetRequest(purchaseOrderRq, "QryRq");
            Assert.IsTrue(purchaseOrderRq.ToString().Contains("<PurchaseOrderQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<PurchaseOrderQueryRq>"));
        }

        [TestMethod]
        public void TestPurchaseOrderAddRq()
        {
            PurchaseOrderAddRq purchaseOrderRq = new();
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.Vendor = new() { FullName = "PurchaseOrderAddRq.Vendor.FullName" };
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.InventorySite = new();
            purchaseOrderRq.ShipToEntity = new();
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.ShipToEntity = null;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLine = new();
            purchaseOrderRq.PurchaseOrderLineGroup = new();
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLineGroup = null;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLine = new();
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLine.Add(new());
            purchaseOrderRq.PurchaseOrderLine[0].Item = new();
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            var model = new AddRqModel<PurchaseOrderAddRq>("PurchaseOrderAdd");
            model.SetRequest(purchaseOrderRq, "AddRq");
            Assert.IsTrue(purchaseOrderRq.ToString().Contains("<PurchaseOrderAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<PurchaseOrderAddRq>"));
        }

        [TestMethod]
        public void TestPurchaseOrderModRq()
        {
            PurchaseOrderModRq purchaseOrderRq = new();
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.TxnID = "PurchaseOrderModRq.TxnID";
            purchaseOrderRq.EditSequence = "PurchaseOrderModRq.EditSequence";
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.Vendor = new() { FullName = "PurchaseOrderAddRq.Vendor.FullName" };
            purchaseOrderRq.TxnDate = DateTime.Now;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.InventorySite = new();
            purchaseOrderRq.ShipToEntity = new();
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.ShipToEntity = null;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLine = new();
            purchaseOrderRq.PurchaseOrderLine.Add( new() { TxnLineID = "PurchaseOrderLineAddDto.TxnLineID" });
            purchaseOrderRq.PurchaseOrderLineGroup = new() { TxnLineID = "PurchaseOrderLineGroupAddDto.TxnLineID" };
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLineGroup = null;
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLine.Clear();
            purchaseOrderRq.PurchaseOrderLine.Add(new());
            Assert.IsFalse(purchaseOrderRq.IsEntityValid());

            purchaseOrderRq.PurchaseOrderLine[0].TxnLineID = "PurchaseOrderModRq.TxnLineID";
            Assert.IsTrue(purchaseOrderRq.IsEntityValid());

            var model = new ModRqModel<PurchaseOrderModRq>("PurchaseOrderMod");
            model.SetRequest(purchaseOrderRq, "ModRq");
            Assert.IsTrue(purchaseOrderRq.ToString().Contains("<PurchaseOrderModRq>"));
            Assert.IsTrue(model.ToString().Contains("<PurchaseOrderModRq>"));
        }
    }
}