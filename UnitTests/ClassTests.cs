using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class ClassTests
    {
        [TestMethod]
        public void TestClassQueryRq()
        {
            ClassQueryRq clsRq = new();
            Assert.IsTrue(clsRq.IsEntityValid());

            clsRq.ListID = new() { "ClassQueryRq.ListID" };
            clsRq.MaxReturned = -1;
            Assert.IsTrue(clsRq.IsEntityValid());

            clsRq.ListID = null;
            clsRq.FullName = new() { "ClassQueryRq.FullName" };
            Assert.IsTrue(clsRq.IsEntityValid());

            clsRq.FullName = null;
            clsRq.NameFilter = new();
            clsRq.MaxReturned = 99999;
            Assert.IsFalse(clsRq.IsEntityValid());

            clsRq.NameFilter.MatchCriterion = MatchCriterion.None;
            clsRq.NameFilter.Name = "A";
            Assert.IsFalse(clsRq.IsEntityValid());

            clsRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(clsRq.IsEntityValid());

            var model = new QryRqModel<ClassQueryRq>();
            model.SetRequest(clsRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<ClassQueryRq>"));
            Assert.IsTrue(clsRq.ToString().Contains("<ClassQueryRq>"));
        }

        [TestMethod]
        public void TestClassAddRq()
        {
            ClassAddRq clsRq = new();
            Assert.IsFalse(clsRq.IsEntityValid());

            clsRq.Name = "ClassAddRq";
            Assert.IsTrue(clsRq.IsEntityValid());

            clsRq.Parent = new() { FullName = "ClassAddRq.Parent.FullName" };
            Assert.IsTrue(clsRq.IsEntityValid());

            var model = new AddRqModel<ClassAddRq>("ClassAdd");
            model.SetRequest(clsRq, "AddRq");
            Assert.IsTrue(clsRq.ToString().Contains("<ClassAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<ClassAddRq>"));
        }

        [TestMethod]
        public void TestClassModRq()
        {
            ClassModRq clsRq = new();
            Assert.IsFalse(clsRq.IsEntityValid());

            clsRq.ListID = "ClassModRq.ListID";
            clsRq.EditSequence = "ClassModRq.EditSequence";
            clsRq.Name = "ClassModRq";
            Assert.IsTrue(clsRq.IsEntityValid());

            clsRq.Name = null;
            Assert.IsTrue(clsRq.IsEntityValid());

            clsRq.Name = "ClassModRq";
            Assert.IsTrue(clsRq.IsEntityValid());

            var model = new ModRqModel<ClassModRq>("ClassMod");
            model.SetRequest(clsRq, "ModRq");
            Assert.IsTrue(clsRq.ToString().Contains("<ClassModRq>"));
            Assert.IsTrue(model.ToString().Contains("<ClassModRq>"));
        }
    }
}