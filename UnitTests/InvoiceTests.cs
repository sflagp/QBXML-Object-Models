using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class InvoiceTests
    {
        [TestMethod]
        public void TestInvoiceQueryRq()
        {
            InvoiceQueryRq invoiceRq = new();
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.TxnID = new() { "InventoryAdjustmentQueryRq.TxnID" };
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.TxnID = null;
            invoiceRq.RefNumber = new() { "InventoryAdjustmentQueryRq.FullName" };
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.TxnDateRangeFilter = new();
            invoiceRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            invoiceRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.ModifiedDateRangeFilter = new();
            invoiceRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            invoiceRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.TxnDateRangeFilter = null;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            var model = new QryRqModel<InvoiceQueryRq>();
            model.SetRequest(invoiceRq, "QryRq");
            Assert.IsTrue(invoiceRq.ToString().Contains("<InvoiceQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<InvoiceQueryRq>"));
        }

        [TestMethod]
        public void TestInvoiceAddRq()
        {
            InvoiceAddRq invoiceRq = new();
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.Customer = new() { FullName = "InvoiceAddRq.Customer.FullName" };
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceLine = new();
            invoiceRq.InvoiceGroupLine = new();
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceGroupLine = null;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.SetCredit = new();
            invoiceRq.SetCredit.Add(new());
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.SetCredit[0].CreditTxnID = "InvoiceAddRq.SetCredit.CreditTxnID";
            invoiceRq.SetCredit[0].AppliedAmount = 10m;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceLine = new();
            invoiceRq.InvoiceGroupLine = new();
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceGroupLine = null;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            var model = new AddRqModel<InvoiceAddRq>("InvoiceAdd");
            model.SetRequest(invoiceRq, "AddRq");
            Assert.IsTrue(invoiceRq.ToString().Contains("<InvoiceAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<InvoiceAddRq>"));
        }

        [TestMethod]
        public void TestInvoiceModRq()
        {
            InvoiceModRq invoiceRq = new();
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.TxnID = "InvoiceModRq.TxnID";
            invoiceRq.EditSequence = "InvoiceModRq.EditSequence";
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.TxnDate = DateTime.Now;
            invoiceRq.Customer = new() { FullName = "InvoiceAddRq.Customer.FullName" };
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceLine = new();
            invoiceRq.InvoiceGroupLine = new() { TxnLineID = "InvoiceGroupLine.TxnLineID" };
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceGroupLine = null;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.SetCredit = new();
            invoiceRq.SetCredit.Add(new());
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.SetCredit[0].CreditTxnID = "InvoiceAddRq.SetCredit.CreditTxnID";
            invoiceRq.SetCredit[0].AppliedAmount = 10m;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceLine = new();
            invoiceRq.InvoiceGroupLine = new() { TxnLineID = "InvoiceGroupLine.TxnLineID" };
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceGroupLine = null;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceLine.Add(new() { TxnLineID = "InvoiceModRq.InvoiceLine.TxnLineID", Rate = 10m, RatePercent = "5" });
            Assert.IsFalse(invoiceRq.IsEntityValid());

            invoiceRq.InvoiceLine[0].RatePercent = null;
            Assert.IsTrue(invoiceRq.IsEntityValid());

            var model = new ModRqModel<InvoiceModRq>("InvoiceMod");
            model.SetRequest(invoiceRq, "ModRq");
            Assert.IsTrue(invoiceRq.ToString().Contains("<InvoiceModRq>"));
            Assert.IsTrue(model.ToString().Contains("<InvoiceModRq>"));
        }
    }
}