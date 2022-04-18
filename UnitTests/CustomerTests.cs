using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void TestCustomerQueryRq()
        {
            CustomerQueryRq custRq = new();
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.ListID = new() { "CustomerQueryRq.ListID" };
            custRq.MaxReturned = -1;
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.ListID = null;
            custRq.FullName = new() { "CustomerQueryRq.FullName" };
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.FullName = null;
            custRq.NameFilter = new();
            custRq.MaxReturned = 99999;
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.NameFilter.MatchCriterion = MatchCriterion.None;
            custRq.NameFilter.Name = "A";
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.NameRangeFilter = new();
            custRq.NameRangeFilter.FromName = "A";
            custRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.NameFilter = null;
            Assert.IsTrue(custRq.IsEntityValid());

            var model = new QryRqModel<CustomerQueryRq>();
            model.SetRequest(custRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<CustomerQueryRq>"));
            Assert.IsTrue(custRq.ToString().Contains("<CustomerQueryRq>"));
        }

        [TestMethod]
        public void TestCustomerAddRq()
        {
            CustomerAddRq custRq = new();
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.Name = "CustomerAddRq";
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact = new();
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.AdditionalContact = null;
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.PreferredDeliveryMethod = PreferredDeliveryMethod.None;
            Assert.IsTrue(custRq.IsEntityValid());

            var model = new AddRqModel<CustomerAddRq>("CustomerAdd");
            model.SetRequest(custRq, "AddRq");
            Assert.IsTrue(custRq.ToString().Contains("<CustomerAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CustomerAddRq>"));
        }

        [TestMethod]
        public void TestCustomerModRq()
        {
            CustomerModRq custRq = new();
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.ListID = "CustomerModRq.ListID";
            custRq.EditSequence = "CustomerModRq.EditSequence";
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.Name = "CustomerAddRq";
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact = new();
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.AdditionalContact.Add(new() { ContactName = "CustomerAddRq.ContactName", ContactValue = "CustomerAddRq.ContactValue" });
            Assert.IsFalse(custRq.IsEntityValid());

            custRq.AdditionalContact = null;
            Assert.IsTrue(custRq.IsEntityValid());

            custRq.PreferredDeliveryMethod = PreferredDeliveryMethod.None;
            Assert.IsTrue(custRq.IsEntityValid());

            var model = new ModRqModel<CustomerModRq>("CustomerMod");
            model.SetRequest(custRq, "ModRq");
            Assert.IsTrue(custRq.ToString().Contains("<CustomerModRq>"));
            Assert.IsTrue(model.ToString().Contains("<CustomerModRq>"));
        }
    }

    [TestClass]
    public class CustomerMsgTests
    {
        [TestMethod]
        public void TestCustomerMsgQueryRq()
        {
            CustomerMsgQueryRq custMsgRq = new();
            Assert.IsTrue(custMsgRq.IsEntityValid());

            custMsgRq.ListID = new() { "CustomerTypeQueryRq.ListID" };
            custMsgRq.MaxReturned = -1;
            Assert.IsTrue(custMsgRq.IsEntityValid());

            custMsgRq.ListID = null;
            custMsgRq.FullName = new() { "CustomerTypeQueryRq.FullName" };
            Assert.IsTrue(custMsgRq.IsEntityValid());

            custMsgRq.FullName = null;
            custMsgRq.NameFilter = new();
            custMsgRq.MaxReturned = 99999;
            Assert.IsFalse(custMsgRq.IsEntityValid());

            custMsgRq.NameFilter.MatchCriterion = MatchCriterion.None;
            custMsgRq.NameFilter.Name = "A";
            Assert.IsFalse(custMsgRq.IsEntityValid());

            custMsgRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(custMsgRq.IsEntityValid());

            custMsgRq.NameRangeFilter = new();
            custMsgRq.NameRangeFilter.FromName = "A";
            custMsgRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(custMsgRq.IsEntityValid());

            custMsgRq.NameFilter = null;
            Assert.IsTrue(custMsgRq.IsEntityValid());

            var model = new QryRqModel<CustomerMsgQueryRq>();
            model.SetRequest(custMsgRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<CustomerMsgQueryRq>"));
            Assert.IsTrue(custMsgRq.ToString().Contains("<CustomerMsgQueryRq>"));
        }

        [TestMethod]
        public void TestCustomerMsgAddRq()
        {
            CustomerMsgAddRq custMsgRq = new();
            Assert.IsFalse(custMsgRq.IsEntityValid());

            custMsgRq.Name = "CustomerTypeAddRq.CustomerType.Name";
            Assert.IsTrue(custMsgRq.IsEntityValid());

            var model = new AddRqModel<CustomerMsgAddRq>("CustomerMsgAdd");
            model.SetRequest(custMsgRq, "AddRq");
            Assert.IsTrue(custMsgRq.ToString().Contains("<CustomerMsgAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CustomerMsgAddRq>"));
        }
    }

    [TestClass]
    public class CustomerTypeTests
    {
        [TestMethod]
        public void TestCustomerTypeQueryRq()
        {
            CustomerTypeQueryRq custTypeRq = new();
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = new() { "CustomerTypeQueryRq.ListID" };
            custTypeRq.MaxReturned = -1;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = null;
            custTypeRq.FullName = new() { "CustomerTypeQueryRq.FullName" };
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

            var model = new QryRqModel<CustomerTypeQueryRq>();
            model.SetRequest(custTypeRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<CustomerTypeQueryRq>"));
            Assert.IsTrue(custTypeRq.ToString().Contains("<CustomerTypeQueryRq>"));
        }

        [TestMethod]
        public void TestCustomerTypeAddRq()
        {
            CustomerTypeAddRq custTypeRq = new();
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.Name = "CustomerType.Name";
            Assert.IsTrue(custTypeRq.IsEntityValid());

            var model = new AddRqModel<CustomerTypeAddRq>("CustomerTypeAdd");
            model.SetRequest(custTypeRq, "AddRq");
            Assert.IsTrue(custTypeRq.ToString().Contains("<CustomerTypeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CustomerTypeAddRq>"));
        }
    }
}