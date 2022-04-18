using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class TimeTrackingTests
    {
        [TestMethod]
        public void TestTimeTrackingQueryRq()
        {
            TimeTrackingQueryRq timeTrackingRq = new();
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            timeTrackingRq.TxnID = new() { "TimeTrackingQueryRq.TxnID" };
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            timeTrackingRq.TxnID = null;
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            timeTrackingRq.TxnDateRangeFilter = new();
            timeTrackingRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            timeTrackingRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            timeTrackingRq.ModifiedDateRangeFilter = new();
            timeTrackingRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            timeTrackingRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(timeTrackingRq.IsEntityValid());

            timeTrackingRq.TxnDateRangeFilter = null;
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            var model = new QryRqModel<TimeTrackingQueryRq>();
            model.SetRequest(timeTrackingRq, "QryRq");
            Assert.IsTrue(timeTrackingRq.ToString().Contains("<TimeTrackingQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<TimeTrackingQueryRq>"));
        }

        [TestMethod]
        public void TestTimeTrackingAddRq()
        {
            TimeTrackingAddRq timeTrackingRq = new();
            Assert.IsFalse(timeTrackingRq.IsEntityValid());

            timeTrackingRq.TxnDate = DateTime.Now;
            timeTrackingRq.Entity = new();
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            timeTrackingRq.BillableStatus = BillStatus.Billable;
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            var model = new AddRqModel<TimeTrackingAddRq>("TimeTrackingAdd");
            model.SetRequest(timeTrackingRq, "AddRq");
            Assert.IsTrue(timeTrackingRq.ToString().Contains("<TimeTrackingAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<TimeTrackingAddRq>"));
        }

        [TestMethod]
        public void TestTimeTrackingModRq()
        {
            TimeTrackingModRq timeTrackingRq = new();
            Assert.IsFalse(timeTrackingRq.IsEntityValid());

            timeTrackingRq.TxnID = "TimeTrackingModRq.TxnID";
            timeTrackingRq.EditSequence = "TimeTrackingModRq.EditSequence";
            timeTrackingRq.TxnDate = DateTime.Now;
            timeTrackingRq.Entity = new();
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            timeTrackingRq.BillableStatus = BillStatus.Billable;
            Assert.IsTrue(timeTrackingRq.IsEntityValid());

            var model = new ModRqModel<TimeTrackingModRq>("TimeTrackingMod");
            model.SetRequest(timeTrackingRq, "ModRq");
            Assert.IsTrue(timeTrackingRq.ToString().Contains("<TimeTrackingModRq>"));
            Assert.IsTrue(model.ToString().Contains("<TimeTrackingModRq>"));
        }
    }
}