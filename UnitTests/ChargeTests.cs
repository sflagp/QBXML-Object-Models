using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class ChargeTests
    {
        [TestMethod]
        public void TestChargeModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbChargesView qryRs, addRs = new(""), modRs;
                ChargeAddRq addRq = new();
                ChargeModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                ChargeQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = "StartsWith" };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalCharges == 0)
                {
                    Random rdm = new();

                    ItemNonInventoryQueryRq itemRq = new();
                    QbItemNonInventoryView items = new(QB.ExecuteQbRequest(itemRq));
                    ItemNonInventoryRetDto item = items.ItemsNonInventory[rdm.Next(0, items.ItemsNonInventory.Count)];

                    CustomerQueryRq customerRq = new();
                    QbCustomersView customers = new(QB.ExecuteQbRequest(customerRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    addRq.Customer = new() { ListID = customer.ListID };
                    addRq.Item = new() { ListID = item.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.Amount = 123.45M;
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                ChargeRetDto charge = qryRs.TotalCharges == 0 ? addRs.Charges[0] : qryRs.Charges[0];
                modRq.TxnID = charge.TxnID;
                modRq.EditSequence = charge.EditSequence;
                modRq.TxnDate = charge.TxnDate;
                modRq.Customer = charge.Customer;
                modRq.Quantity = "1";
                modRq.Rate = 123.45M;
                modRq.Amount = 123.45M;
                Assert.IsTrue(modRq.IsEntityValid());

                modRq.TxnDate = default;
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
