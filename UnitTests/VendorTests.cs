using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class VendorTests
    {
        [TestMethod]
        public void TestVendorModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                VendorRs qryRs, addRs = new(""), modRs;
                VendorAddRq addRq = new();
                VendorModRq modRq = new();
                string addRqName = $"QbProcessor.Vendor";
                #endregion

                #region Query Test
                VendorQueryRq qryRq = new();
                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalVendors == 0)
                {
                    addRq.Name = addRqName;
                    addRq.IsActive = true;
                    addRq.VendorAddress = new()
                    {
                        Addr1 = "3648 Kapalua Way",
                        City = "Raleigh",
                        State = "NC",
                        PostalCode = "27610"
                    };
                    addRq.Phone = "305-775-4754";
                    addRq.Notes = addRq.GetType().Name;
                    addRq.OpenBalance = 123.45M;
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));

                }
                #endregion

                #region Mod Test
                VendorRetDto acct = qryRs.TotalVendors == 0 ? addRs.Vendors[0] : qryRs.Vendors[0];
                modRq.ListID = acct.ListID;
                modRq.EditSequence = acct.EditSequence;
                modRq.FirstName = "Greg";
                modRq.LastName = "Prieto";
                modRq.Notes = $"{modRq.GetType().Name} on {DateTime.Now}";
                modRq.IsActive = true;
                Assert.IsTrue(modRq.IsEntityValid());

                modRs = new(QB.ExecuteQbRequest(modRq));
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void TestVendorCreditModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                VendorCreditRs qryRs, addRs = new(""), modRs;
                VendorCreditAddRq addRq = new();
                VendorCreditModRq modRq = new();
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                VendorCreditQueryRq qryRq = new();
                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                qryRq.IncludeLineItems = true;
                qryRq.IncludeLinkedTxns = true;
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalVendorCredits == 0)
                {
                    Random rdm = new();

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    ItemInventoryQueryRq itemsRq = new() { NameFilter = new() { Name = "QbProcessor", MatchCriterion = MatchCriterion.StartsWith } };
                    ItemInventoryRs items = new(QB.ExecuteQbRequest(itemsRq));
                    ItemInventoryRetDto item = items.ItemInventory[rdm.Next(0, items.ItemInventory.Count)];

                    addRq.RefNumber = addRqName;
                    addRq.Vendor = new() { ListID = vendor.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.Memo = $"{addRqName}.{addRq.GetType().Name}";
                    addRq.ItemLine = new();
                    addRq.ItemLine.Add(new()
                    {
                        Item = new() { ListID = item.ListID },
                        Desc = item.PurchaseDesc,
                        Quantity = 5,
                        Cost = item.PurchaseCost,
                        Amount = 5 * item.PurchaseCost
                    });
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));

                }
                #endregion

                #region Mod Test
                VendorCreditRetDto acct = qryRs.TotalVendorCredits == 0 ? addRs.VendorCredits[0] : qryRs.VendorCredits[0];
                modRq.TxnID = acct.TxnID;
                modRq.EditSequence = acct.EditSequence;
                modRq.Memo = $"{modRq.GetType().Name} on {DateTime.Now}";
                Assert.IsTrue(modRq.IsEntityValid());

                modRs = new(QB.ExecuteQbRequest(modRq));
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void TestVendorTypeModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                VendorTypeRs qryRs, addRs;
                VendorTypeAddRq addRq = new();
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                VendorTypeQueryRq qryRq = new();
                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalVendorTypes == 0)
                {
                    addRq.Name = addRqName;
                    addRq.IsActive = true;
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion
            }
            Thread.Sleep(2000);
        }

    }
}
