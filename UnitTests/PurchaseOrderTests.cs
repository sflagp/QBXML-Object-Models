using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class PurchaseOrderTests
    {
        [TestMethod]
        public void TestPurchaseOrderModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                PurchaseOrderRs qryRs, addRs = new(""), modRs;
                PurchaseOrderAddRq addRq = new();
                PurchaseOrderModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                PurchaseOrderQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalPurchaseOrders == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    accountsRq.AccountType = AccountType.AccountsReceivable;
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto account = accounts.Accounts[rdm.Next(0, accounts.Accounts.Count)];

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    ItemInventoryQueryRq itemsRq = new() { NameFilter = new() { Name = "QbProcessor", MatchCriterion = MatchCriterion.StartsWith } };
                    ItemInventoryRs items = new(QB.ExecuteQbRequest(itemsRq));

                    ItemOtherChargeQueryRq chargeRq = new();
                    ItemOtherChargeRs charges = new(QB.ExecuteQbRequest(chargeRq));

                    addRq.Vendor = new() { ListID = vendor.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.PurchaseOrderLine = new();
                    addRq.PurchaseOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[0].ListID },
                        ManufacturerPartNumber = "12345-01",
                        Rate = 12.34M,
                        Quantity = 5,
                        Desc = $"#1 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.PurchaseOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[1].ListID },
                        ManufacturerPartNumber = "12345-02",
                        Rate = 20M,
                        Quantity = 1,
                        Desc = $"#2 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.PurchaseOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[2].ListID },
                        ManufacturerPartNumber = "12345-03",
                        Rate = 250,
                        Amount = 123.45M,
                        Desc = $"#3 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.PurchaseOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[3].ListID },
                        ManufacturerPartNumber = "12345-04",
                        Rate = 11M,
                        Quantity = 30,
                        Desc = $"#4 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                PurchaseOrderRetDto PurchaseOrder = qryRs.TotalPurchaseOrders == 0 ? addRs.PurchaseOrders[0] : qryRs.PurchaseOrders[0];
                modRq.TxnID = PurchaseOrder.TxnID;
                modRq.EditSequence = PurchaseOrder.EditSequence;
                modRq.TxnDate = PurchaseOrder.TxnDate;
                modRq.Vendor = PurchaseOrder.Vendor;
                modRq.VendorAddress = new()
                {
                    Addr1 = "3648 Kapalua Way",
                    City = "Raleigh",
                    State = "NC",
                    PostalCode = "27610"
                };
                modRq.Memo = $"QbProcessor.{modRq.GetType().Name} on {DateTime.Now}";
                Assert.IsTrue(modRq.IsEntityValid());

                result = QB.ExecuteQbRequest(modRq);
                modRs = new(result);
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
