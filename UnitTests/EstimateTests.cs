using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class EstimateTests
    {
        [TestMethod]
        public void TestEstimateQueryRq()
        {
            EstimateQueryRq estimateRq = new();
            Assert.IsTrue(estimateRq.IsEntityValid());

            estimateRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(estimateRq.IsEntityValid());

            var model = new QryRqModel<EstimateQueryRq>();
            model.SetRequest(estimateRq, "QryRq");
            Assert.IsTrue(estimateRq.ToString().Contains("<EstimateQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<EstimateQueryRq>"));
        }

        [TestMethod]
        public void TestEstimateAddRq()
        {
            EstimateAddRq estimateRq = new();
            Assert.IsFalse(estimateRq.IsEntityValid());

            estimateRq.Customer = new();
            Assert.IsTrue(estimateRq.IsEntityValid());

            estimateRq.EstimateLine = new();
            estimateRq.EstimateLineGroup = new();
            Assert.IsFalse(estimateRq.IsEntityValid());

            estimateRq.EstimateLineGroup = null;
            Assert.IsTrue(estimateRq.IsEntityValid());

            var model = new AddRqModel<EstimateAddRq>("EstimateAdd");
            model.SetRequest(estimateRq, "AddRq");
            Assert.IsTrue(estimateRq.ToString().Contains("<EstimateAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<EstimateAddRq>"));
        }

        [TestMethod]
        public void TestEstimateModRq()
        {
            EstimateModRq estimateRq = new();
            Assert.IsFalse(estimateRq.IsEntityValid());

            estimateRq.TxnID = "EstimateModRq.TxnID";
            estimateRq.EditSequence = null;
            estimateRq.TxnDate = DateTime.Now;
            Assert.IsFalse(estimateRq.IsEntityValid());

            estimateRq.TxnID = null;
            estimateRq.EditSequence = "EstimateModRq.EditSequence";
            Assert.IsFalse(estimateRq.IsEntityValid());

            estimateRq.Customer = new();
            estimateRq.TxnID = "EstimateModRq.TxnID";
            estimateRq.EditSequence = "EstimateModRq.EditSequence";
            Assert.IsTrue(estimateRq.IsEntityValid());

            estimateRq.EstimateLine = new();
            estimateRq.EstimateLine.Add( new() { TxnLineID = "EstimateLineGroup.TxnLineID" });
            estimateRq.EstimateLineGroup = new() { TxnLineID = "EstimateLineGroup.TxnLineID" };
            Assert.IsFalse(estimateRq.IsEntityValid());

            estimateRq.EstimateLineGroup = null;
            Assert.IsTrue(estimateRq.IsEntityValid());

            var model = new ModRqModel<EstimateModRq>("EstimateMod");
            model.SetRequest(estimateRq, "ModRq");
            Assert.IsTrue(estimateRq.ToString().Contains("<EstimateModRq>"));
            Assert.IsTrue(model.ToString().Contains("<EstimateModRq>"));
        }
    }
}
