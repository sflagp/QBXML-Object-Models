using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class VehicleTests
    {
        [TestMethod]
        public void TestVehicleQueryRq()
        {
            VehicleQueryRq vehicleRq = new();
            Assert.IsTrue(vehicleRq.IsEntityValid());

            vehicleRq.ListID = new() { "VehicleQueryRq.ListID" };
            vehicleRq.MaxReturned = -1;
            Assert.IsTrue(vehicleRq.IsEntityValid());

            vehicleRq.ListID = null;
            vehicleRq.FullName = new() { "VehicleQueryRq.FullName" };
            Assert.IsTrue(vehicleRq.IsEntityValid());

            vehicleRq.FullName = null;
            vehicleRq.NameFilter = new();
            vehicleRq.MaxReturned = 99999;
            Assert.IsFalse(vehicleRq.IsEntityValid());

            vehicleRq.NameFilter.MatchCriterion = MatchCriterion.None;
            vehicleRq.NameFilter.Name = "A";
            Assert.IsFalse(vehicleRq.IsEntityValid());

            vehicleRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(vehicleRq.IsEntityValid());

            vehicleRq.NameRangeFilter = new();
            vehicleRq.NameRangeFilter.FromName = "A";
            vehicleRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(vehicleRq.IsEntityValid());

            vehicleRq.NameFilter = null;
            Assert.IsTrue(vehicleRq.IsEntityValid());

            var model = new QryRqModel<VehicleQueryRq>();
            model.SetRequest(vehicleRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<VehicleQueryRq>"));
            Assert.IsTrue(vehicleRq.ToString().Contains("<VehicleQueryRq>"));
        }

        [TestMethod]
        public void TestVehicleAddRq()
        {
            VehicleAddRq vehicleRq = new();
            Assert.IsFalse(vehicleRq.IsEntityValid());

            vehicleRq.Name = "VehicleAddRq";
            Assert.IsTrue(vehicleRq.IsEntityValid());

            vehicleRq.IsActive = true;
            vehicleRq.Desc = "VehicleAddRq.Desc";
            Assert.IsTrue(vehicleRq.IsEntityValid());

            var model = new AddRqModel<VehicleAddRq>("VehicleAdd");
            model.SetRequest(vehicleRq, "AddRq");
            Assert.IsTrue(vehicleRq.ToString().Contains("<VehicleAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<VehicleAddRq>"));
        }

        [TestMethod]
        public void TestVehicleModRq()
        {
            VehicleModRq vehicleRq = new();
            Assert.IsFalse(vehicleRq.IsEntityValid());

            vehicleRq.ListID = "VehicleModRq.ListID";
            vehicleRq.EditSequence = "VehicleModRq.EditSequence";
            Assert.IsTrue(vehicleRq.IsEntityValid());

            vehicleRq.Name = "VehicleModRq";
            Assert.IsTrue(vehicleRq.IsEntityValid());

            vehicleRq.IsActive = true;
            vehicleRq.Desc = "VehicleModRq.Desc";
            Assert.IsTrue(vehicleRq.IsEntityValid());

            var model = new ModRqModel<VehicleModRq>("VehicleMod");
            model.SetRequest(vehicleRq, "ModRq");
            Assert.IsTrue(vehicleRq.ToString().Contains("<VehicleModRq>"));
            Assert.IsTrue(model.ToString().Contains("<VehicleModRq>"));
        }
    }

    [TestClass]
    public class VehicleMileageTests
    {
        [TestMethod]
        public void TestVehicleMileageQueryRq()
        {
            VehicleMileageQueryRq vehicleMileageRq = new();
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.TxnID = "VehicleMileageQueryRq.TxnID";
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.ModifiedDateRangeFilter = new();
            Assert.IsFalse(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.TxnID = null;
            vehicleMileageRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            vehicleMileageRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.TxnDateRangeFilter = new();
            vehicleMileageRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            vehicleMileageRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsFalse(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.ModifiedDateRangeFilter = null;
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            var model = new QryRqModel<VehicleMileageQueryRq>();
            model.SetRequest(vehicleMileageRq, "QryRq");
            Assert.IsTrue(vehicleMileageRq.ToString().Contains("<VehicleMileageQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<VehicleMileageQueryRq>"));
        }

        [TestMethod]
        public void TestVehicleMileageAddRq()
        {
            VehicleMileageAddRq vehicleMileageRq = new();
            Assert.IsFalse(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.Vehicle = new();
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.OdometerStart = "100000";
            Assert.IsFalse(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.OdometerEnd = "100125";
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.TotalMiles = "125";
            Assert.IsFalse(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.OdometerStart = null;
            vehicleMileageRq.OdometerEnd = null;
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            vehicleMileageRq.BillableStatus = BillStatus.HasBeenBilled;
            Assert.IsTrue(vehicleMileageRq.IsEntityValid());

            var model = new AddRqModel<VehicleMileageAddRq>("VehicleMileageAdd");
            model.SetRequest(vehicleMileageRq, "AddRq");
            Assert.IsTrue(vehicleMileageRq.ToString().Contains("<VehicleMileageAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<VehicleMileageAddRq>"));
        }
    }
}