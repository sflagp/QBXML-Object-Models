using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class SalesOrderTests
    {
        [TestMethod]
        public void TestSalesOrderQueryRq()
        {
            SalesOrderQueryRq salesOrderRq = new();
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.TxnID = new() { "SalesOrderQueryRq.TxnID" };
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.TxnID = null;
            salesOrderRq.RefNumber = new() { "SalesOrderQueryRq.FullName" };
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.TxnDateRangeFilter = new();
            salesOrderRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            salesOrderRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.ModifiedDateRangeFilter = new();
            salesOrderRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            salesOrderRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.TxnDateRangeFilter = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.RefNumberFilter = new() { MatchCriterion = MatchCriterion.Contains, RefNumber = "RefNumberFilter.RefNumber" };
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.RefNumberRangeFilter = new() { FromRefNumber = "1", ToRefNumber = "10" };
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.RefNumberFilter = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            var model = new QryRqModel<SalesOrderQueryRq>();
            model.SetRequest(salesOrderRq, "QryRq");
            Assert.IsTrue(salesOrderRq.ToString().Contains("<SalesOrderQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesOrderQueryRq>"));
        }

        [TestMethod]
        public void TestSalesOrderAddRq()
        {
            SalesOrderAddRq salesOrderRq = new();
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.Customer = new();
            salesOrderRq.SalesRep = new() { FullName = "SalesOrderAddRq.SalesRep.FullName" };
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine = new();
            salesOrderRq.SalesOrderLineGroup = new();
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLineGroup = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine = new();
            salesOrderRq.SalesOrderLine.Add(new());
            salesOrderRq.SalesOrderLine[0].Rate = 1.0M;
            salesOrderRq.SalesOrderLine[0].RatePercent = "SalesOrderAddRq.SalesOrderLine.RatePercent";
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].RatePercent = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].SerialNumber = "SalesOrderAddRq.SalesOrderLine.SerialNumber";
            salesOrderRq.SalesOrderLine[0].LotNumber = "SalesOrderAddRq.SalesOrderLine.LotNumber";
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].LotNumber = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine = null;
            salesOrderRq.SalesOrderLineGroup = new();
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLineGroup.ItemGroup = new();
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            var model = new AddRqModel<SalesOrderAddRq>("SalesOrderAdd");
            model.SetRequest(salesOrderRq, "AddRq");
            Assert.IsTrue(salesOrderRq.ToString().Contains("<SalesOrderAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesOrderAddRq>"));
        }

        [TestMethod]
        public void TestSalesOrderModRq()
        {
            SalesOrderModRq salesOrderRq = new();
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.TxnID = "SalesOrderModRq.TxnID";
            salesOrderRq.EditSequence = "SalesOrderModRq.EditSequence";
            salesOrderRq.TxnDate = DateTime.Now;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesRep = new();
            salesOrderRq.Customer = new();
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine = new();
            salesOrderRq.SalesOrderLineGroup = new();
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine = new();
            salesOrderRq.SalesOrderLine.Add(new());
            salesOrderRq.SalesOrderLine[0].TxnLineID = "SalesOrderModRq.SalesOrderLine.TxnLineID";
            salesOrderRq.SalesOrderLineGroup = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine = new();
            salesOrderRq.SalesOrderLine.Add(new() { TxnLineID = "SalesOrderModRq.SalesOrderLine.TxnLineId" });
            salesOrderRq.SalesOrderLine[0].Rate = 1.0M;
            salesOrderRq.SalesOrderLine[0].RatePercent = "SalesOrderAddRq.SalesOrderLine.RatePercent";
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].RatePercent = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].PriceLevel = new();
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].PriceLevel = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].SerialNumber = "SalesOrderAddRq.SalesOrderLine.SerialNumber";
            salesOrderRq.SalesOrderLine[0].LotNumber = "SalesOrderAddRq.SalesOrderLine.LotNumber";
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine[0].LotNumber = null;
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLine = null;
            salesOrderRq.SalesOrderLineGroup = new();
            Assert.IsFalse(salesOrderRq.IsEntityValid());

            salesOrderRq.SalesOrderLineGroup.ItemGroup = new();
            salesOrderRq.SalesOrderLineGroup.TxnLineID = "SalesOrderModRq.SalesOrderLineGroup.TxnLineID";
            Assert.IsTrue(salesOrderRq.IsEntityValid());

            var model = new ModRqModel<SalesOrderModRq>("SalesOrderMod");
            model.SetRequest(salesOrderRq, "ModRq");
            Assert.IsTrue(salesOrderRq.ToString().Contains("<SalesOrderModRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesOrderModRq>"));
        }
    }

    [TestClass]
    public class SalesReceiptTests
    {
        [TestMethod]
        public void TestSalesReceiptQueryRq()
        {
            SalesReceiptQueryRq salesReceiptRq = new();
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.TxnID = new() { "SalesReceiptQueryRq.TxnID" };
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.TxnID = null;
            salesReceiptRq.RefNumber = new() { "SalesReceiptQueryRq.FullName" };
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.TxnDateRangeFilter = new();
            salesReceiptRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            salesReceiptRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.ModifiedDateRangeFilter = new();
            salesReceiptRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            salesReceiptRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.TxnDateRangeFilter = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.RefNumberFilter = new() { MatchCriterion = MatchCriterion.Contains, RefNumber = "RefNumberFilter.RefNumber" };
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.RefNumberRangeFilter = new() { FromRefNumber = "1", ToRefNumber = "10" };
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.RefNumberFilter = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            var model = new QryRqModel<SalesReceiptQueryRq>();
            model.SetRequest(salesReceiptRq, "QryRq");
            Assert.IsTrue(salesReceiptRq.ToString().Contains("<SalesReceiptQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesReceiptQueryRq>"));
        }

        [TestMethod]
        public void TestSalesReceiptAddRq()
        {
            SalesReceiptAddRq salesReceiptRq = new();
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesRep = new() { FullName = "SalesReceiptAddRq.SalesRep.FullName" };
            salesReceiptRq.Customer = new();
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine = new();
            salesReceiptRq.SalesReceiptLineGroup = new();
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLineGroup = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine = new();
            salesReceiptRq.SalesReceiptLine.Add(new());
            salesReceiptRq.SalesReceiptLine[0].Rate = 1.0M;
            salesReceiptRq.SalesReceiptLine[0].RatePercent = "SalesReceiptAddRq.SalesOrderLine.RatePercent";
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine[0].RatePercent = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine[0].SerialNumber = "SalesReceiptAddRq.SalesOrderLine.SerialNumber";
            salesReceiptRq.SalesReceiptLine[0].LotNumber = "SalesReceiptAddRq.SalesOrderLine.LotNumber";
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine[0].LotNumber = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine = null;
            salesReceiptRq.SalesReceiptLineGroup = new();
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLineGroup.ItemGroup = new();
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            var model = new AddRqModel<SalesReceiptAddRq>("SalesReceiptAdd");
            model.SetRequest(salesReceiptRq, "AddRq");
            Assert.IsTrue(salesReceiptRq.ToString().Contains("<SalesReceiptAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesReceiptAddRq>"));
        }

        [TestMethod]
        public void TestSalesReceiptModRq()
        {
            SalesReceiptModRq salesReceiptRq = new();
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.TxnID = "SalesOrderModRq.TxnID";
            salesReceiptRq.EditSequence = "SalesOrderModRq.EditSequence";
            salesReceiptRq.TxnDate = DateTime.Now;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesRep = new();
            salesReceiptRq.Customer = new();
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine = new();
            salesReceiptRq.SalesReceiptLine.Add(new() { TxnLineID = "SalesReceiptLine.TxnLineID" });
            salesReceiptRq.SalesReceiptLineGroup = new() { TxnLineID = "SalesReceiptLineGroup.TxnLineID" };
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLineGroup = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine = new();
            salesReceiptRq.SalesReceiptLine.Add(new() { TxnLineID = "SalesReceiptRq.SalesReceiptLine.TxnLineID" });
            salesReceiptRq.SalesReceiptLine[0].Rate = 1.0M;
            salesReceiptRq.SalesReceiptLine[0].RatePercent = "SalesReceiptAddRq.SalesOrderLine.RatePercent";
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine[0].RatePercent = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine[0].SerialNumber = "SalesReceiptAddRq.SalesOrderLine.SerialNumber";
            salesReceiptRq.SalesReceiptLine[0].LotNumber = "SalesReceiptAddRq.SalesOrderLine.LotNumber";
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine[0].LotNumber = null;
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLine = null;
            salesReceiptRq.SalesReceiptLineGroup = new();
            Assert.IsFalse(salesReceiptRq.IsEntityValid());

            salesReceiptRq.SalesReceiptLineGroup.TxnLineID = "SalesReceiptModRq.SalesReceiptLineGroup.TxnLineID";
            Assert.IsTrue(salesReceiptRq.IsEntityValid());

            var model = new ModRqModel<SalesReceiptModRq>("SalesReceiptMod");
            model.SetRequest(salesReceiptRq, "ModRq");
            Assert.IsTrue(salesReceiptRq.ToString().Contains("<SalesReceiptModRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesReceiptModRq>"));
        }
    }

    [TestClass]
    public class SalesRepTests
    {
        [TestMethod]
        public void TestSalesRepQueryRq()
        {
            SalesRepQueryRq salesRepRq = new();
            Assert.IsTrue(salesRepRq.IsEntityValid());

            salesRepRq.ListID = new() { "SalesRepQueryRq.ListID" };
            salesRepRq.MaxReturned = -1;
            Assert.IsTrue(salesRepRq.IsEntityValid());

            salesRepRq.ListID = null;
            salesRepRq.FullName = new() { "SalesRepQueryRq.FullName" };
            Assert.IsTrue(salesRepRq.IsEntityValid());

            salesRepRq.FullName = null;
            salesRepRq.NameFilter = new();
            salesRepRq.MaxReturned = 99999;
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.NameFilter.MatchCriterion = MatchCriterion.None;
            salesRepRq.NameFilter.Name = "A";
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(salesRepRq.IsEntityValid());

            salesRepRq.NameRangeFilter = new();
            salesRepRq.NameRangeFilter.FromName = "A";
            salesRepRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.NameFilter = null;
            Assert.IsTrue(salesRepRq.IsEntityValid());

            var model = new QryRqModel<SalesRepQueryRq>();
            model.SetRequest(salesRepRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<SalesRepQueryRq>"));
            Assert.IsTrue(salesRepRq.ToString().Contains("<SalesRepQueryRq>"));
        }

        [TestMethod]
        public void TestSalesRepAddRq()
        {
            SalesRepAddRq salesRepRq = new();
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.Initial = "SalesRepAddRq.Initial";
            salesRepRq.SalesRepEntity = new();
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.Initial = "Initi";
            salesRepRq.SalesRepEntity = new();
            Assert.IsTrue(salesRepRq.IsEntityValid());

            var model = new AddRqModel<SalesRepAddRq>("SalesRepAdd");
            model.SetRequest(salesRepRq, "AddRq");
            Assert.IsTrue(salesRepRq.ToString().Contains("<SalesRepAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesRepAddRq>"));
        }

        [TestMethod]
        public void TestSalesRepModRq()
        {
            SalesRepModRq salesRepRq = new();
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.ListID = "SalesRepModRq.ListID";
            salesRepRq.EditSequence = "SalesRepModRq.EditSequence";
            Assert.IsTrue(salesRepRq.IsEntityValid());

            salesRepRq.Initial = "SalesRepAddRq.Initial";
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.Initial = "Initi";
            Assert.IsTrue(salesRepRq.IsEntityValid());

            var model = new ModRqModel<SalesRepModRq>("SalesRepMod");
            model.SetRequest(salesRepRq, "ModRq");
            Assert.IsTrue(salesRepRq.ToString().Contains("<SalesRepModRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesRepModRq>"));
        }
    }

    [TestClass]
    public class SalesTaxCodeTests
    {
        [TestMethod]
        public void TestSalesTaxCodeQueryRq()
        {
            SalesTaxCodeQueryRq salesTaxCodeRq = new();
            Assert.IsTrue(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.ListID = new() { "SalesTaxCodeQueryRq.ListID" };
            salesTaxCodeRq.MaxReturned = -1;
            Assert.IsTrue(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.ListID = null;
            salesTaxCodeRq.FullName = new() { "SalesTaxCodeQueryRq.FullName" };
            Assert.IsTrue(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.FullName = null;
            salesTaxCodeRq.NameFilter = new();
            salesTaxCodeRq.MaxReturned = 99999;
            Assert.IsFalse(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.NameFilter.MatchCriterion = MatchCriterion.None;
            salesTaxCodeRq.NameFilter.Name = "A";
            Assert.IsFalse(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.NameRangeFilter = new();
            salesTaxCodeRq.NameRangeFilter.FromName = "A";
            salesTaxCodeRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.NameFilter = null;
            Assert.IsTrue(salesTaxCodeRq.IsEntityValid());

            var model = new QryRqModel<SalesTaxCodeQueryRq>();
            model.SetRequest(salesTaxCodeRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<SalesTaxCodeQueryRq>"));
            Assert.IsTrue(salesTaxCodeRq.ToString().Contains("<SalesTaxCodeQueryRq>"));
        }

        [TestMethod]
        public void TestSalesTaxCodeAddRq()
        {
            SalesTaxCodeAddRq salesTaxCodeRq = new();
            Assert.IsFalse(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.Name = "SalesRepAddRq.Name";
            Assert.IsFalse(salesTaxCodeRq.IsEntityValid());

            salesTaxCodeRq.Name = "Nm";
            Assert.IsTrue(salesTaxCodeRq.IsEntityValid());

            var model = new AddRqModel<SalesTaxCodeAddRq>("SalesTaxCodeAdd");
            model.SetRequest(salesTaxCodeRq, "AddRq");
            Assert.IsTrue(salesTaxCodeRq.ToString().Contains("<SalesTaxCodeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesTaxCodeAddRq>"));
        }

        [TestMethod]
        public void TestSalesTaxCodeModRq()
        {
            SalesTaxCodeModRq salesRepRq = new();
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.ListID = "SalesTaxCodeModRq.ListID";
            salesRepRq.EditSequence = "SalesTaxCodeModRq.EditSequence";
            Assert.IsTrue(salesRepRq.IsEntityValid());

            salesRepRq.Name = "SalesTaxCodeModRq.Name";
            Assert.IsFalse(salesRepRq.IsEntityValid());

            salesRepRq.Name = "Nam";
            Assert.IsTrue(salesRepRq.IsEntityValid());

            var model = new ModRqModel<SalesTaxCodeModRq>("SalesTaxCodeMod");
            model.SetRequest(salesRepRq, "ModRq");
            Assert.IsTrue(salesRepRq.ToString().Contains("<SalesTaxCodeModRq>"));
            Assert.IsTrue(model.ToString().Contains("<SalesTaxCodeModRq>"));
        }
    }
}