using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void TestEmployeeQueryRq()
        {
            EmployeeQueryRq empRq = new();
            Assert.IsTrue(empRq.IsEntityValid());

            empRq.ListID = new() { "EmployeeQueryRq.ListID" };
            empRq.MaxReturned = -1;
            Assert.IsTrue(empRq.IsEntityValid());

            empRq.ListID = null;
            empRq.FullName = new() { "EmployeeQueryRq.FullName" };
            Assert.IsTrue(empRq.IsEntityValid());

            empRq.FullName = null;
            empRq.NameFilter = new();
            empRq.MaxReturned = 99999;
            Assert.IsFalse(empRq.IsEntityValid());

            empRq.NameFilter.MatchCriterion = MatchCriterion.None;
            empRq.NameFilter.Name = "A";
            Assert.IsFalse(empRq.IsEntityValid());

            empRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(empRq.IsEntityValid());

            var model = new QryRqModel<EmployeeQueryRq>();
            model.SetRequest(empRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<EmployeeQueryRq>"));
            Assert.IsTrue(empRq.ToString().Contains("<EmployeeQueryRq>"));
        }

        [TestMethod]
        public void TestEmployeeAddRq()
        {
            EmployeeAddRq empRq = new();
            Assert.IsTrue(empRq.IsEntityValid());

            empRq.FirstName = "EmployeeAddRq.FirstName";
            empRq.FirstName = "EmployeeAddRq.LastName";
            empRq.EmployeeType = EmployeeType.None;
            Assert.IsFalse(empRq.IsEntityValid());

            empRq.EmployeeType = EmployeeType.Regular;
            Assert.IsTrue(empRq.IsEntityValid());

            var model = new AddRqModel<EmployeeAddRq>("EmployeeAdd");
            model.SetRequest(empRq, "AddRq");
            Assert.IsTrue(empRq.ToString().Contains("<EmployeeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<EmployeeAddRq>"));
        }

        [TestMethod]
        public void TestEmployeeModRq()
        {
            EmployeeModRq empRq = new();
            Assert.IsFalse(empRq.IsEntityValid());

            empRq.ListID = "EmployeeModRq.ListID";
            empRq.EditSequence = "EmployeeModRq.EditSequence";
            Assert.IsTrue(empRq.IsEntityValid());

            empRq.EmployeeType = EmployeeType.None;
            Assert.IsFalse(empRq.IsEntityValid());

            empRq.EmployeeType = EmployeeType.Officer;
            Assert.IsTrue(empRq.IsEntityValid());

            var model = new ModRqModel<EmployeeModRq>("EmployeeMod");
            model.SetRequest(empRq, "ModRq");
            Assert.IsTrue(empRq.ToString().Contains("<EmployeeModRq>"));
            Assert.IsTrue(model.ToString().Contains("<EmployeeModRq>"));
        }
    }
}