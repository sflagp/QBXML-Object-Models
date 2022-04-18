using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class ShipMethodTests
    {
        [TestMethod]
        public void TestShipMethodQueryRq()
        {
            ShipMethodQueryRq custTypeRq = new();
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = new() { "ShipMethodQueryRq.ListID" };
            custTypeRq.MaxReturned = -1;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = null;
            custTypeRq.FullName = new() { "ShipMethodQueryRq.FullName" };
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

            var model = new QryRqModel<ShipMethodQueryRq>();
            model.SetRequest(custTypeRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<ShipMethodQueryRq>"));
            Assert.IsTrue(custTypeRq.ToString().Contains("<ShipMethodQueryRq>"));
        }

        [TestMethod]
        public void TestShipMethodAddRq()
        {
            ShipMethodAddRq custTypeRq = new();
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.Name = "ShipMethodAddRq.ShipMethod.Name";
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.Name = "ShipMethodName";
            Assert.IsTrue(custTypeRq.IsEntityValid());

            var model = new AddRqModel<ShipMethodAddRq>("ShipMethodAdd");
            model.SetRequest(custTypeRq, "AddRq");
            Assert.IsTrue(custTypeRq.ToString().Contains("<ShipMethodAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<ShipMethodAddRq>"));
        }
    }
}