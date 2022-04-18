using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class PriceLevelTests
    {
        [TestMethod]
        public void TestPriceLevelQueryRq()
        {
            PriceLevelQueryRq priceLevelRq = new();
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.ToString();

            priceLevelRq.ListID = new() { "PriceLevelQueryRq.ListID" };
            priceLevelRq.MaxReturned = -1;
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.ListID = null;
            priceLevelRq.FullName = new() { "PriceLevelQueryRq.FullName" };
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.FullName = null;
            priceLevelRq.NameFilter = new();
            priceLevelRq.MaxReturned = 99999;
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.NameFilter.MatchCriterion = MatchCriterion.None;
            priceLevelRq.NameFilter.Name = "A";
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.NameRangeFilter = new();
            priceLevelRq.NameRangeFilter.FromName = "A";
            priceLevelRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.NameFilter = null;
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            var model = new QryRqModel<PriceLevelQueryRq>();
            model.SetRequest(priceLevelRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<PriceLevelQueryRq>"));
            Assert.IsTrue(priceLevelRq.ToString().Contains("<PriceLevelQueryRq>"));
        }

        [TestMethod]
        public void TestPriceLevelAddRq()
        {
            PriceLevelAddRq priceLevelRq = new();
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.Name = "PriceLevelAddRq.Name";
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem = new();
            priceLevelRq.PriceLevelPerItem.Add(new());
            priceLevelRq.PriceLevelPerItem[0].Item = new();
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem[0].CustomPrice = 1.1M;
            priceLevelRq.PriceLevelPerItem[0].AdjustPercentage = "10%";
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem[0].CustomPrice = null;
            priceLevelRq.PriceLevelPerItem[0].AdjustRelativeTo = AdjustRelativeTo.None;
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem[0].AdjustRelativeTo = AdjustRelativeTo.Cost;
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            var model = new AddRqModel<PriceLevelAddRq>("PriceLevelAdd");
            model.SetRequest(priceLevelRq, "AddRq");
            Assert.IsTrue(priceLevelRq.ToString().Contains("<PriceLevelAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<PriceLevelAddRq>"));
        }

        [TestMethod]
        public void TestPriceLevelModRq()
        {
            PriceLevelModRq priceLevelRq = new();
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.ListID = "PriceLevelModRq.ListID";
            priceLevelRq.EditSequence = "PriceLevelModRq.EditSequence";
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.Name = "PriceLevelModRq.Name";
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem = new();
            priceLevelRq.PriceLevelPerItem.Add(new());
            priceLevelRq.PriceLevelPerItem[0].Item = new();
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem[0].CustomPrice = 1.1M;
            priceLevelRq.PriceLevelPerItem[0].AdjustPercentage = "10%";
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem[0].CustomPrice = null;
            priceLevelRq.PriceLevelPerItem[0].AdjustRelativeTo = AdjustRelativeTo.None;
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem[0].AdjustRelativeTo = AdjustRelativeTo.Cost;
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelFixedPercentage = "25.0";
            Assert.IsFalse(priceLevelRq.IsEntityValid());

            priceLevelRq.PriceLevelPerItem = null;
            Assert.IsTrue(priceLevelRq.IsEntityValid());

            var model = new ModRqModel<PriceLevelModRq>("PriceLevelMod");
            model.SetRequest(priceLevelRq, "ModRq");
            Assert.IsTrue(priceLevelRq.ToString().Contains("<PriceLevelModRq>"));
            Assert.IsTrue(model.ToString().Contains("<PriceLevelModRq>"));
        }
    }
}