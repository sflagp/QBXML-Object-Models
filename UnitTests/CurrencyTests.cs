using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod]
        public void TestCurrencyQueryRq()
        {
            CurrencyQueryRq acctRq = new();
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.ListID = new() { "CurrencyQueryRq.ListID" };
            acctRq.MaxReturned = -1;
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.ListID = null;
            acctRq.FullName = new() { "CurrencyQueryRq.FullName" };
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.FullName = null;
            acctRq.NameFilter = new();
            acctRq.MaxReturned = 99999;
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.NameFilter.MatchCriterion = MatchCriterion.None;
            acctRq.NameFilter.Name = "A";
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(acctRq.IsEntityValid());

            var model = new QryRqModel<CurrencyQueryRq>();
            model.SetRequest(acctRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<CurrencyQueryRq>"));
            Assert.IsTrue(acctRq.ToString().Contains("<CurrencyQueryRq>"));
        }

        [TestMethod]
        public void TestCurrencyAddRq()
        {
            CurrencyAddRq acctRq = new();
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.Name = "CurrencyAddRq.Name";
            acctRq.CurrencyCode = "CurrencyAddRq.CurrencyCode";
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.CurrencyCode = "CAC";
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.CurrencyFormat = new();
            acctRq.CurrencyFormat.DecimalPlaces = DecimalPlaces.None;
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.CurrencyFormat.DecimalPlaces = DecimalPlaces.Two;
            Assert.IsTrue(acctRq.IsEntityValid());

            var model = new AddRqModel<CurrencyAddRq>("CurrencyAdd");
            model.SetRequest(acctRq, "AddRq");
            Assert.IsTrue(acctRq.ToString().Contains("<CurrencyAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CurrencyAddRq>"));
        }

        [TestMethod]
        public void TestCurrencyModRq()
        {
            CurrencyModRq acctRq = new();
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.ListID = "CurrencyModRq.ListID";
            acctRq.EditSequence = "CurrencyModRq.EditSequence";
            acctRq.Name = "CurrencyAddRq.Name";
            acctRq.CurrencyFormat = new();
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.CurrencyFormat.DecimalPlaces = DecimalPlaces.None;
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.CurrencyFormat.DecimalPlaces = DecimalPlaces.Two;
            Assert.IsTrue(acctRq.IsEntityValid());

            acctRq.CurrencyCode = "CurrencyModRq.CurrencyCode";
            Assert.IsFalse(acctRq.IsEntityValid());

            acctRq.CurrencyCode = "CAC";
            Assert.IsTrue(acctRq.IsEntityValid());

            var model = new ModRqModel<CurrencyModRq>("CurrencyMod");
            model.SetRequest(acctRq, "ModRq");
            Assert.IsTrue(acctRq.ToString().Contains("<CurrencyModRq>"));
            Assert.IsTrue(model.ToString().Contains("<CurrencyModRq>"));
        }
    }
}