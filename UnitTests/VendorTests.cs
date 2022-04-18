using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class VendorTests
    {
        [TestMethod]
        public void TestVendorQueryRq()
        {
            VendorQueryRq vendorRq = new();
            Assert.IsTrue(vendorRq.IsEntityValid());
            
            vendorRq.ListID = new() { "VendorQueryRq.ListID" };
            vendorRq.MaxReturned = -1;
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.FullName = new() { "VendorQueryRq.FullName" };
            Assert.IsFalse(vendorRq.IsEntityValid());

            vendorRq.ListID = null;
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.FullName = null;
            vendorRq.NameFilter = new();
            vendorRq.MaxReturned = 99999;
            Assert.IsFalse(vendorRq.IsEntityValid());

            vendorRq.NameFilter.MatchCriterion = MatchCriterion.None;
            vendorRq.NameFilter.Name = "A";
            Assert.IsFalse(vendorRq.IsEntityValid());

            vendorRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.NameRangeFilter = new();
            vendorRq.NameRangeFilter.FromName = "A";
            vendorRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(vendorRq.IsEntityValid());

            vendorRq.NameFilter = null;
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.TotalBalanceFilter = new() { Operator = "Operator", Amount = 1000 };
            Assert.IsFalse(vendorRq.IsEntityValid());

            vendorRq.TotalBalanceFilter.Operator = "GreaterThanEqual";
            Assert.IsTrue(vendorRq.IsEntityValid());

            var model = new QryRqModel<VendorQueryRq>();
            model.SetRequest(vendorRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<VendorQueryRq>"));
            Assert.IsTrue(vendorRq.ToString().Contains("<VendorQueryRq>"));
        }

        [TestMethod]
        public void TestVendorAddRq()
        {
            VendorAddRq custRq = new();
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.Name = "VendorAddRq";
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact = new();
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.AdditionalContact = null;
            Assert.IsTrue(custRq.IsEntityValid());

            var model = new AddRqModel<VendorAddRq>("VendorAdd");
            model.SetRequest(custRq, "AddRq");
            Assert.IsTrue(custRq.ToString().Contains("<VendorAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<VendorAddRq>"));
        }

        [TestMethod]
        public void TestVendorModRq()
        {
            VendorModRq vendorRq = new();
            Assert.IsFalse(vendorRq.IsEntityValid());

            vendorRq.ListID = "VendorModRq.ListID";
            vendorRq.EditSequence = "VendorModRq.EditSequence";
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.Name = "VendorAddRq";
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.AdditionalContact = new();
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            Assert.IsTrue(vendorRq.IsEntityValid());

            vendorRq.AdditionalContact.Add(new() { ContactName = "VendorAddRq.ContactName", ContactValue = "VendorAddRq.ContactValue" });
            Assert.IsFalse(vendorRq.IsEntityValid());

            vendorRq.AdditionalContact = null;
            Assert.IsTrue(vendorRq.IsEntityValid());

            var model = new ModRqModel<VendorModRq>("VendorMod");
            model.SetRequest(vendorRq, "ModRq");
            Assert.IsTrue(vendorRq.ToString().Contains("<VendorModRq>"));
            Assert.IsTrue(model.ToString().Contains("<VendorModRq>"));
        }
    }

    [TestClass]
    public class VendorCreditTests
    {
        [TestMethod]
        public void TestVendorCreditQueryRq()
        {
            VendorCreditQueryRq vendorCreditRq = new();
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.TxnID = new() { "VendorCreditQueryRq.TxnID" };
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.TxnID = null;
            vendorCreditRq.RefNumber = new() { "VendorCreditQueryRq.FullName" };
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.TxnDateRangeFilter = new();
            vendorCreditRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            vendorCreditRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.ModifiedDateRangeFilter = new();
            vendorCreditRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            vendorCreditRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(vendorCreditRq.IsEntityValid());

            vendorCreditRq.TxnDateRangeFilter = null;
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.AccountFilter = new(){ FullName = new() { "VendorCreditQueryRq.AccountFilter.FullName" } };
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.AccountFilter.ListID = new() { "VendorCreditQueryRq.AccountFilter.ListID" };
            Assert.IsFalse(vendorCreditRq.IsEntityValid());

            vendorCreditRq.AccountFilter = null;
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            var model = new QryRqModel<VendorCreditQueryRq>();
            model.SetRequest(vendorCreditRq, "QryRq");
            Assert.IsTrue(vendorCreditRq.ToString().Contains("<VendorCreditQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<VendorCreditQueryRq>"));
        }

        [TestMethod]
        public void TestVendorCreditAddRq()
        {
            VendorCreditAddRq vendorCreditRq = new();
            Assert.IsFalse(vendorCreditRq.IsEntityValid());

            vendorCreditRq.Vendor = new() { FullName = "VendorCreditAddRq.Vendor.FullName" };
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.ItemLine = new();
            vendorCreditRq.ItemGroupLine = new();
            Assert.IsFalse(vendorCreditRq.IsEntityValid());

            vendorCreditRq.ItemGroupLine = null;
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.ExpenseLine = new();
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            var model = new AddRqModel<VendorCreditAddRq>("VendorCreditAdd");
            model.SetRequest(vendorCreditRq, "AddRq");
            Assert.IsTrue(vendorCreditRq.ToString().Contains("<VendorCreditAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<VendorCreditAddRq>"));
        }

        [TestMethod]
        public void TestVendorCreditModRq()
        {
            VendorCreditModRq vendorCreditRq = new();
            Assert.IsFalse(vendorCreditRq.IsEntityValid());

            vendorCreditRq.TxnID = "VendorCreditModRq.TxnID";
            vendorCreditRq.EditSequence = "VendorCreditModRq.EditSequence";
            vendorCreditRq.TxnDate = DateTime.Now;
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.Vendor = new() { FullName = "VendorCreditAddRq.Vendor.FullName" };
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.ItemLine = new();
            vendorCreditRq.ItemLine.Add(new() { TxnLineID = "ItemLine.TxnLineID" });
            vendorCreditRq.ItemGroupLine = new() { TxnLineID = "ItemGroupLine.TxnLineID" };
            Assert.IsFalse(vendorCreditRq.IsEntityValid());

            vendorCreditRq.ItemGroupLine = null;
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            vendorCreditRq.ExpenseLine = new();
            Assert.IsTrue(vendorCreditRq.IsEntityValid());

            var model = new ModRqModel<VendorCreditModRq>("VendorCreditMod");
            model.SetRequest(vendorCreditRq, "ModRq");
            Assert.IsTrue(vendorCreditRq.ToString().Contains("<VendorCreditModRq>"));
            Assert.IsTrue(model.ToString().Contains("<VendorCreditModRq>"));
        }
    }
    [TestClass]
    public class VendorTypeTests
    {
        [TestMethod]
        public void TestVendorTypeQueryRq()
        {
            VendorTypeQueryRq custTypeRq = new();
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = new() { "VendorTypeQueryRq.ListID" };
            custTypeRq.MaxReturned = -1;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = null;
            custTypeRq.FullName = new() { "VendorTypeQueryRq.FullName" };
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.FullName = null;
            custTypeRq.NameFilter = new();
            custTypeRq.MaxReturned = 99999;
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.NameFilter.MatchCriterion = MatchCriterion.None;
            custTypeRq.NameFilter.Name = "A";
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.NameRangeFilter = new();
            custTypeRq.NameRangeFilter.FromName = "A";
            custTypeRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.NameFilter = null;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            var model = new QryRqModel<VendorTypeQueryRq>();
            model.SetRequest(custTypeRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<VendorTypeQueryRq>"));
            Assert.IsTrue(custTypeRq.ToString().Contains("<VendorTypeQueryRq>"));
        }

        [TestMethod]
        public void TestVendorTypeAddRq()
        {
            VendorTypeAddRq custTypeRq = new();
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.Name = "VendorTypeAddRq.VendorType.Name";
            Assert.IsTrue(custTypeRq.IsEntityValid());

            var model = new AddRqModel<VendorTypeAddRq>("VendorTypeAdd");
            model.SetRequest(custTypeRq, "AddRq");
            Assert.IsTrue(custTypeRq.ToString().Contains("<VendorTypeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<VendorTypeAddRq>"));
        }
    }
}