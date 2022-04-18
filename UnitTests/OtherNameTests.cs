using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class OtherNameTests
    {
        [TestMethod]
        public void TestOtherNameQueryRq()
        {
            OtherNameQueryRq otherNameRq = new();
            Assert.IsTrue(otherNameRq.IsEntityValid());

            otherNameRq.ListID = new() { "JobTypeQueryRq.ListID" };
            otherNameRq.MaxReturned = -1;
            Assert.IsTrue(otherNameRq.IsEntityValid());

            otherNameRq.ListID = null;
            otherNameRq.FullName = new() { "JobTypeQueryRq.FullName" };
            Assert.IsTrue(otherNameRq.IsEntityValid());

            otherNameRq.FullName = null;
            otherNameRq.NameFilter = new();
            otherNameRq.MaxReturned = 99999;
            Assert.IsFalse(otherNameRq.IsEntityValid());

            otherNameRq.NameFilter.MatchCriterion = MatchCriterion.None;
            otherNameRq.NameFilter.Name = "A";
            Assert.IsFalse(otherNameRq.IsEntityValid());

            otherNameRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(otherNameRq.IsEntityValid());

            otherNameRq.NameRangeFilter = new();
            otherNameRq.NameRangeFilter.FromName = "A";
            otherNameRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(otherNameRq.IsEntityValid());

            otherNameRq.NameFilter = null;
            Assert.IsTrue(otherNameRq.IsEntityValid());

            var model = new QryRqModel<OtherNameQueryRq>();
            model.SetRequest(otherNameRq, "QryRq");
            Assert.IsTrue(otherNameRq.ToString().Contains("<OtherNameQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<OtherNameQueryRq>"));
        }

        [TestMethod]
        public void TestOtherNameAddRq()
        {
            OtherNameAddRq otherNameRq = new();
            Assert.IsFalse(otherNameRq.IsEntityValid());

            otherNameRq.Name = "OtherNameAddRq.FullName";
            otherNameRq.IsActive = true;
            Assert.IsTrue(otherNameRq.IsEntityValid());

            var model = new AddRqModel<OtherNameAddRq>("OtherNameAdd");
            model.SetRequest(otherNameRq, "AddRq");
            Assert.IsTrue(otherNameRq.ToString().Contains("<OtherNameAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<OtherNameAddRq>"));
        }

        [TestMethod]
        public void TestOtherNameModRq()
        {
            OtherNameModRq otherNameRq = new();
            Assert.IsFalse(otherNameRq.IsEntityValid());

            otherNameRq.ListID = "OtherNameAddRq.ListID";
            otherNameRq.EditSequence = "OtherNameAddRq.EditSequence";
            Assert.IsFalse(otherNameRq.IsEntityValid());

            otherNameRq.Name = "OtherNameAddRq.Name";
            otherNameRq.IsActive = true;
            Assert.IsTrue(otherNameRq.IsEntityValid());

            var model = new ModRqModel<OtherNameModRq>("OtherNameMod");
            model.SetRequest(otherNameRq, "ModRq");
            Assert.IsTrue(otherNameRq.ToString().Contains("<OtherNameModRq>"));
            Assert.IsTrue(model.ToString().Contains("<OtherNameModRq>"));
        }
    }
}
