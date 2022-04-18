using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class TransferInventoryTests
    {
        [TestMethod]
        public void TestTransferInventoryQueryRq()
        {
            TransferInventoryQueryRq transferInventoryRq = new();
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnID = new() { "InventoryAdjustmentQueryRq.TxnID" };
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnID = null;
            transferInventoryRq.RefNumber = new() { "InventoryAdjustmentQueryRq.FullName" };
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnDateRangeFilter = new();
            transferInventoryRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            transferInventoryRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.ModifiedDateRangeFilter = new();
            transferInventoryRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            transferInventoryRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnDateRangeFilter = null;
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            var model = new QryRqModel<TransferInventoryQueryRq>();
            model.SetRequest(transferInventoryRq, "QryRq");
            Assert.IsTrue(transferInventoryRq.ToString().Contains("<TransferInventoryQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferInventoryQueryRq>"));
        }

        [TestMethod]
        public void TestTransferInventoryAddRq()
        {
            TransferInventoryAddRq transferInventoryRq = new();
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnDate = DateTime.Now;
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.FromInventorySite = new();
            transferInventoryRq.ToInventorySite = new();
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            var model = new AddRqModel<TransferInventoryAddRq>("TransferInventoryAdd");
            model.SetRequest(transferInventoryRq, "AddRq");
            Assert.IsTrue(transferInventoryRq.ToString().Contains("<TransferInventoryAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferInventoryAddRq>"));
        }

        [TestMethod]
        public void TestTransferInventoryModRq()
        {
            TransferInventoryModRq transferInventoryRq = new();
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnID = "TransferInventoryQueryRq.TxnID";
            transferInventoryRq.EditSequence = "TransferInventoryQueryRq.EditSequence";
            transferInventoryRq.TxnDate = DateTime.Now;
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine = new();
            transferInventoryRq.TransferInventoryLine.Add(new() { TxnLineID = "TransferInventoryModRq.TxnLineID" });
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine[0].SerialNumber = "TransferInventoryModRq.TransferInventoryLine.SerialNumber";
            transferInventoryRq.TransferInventoryLine[0].LotNumber = "TransferInventoryModRq.TransferInventoryLine.LotNumber";
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine.Clear();
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            var model = new ModRqModel<TransferInventoryModRq>("TransferInventoryMod");
            model.SetRequest(transferInventoryRq, "ModRq");
            Assert.IsTrue(transferInventoryRq.ToString().Contains("<TransferInventoryModRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferInventoryModRq>"));
        }
    }

    [TestClass]
    public class InventoryAdjustmentTests
    {
        [TestMethod]
        public void TestInventoryAdjustmentQueryRq()
        {
            InventoryAdjustmentQueryRq inventoryAdjustmentRq = new();
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.TxnID = new() { "InventoryAdjustmentQueryRq.TxnID" };
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.TxnID = null;
            inventoryAdjustmentRq.RefNumber = new() { "InventoryAdjustmentQueryRq.FullName" };
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.TxnDateRangeFilter = new();
            inventoryAdjustmentRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            inventoryAdjustmentRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.ModifiedDateRangeFilter = new();
            inventoryAdjustmentRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            inventoryAdjustmentRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.TxnDateRangeFilter = null;
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            var model = new QryRqModel<InventoryAdjustmentQueryRq>();
            model.SetRequest(inventoryAdjustmentRq, "QryRq");
            Assert.IsTrue(inventoryAdjustmentRq.ToString().Contains("<InventoryAdjustmentQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<InventoryAdjustmentQueryRq>"));
        }

        [TestMethod]
        public void TestInventoryAdjustmentAddRq()
        {
            InventoryAdjustmentAddRq inventoryAdjustmentRq = new();
            Assert.IsFalse(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.Account = new();
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd = new();
            inventoryAdjustmentRq.InventoryAdjustmentLineAdd.Add(new());
            Assert.IsFalse(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].Item = new();
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].QuantityAdjustment = new();
            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].ValueAdjustment = new();
            Assert.IsFalse(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].ValueAdjustment = null;
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            var model = new AddRqModel<InventoryAdjustmentAddRq>("InventoryAdjustmentAdd");
            model.SetRequest(inventoryAdjustmentRq, "AddRq");
            Assert.IsTrue(inventoryAdjustmentRq.ToString().Contains("<InventoryAdjustmentAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<InventoryAdjustmentAddRq>"));
        }

        [TestMethod]
        public void TestInventoryAdjustmentModRq()
        {
            TransferInventoryModRq transferInventoryRq = new();
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnID = "TransferInventoryQueryRq.TxnID";
            transferInventoryRq.EditSequence = "TransferInventoryQueryRq.EditSequence";
            transferInventoryRq.TxnDate = DateTime.Now;
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine = new();
            transferInventoryRq.TransferInventoryLine.Add(new() { TxnLineID = "TransferInventoryModRq.TxnLineID" });
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine[0].SerialNumber = "TransferInventoryModRq.TransferInventoryLine.SerialNumber";
            transferInventoryRq.TransferInventoryLine[0].LotNumber = "TransferInventoryModRq.TransferInventoryLine.LotNumber";
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine.Clear();
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            var model = new ModRqModel<TransferInventoryModRq>("TransferInventoryMod");
            model.SetRequest(transferInventoryRq, "ModRq");
            Assert.IsTrue(transferInventoryRq.ToString().Contains("<TransferInventoryModRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferInventoryModRq>"));
        }
    }

    [TestClass]
    public class InventorySiteTests
    {
        [TestMethod]
        public void TestTransferInventoryQueryRq()
        {
            InventorySiteQueryRq inventorySiteRq = new();
            Assert.IsTrue(inventorySiteRq.IsEntityValid());

            inventorySiteRq.ListID = new() { "InventorySiteQueryRq.ListID" };
            Assert.IsTrue(inventorySiteRq.IsEntityValid());

            inventorySiteRq.ListID = null;
            inventorySiteRq.FullName = new() { "InventorySiteQueryRq.FullName" };
            Assert.IsTrue(inventorySiteRq.IsEntityValid());

            inventorySiteRq.NameFilter = new();
            inventorySiteRq.NameFilter.MatchCriterion = MatchCriterion.None;
            inventorySiteRq.NameFilter.Name = "A";
            Assert.IsFalse(inventorySiteRq.IsEntityValid());

            inventorySiteRq.NameFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(inventorySiteRq.IsEntityValid());

            inventorySiteRq.NameRangeFilter = new();
            inventorySiteRq.NameRangeFilter.FromName = "A";
            inventorySiteRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(inventorySiteRq.IsEntityValid());

            inventorySiteRq.NameFilter = null;
            Assert.IsTrue(inventorySiteRq.IsEntityValid());

            var model = new QryRqModel<InventorySiteQueryRq>();
            model.SetRequest(inventorySiteRq, "QryRq");
            Assert.IsTrue(inventorySiteRq.ToString().Contains("<InventorySiteQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<InventorySiteQueryRq>"));
        }

        [TestMethod]
        public void TestTransferInventoryAddRq()
        {
            InventoryAdjustmentAddRq inventoryAdjustmentRq = new();
            Assert.IsFalse(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.Account = new();
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd = new();
            inventoryAdjustmentRq.InventoryAdjustmentLineAdd.Add(new());
            Assert.IsFalse(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].Item = new();
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].QuantityAdjustment = new();
            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].ValueAdjustment = new();
            Assert.IsFalse(inventoryAdjustmentRq.IsEntityValid());

            inventoryAdjustmentRq.InventoryAdjustmentLineAdd[0].ValueAdjustment = null;
            Assert.IsTrue(inventoryAdjustmentRq.IsEntityValid());

            var model = new AddRqModel<InventoryAdjustmentAddRq>("InventoryAdjustmentAdd");
            model.SetRequest(inventoryAdjustmentRq, "AddRq");
            Assert.IsTrue(inventoryAdjustmentRq.ToString().Contains("<InventoryAdjustmentAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<InventoryAdjustmentAddRq>"));
        }

        [TestMethod]
        public void TestTransferInventoryModRq()
        {
            TransferInventoryModRq transferInventoryRq = new();
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TxnID = "TransferInventoryQueryRq.TxnID";
            transferInventoryRq.EditSequence = "TransferInventoryQueryRq.EditSequence";
            transferInventoryRq.TxnDate = DateTime.Now;
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine = new();
            transferInventoryRq.TransferInventoryLine.Add(new() { TxnLineID = "TransferInventoryModRq.TxnLineID" });
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine[0].SerialNumber = "TransferInventoryModRq.TransferInventoryLine.SerialNumber";
            transferInventoryRq.TransferInventoryLine[0].LotNumber = "TransferInventoryModRq.TransferInventoryLine.LotNumber";
            Assert.IsFalse(transferInventoryRq.IsEntityValid());

            transferInventoryRq.TransferInventoryLine.Clear();
            Assert.IsTrue(transferInventoryRq.IsEntityValid());

            var model = new ModRqModel<TransferInventoryModRq>("TransferInventoryMod");
            model.SetRequest(transferInventoryRq, "ModRq");
            Assert.IsTrue(transferInventoryRq.ToString().Contains("<TransferInventoryModRq>"));
            Assert.IsTrue(model.ToString().Contains("<TransferInventoryModRq>"));
        }
    }
}