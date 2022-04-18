using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestAccountQueryRq()
        {
            AccountQueryRq acctRq = new();
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.ListID = new() { "AccountQueryRq.ListID" };
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.ListID = null;
            acctRq.FullName = new() { "AccountQueryRq.FullName" };
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.NameFilter = new();
            acctRq.NameFilter.MatchCriterion = MatchCriterion.None;
            acctRq.NameFilter.Name = "A";
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.NameFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.NameRangeFilter = new();
            acctRq.NameRangeFilter.FromName = "A";
            acctRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.FullName = null;
            acctRq.NameFilter = null;
            acctRq.ToString();
            Assert.IsTrue(acctRq.IsEntityValid());

            var model = new QryRqModel<AccountQueryRq>();
            model.SetRequest(acctRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<AccountQueryRq>"));
            Assert.IsTrue(acctRq.ToString().Contains("<AccountQueryRq>"));
        }

        [TestMethod]
        public void TestAccountAddRq()
        {
            AccountAddRq acctRq = new();
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.Name = "AccountAddRq";
            acctRq.AccountType = AccountType.Income;
            Assert.IsTrue(acctRq.IsEntityValid());
            
            var model = new AddRqModel<AccountAddRq>("AccountAdd");
            model.SetRequest(acctRq, "AddRq");
            Assert.IsTrue(acctRq.ToString().Contains("<AccountAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<AccountAddRq>"));
        }

        [TestMethod]
        public void TestAccountModRq()
        {
            AccountModRq acctRq = new();
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.ListID = "AccountModRq.ListID";
            acctRq.EditSequence = "AccountModRq.EditSequence";
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.AccountType = AccountType.Bank;
            Assert.IsTrue(acctRq.IsEntityValid());

            var model = new ModRqModel<AccountModRq>("AccountMod");
            model.SetRequest(acctRq, "ModRq");
            Assert.IsTrue(acctRq.ToString().Contains("<AccountModRq>"));
            Assert.IsTrue(model.ToString().Contains("<AccountModRq>"));
        }
    }
}