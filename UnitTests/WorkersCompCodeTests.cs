using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class WorkersCompCodeTests
    {
        [TestMethod]
        public void TestWorkersCompCodeQueryRq()
        {
            WorkersCompCodeQueryRq worksCompCodeRq = new();
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.ListID = new() { "WorkersCompCodeQueryRq.ListID" };
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.ListID = null;
            worksCompCodeRq.FullName = new() { "WorkersCompCodeQueryRq.FullName" };
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.NameFilter = new();
            worksCompCodeRq.NameFilter.MatchCriterion = MatchCriterion.None;
            worksCompCodeRq.NameFilter.Name = "A";
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.NameFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.NameRangeFilter = new();
            worksCompCodeRq.NameRangeFilter.FromName = "A";
            worksCompCodeRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.NameFilter = null;
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            var model = new QryRqModel<WorkersCompCodeQueryRq>();
            model.SetRequest(worksCompCodeRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<WorkersCompCodeQueryRq>"));
            Assert.IsTrue(worksCompCodeRq.ToString().Contains("<WorkersCompCodeQueryRq>"));
        }

        [TestMethod]
        public void TestWorkersCompCodeAddRq()
        {
            WorkersCompCodeAddRq worksCompCodeRq = new();
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.Name = "WorkersCompCodeAddRq";
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.RateEntry = new();
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.Name = "WCCodeAddRq";
            worksCompCodeRq.RateEntry.Add(new() { Rate = "250.00", EffectiveDate = DateTime.Now });
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            var model = new AddRqModel<WorkersCompCodeAddRq>("WorkersCompCodeAdd");
            model.SetRequest(worksCompCodeRq, "AddRq");
            Assert.IsTrue(worksCompCodeRq.ToString().Contains("<WorkersCompCodeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<WorkersCompCodeAddRq>"));
        }

        [TestMethod]
        public void TestWorkersCompCodeModRq()
        {
            WorkersCompCodeModRq worksCompCodeRq = new();
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.ListID = "WorkersCompCodeModRq.ListID";
            worksCompCodeRq.EditSequence = "WorkersCompCodeModRq.EditSequence";
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.Name = "WorkersCompCodeAddRq";
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.Name = "WCCodeAddRq";
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.RateEntry = new();
            worksCompCodeRq.RateEntry.Add(new());
            Assert.IsFalse(worksCompCodeRq.IsEntityValid());

            worksCompCodeRq.RateEntry.Clear();
            worksCompCodeRq.RateEntry.Add(new() { Rate = "250.00", EffectiveDate = DateTime.Now });
            Assert.IsTrue(worksCompCodeRq.IsEntityValid());

            var model = new ModRqModel<WorkersCompCodeModRq>("WorkersCompCodeMod");
            model.SetRequest(worksCompCodeRq, "ModRq");
            Assert.IsTrue(worksCompCodeRq.ToString().Contains("<WorkersCompCodeModRq>"));
            Assert.IsTrue(model.ToString().Contains("<WorkersCompCodeModRq>"));
        }
    }
}