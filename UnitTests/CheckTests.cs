using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class CheckTests
    {
        [TestMethod]
        public void TestCheckQueryRq()
        {
            CheckQueryRq checkRq = new();
            Assert.IsTrue(checkRq.IsEntityValid());

            checkRq.TxnID = new() { "CheckQueryRq.TxnID" };
            checkRq.RefNumber = new() { "CheckQueryRq.RefNumber" };
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.RefNumber = null;
            Assert.IsTrue(checkRq.IsEntityValid());

            checkRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(checkRq.IsEntityValid());

            checkRq.RefNumberFilter = new();
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.RefNumberFilter.RefNumber = "CheckQueryRq.RefNumberFilter.RefNumber";
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.RefNumberFilter.MatchCriterion = MatchCriterion.None;
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.RefNumberFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(checkRq.IsEntityValid());

            var model = new QryRqModel<CheckQueryRq>();
            model.SetRequest(checkRq, "QryRq");
            Assert.IsTrue(checkRq.ToString().Contains("<CheckQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<CheckQueryRq>"));
        }

        [TestMethod]
        public void TestCheckAddRq()
        {
            CheckAddRq checkRq = new();
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.Account = new();
            Assert.IsTrue(checkRq.IsEntityValid());

            checkRq.PayeeEntity = new();
            Assert.IsTrue(checkRq.IsEntityValid());

            checkRq.ApplyCheckToTxn = new();
            checkRq.ApplyCheckToTxn.Add(new());
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.ApplyCheckToTxn[0].TxnID = "ApplyCheckToTxn.TxnID";
            Assert.IsTrue(checkRq.IsEntityValid());

            var model = new AddRqModel<CheckAddRq>("CheckAdd");
            model.SetRequest(checkRq, "AddRq");
            Assert.IsTrue(checkRq.ToString().Contains("<CheckAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CheckAddRq>"));
        }

        [TestMethod]
        public void TestCheckModRq()
        {
            CheckModRq checkRq = new();
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.TxnID = "CheckModRq.TxnID";
            checkRq.EditSequence = null;
            checkRq.TxnDate = DateTime.Now;
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.TxnID = null;
            checkRq.EditSequence = "CheckModRq.EditSequence";
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.TxnID = "CheckModRq.TxnID";
            checkRq.EditSequence = "CheckModRq.EditSequence";
            Assert.IsTrue(checkRq.IsEntityValid());

            checkRq.ItemGroupLine = new() { ItemLine = new() };
            checkRq.ItemGroupLine.ItemLine.Add(new()
            {
                TxnLineID = "TxnLineID #1",
                Amount = 1.11M,
                Item = new() { FullName = "Item Line 1"},
                BillableStatus = BillStatus.HasBeenBilled,
                Desc = "Mod line item description 1",
                SalesRep = new() { FullName = "Sales Rep 1" }
            });
            checkRq.ItemGroupLine.ItemLine.Add(new()
            {
                TxnLineID = "TxnLineID #2",
                Amount = 2.22M,
                Item = new() { FullName = "Item Line 2" },
                BillableStatus = BillStatus.Billable,
                Desc = "Line item MOD description 2",
                ClassRef = new() { FullName = "Class reference 1" }
            });
            checkRq.ItemGroupLine.ItemLine.Add(new()
            {
                TxnLineID = "TxnLineID #3",
                Amount = 3.33M,
                Item = new() { FullName = "Item Line 3" },
                BillableStatus = BillStatus.NotBillable,
                Desc = "Line item mod description 3",
                Customer = new() { FullName = "Customer 1" }
            });
            Assert.IsFalse(checkRq.IsEntityValid());

            checkRq.ItemGroupLine.TxnLineID = "ItemGroupLine.TxnLineID";
            Assert.IsTrue(checkRq.IsEntityValid());

            checkRq.ItemGroupLine.ItemGroup = new() { ListID = "ItemGroupLine.ItemGroup" };
            checkRq.ItemGroupLine.Quantity = 123M;
            Assert.IsTrue(checkRq.IsEntityValid());

            var model = new ModRqModel<CheckModRq>("CheckMod");
            model.SetRequest(checkRq, "ModRq");
            Assert.IsTrue(checkRq.ToString().Contains("<CheckModRq>"));
            Assert.IsTrue(model.ToString().Contains("<CheckModRq>"));
        }
    }

    [TestClass]
    public class BillPaymentCheckTests
    {
        [TestMethod]
        public void TestBillPaymentCheckQueryRq()
        {
            BillPaymentCheckQueryRq billPaymentCheckRq = new();
            Assert.IsTrue(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.RefNumberFilter = new();
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.RefNumberFilter.RefNumber = "BillPaymentCheckAddRq.RefNumberFilter.RefNumber";
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.RefNumberFilter.MatchCriterion = MatchCriterion.None;
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.RefNumberFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(billPaymentCheckRq.IsEntityValid());

            var model = new QryRqModel<BillPaymentCheckQueryRq>();
            model.SetRequest(billPaymentCheckRq, "QryRq");
            Assert.IsTrue(billPaymentCheckRq.ToString().Contains("<BillPaymentCheckQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillPaymentCheckQueryRq>"));
        }

        [TestMethod]
        public void TestBillPaymentCheckAddRq()
        {
            BillPaymentCheckAddRq billPaymentCheckRq = new();
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.PayeeEntity = new();
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.BankAccount = new();
            billPaymentCheckRq.AppliedToTxn = new();
            billPaymentCheckRq.AppliedToTxn.Add(new AppliedToTxnAddDto());
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.AppliedToTxn[0].TxnID = "BillPaymentCheckAddRq.TxnID";
            billPaymentCheckRq.AppliedToTxn[0].PaymentAmount = 123.45M;
            Assert.IsTrue(billPaymentCheckRq.IsEntityValid());

            var model = new AddRqModel<BillPaymentCheckAddRq>("BillPaymentCheckAdd");
            model.SetRequest(billPaymentCheckRq, "AddRq");
            Assert.IsTrue(billPaymentCheckRq.ToString().Contains("<BillPaymentCheckAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillPaymentCheckAddRq>"));
        }

        [TestMethod]
        public void TestBillPaymentCheckModRq()
        {
            BillPaymentCheckModRq billPaymentCheckRq = new();
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.TxnID = "BillPaymentCheckModRq.TxnID";
            billPaymentCheckRq.EditSequence = null;
            billPaymentCheckRq.TxnDate = DateTime.Now;
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.TxnID = null;
            billPaymentCheckRq.EditSequence = "BillPaymentCheckModRq.EditSequence";
            Assert.IsFalse(billPaymentCheckRq.IsEntityValid());

            billPaymentCheckRq.TxnID = "BillPaymentCheckModRq.TxnID";
            billPaymentCheckRq.EditSequence = "Test EditSequence";
            Assert.IsTrue(billPaymentCheckRq.IsEntityValid());

            var model = new ModRqModel<BillPaymentCheckModRq>("BillPaymentCheckMod");
            model.SetRequest(billPaymentCheckRq, "ModRq");
            Assert.IsTrue(billPaymentCheckRq.ToString().Contains("<BillPaymentCheckModRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillPaymentCheckModRq>"));
        }
    }

    [TestClass]
    public class SalesTaxPaymentCheckTests
    {
        [TestMethod]
        public void TestSalesTaxPaymentCheckQueryRq()
        {
            SalesTaxPaymentCheckQueryRq salesTaxPaymentCheckRq = new();
            Assert.IsTrue(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.RefNumberFilter = new();
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.RefNumberFilter.RefNumber = "SalesTaxPaymentCheckQueryRq.RefNumberFilter.RefNumber";
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.RefNumberFilter.MatchCriterion = MatchCriterion.None;
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.RefNumberFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(salesTaxPaymentCheckRq.IsEntityValid());

            var model = new QryRqModel<SalesTaxPaymentCheckQueryRq>();
            model.SetRequest(salesTaxPaymentCheckRq, "QryRq");
            Assert.IsTrue(salesTaxPaymentCheckRq.ToString().Contains("<SalesTaxPaymentCheckQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesTaxPaymentCheckQueryRq>"));
        }

        [TestMethod]
        public void TestSalesTaxPaymentCheckAddRq()
        {
            SalesTaxPaymentCheckAddRq salesTaxPaymentCheckRq = new();
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.TxnDate = DateTime.Now;
            salesTaxPaymentCheckRq.PayeeEntity = new();
            salesTaxPaymentCheckRq.BankAccount = new();
            Assert.IsTrue(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.BankAccount = null;
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.PayeeEntity = null;
            salesTaxPaymentCheckRq.BankAccount = new();
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.PayeeEntity = new();
            salesTaxPaymentCheckRq.SalesTaxPaymentCheckLine = new();
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.SalesTaxPaymentCheckLine.Add(new SalesTaxPaymentCheckLineAddDto());
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.SalesTaxPaymentCheckLine[0].Amount = 123.45M;
            Assert.IsTrue(salesTaxPaymentCheckRq.IsEntityValid());

            var model = new AddRqModel<SalesTaxPaymentCheckAddRq>("SalesTaxPaymentCheckAdd");
            model.SetRequest(salesTaxPaymentCheckRq, "AddRq");
            Assert.IsTrue(salesTaxPaymentCheckRq.ToString().Contains("<SalesTaxPaymentCheckAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesTaxPaymentCheckAddRq>"));
        }

        [TestMethod]
        public void TestSalesTaxPaymentCheckModRq()
        {
            SalesTaxPaymentCheckModRq salesTaxPaymentCheckRq = new();
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.TxnID = "SalesTaxPaymentCheckQueryRq.TxnID";
            salesTaxPaymentCheckRq.EditSequence = null;
            salesTaxPaymentCheckRq.TxnDate = DateTime.Now;
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.TxnID = null;
            salesTaxPaymentCheckRq.EditSequence = "SalesTaxPaymentCheckQueryRq.EditSequence";
            Assert.IsFalse(salesTaxPaymentCheckRq.IsEntityValid());

            salesTaxPaymentCheckRq.TxnID = "SalesTaxPaymentCheckQueryRq.TxnID";
            salesTaxPaymentCheckRq.EditSequence = "SalesTaxPaymentCheckQueryRq.EditSequence";
            Assert.IsTrue(salesTaxPaymentCheckRq.IsEntityValid());

            var model = new ModRqModel<SalesTaxPaymentCheckModRq>("SalesTaxPaymentCheckMod");
            model.SetRequest(salesTaxPaymentCheckRq, "ModRq");
            Assert.IsTrue(salesTaxPaymentCheckRq.ToString().Contains("<SalesTaxPaymentCheckModRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesTaxPaymentCheckModRq>"));
        }
    }
}
