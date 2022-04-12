using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class BillTests
    {
        [TestMethod]
        public void TestBillModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbBillsView qryRs, addRs = new(""), modRs;
                BillAddRq addRq = new();
                BillModRq modRq = new();
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                BillQueryRq qryRq = new();
                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = "StartsWith" };
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalBills == 0)
                {
                    Random rdm = new();

                    ItemOtherChargeQueryRq chargesRq = new();
                    QbItemOtherChargesView charges = new(QB.ExecuteQbRequest(chargesRq));
                    ItemOtherChargeRetDto item = charges.ItemOtherCharges[rdm.Next(0, charges.ItemOtherCharges.Count)];

                    VendorQueryRq vendorRq = new();
                    QbVendorsView vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    addRq.Vendor = new() { ListID = vendor.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.ItemLine = new();
                    addRq.ItemLine.Add(new() { Item = new() { ListID = item.ListID }, Amount = 12.34M });
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                BillRetDto bill = qryRs.TotalBills == 0 ? addRs.Bills[0] : qryRs.Bills[0];
                modRq.TxnID = bill.TxnID;
                modRq.EditSequence = bill.EditSequence;
                modRq.TxnDate = DateTime.Now;
                modRq.Memo = $"QbProcessor.{modRq.GetType().Name} on {DateTime.Now}";
                Assert.IsTrue(modRq.IsEntityValid());

                modRs = new(QB.ExecuteQbRequest(modRq));
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void TestBillToPayModel()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                Random rdm = new();
                VendorQueryRq vendorRq = new();
                QbVendorsView vendors = new(QB.ExecuteQbRequest(vendorRq));
                VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];
                #endregion

                #region Query Test
                BillToPayQueryRq qryRq = new();
                qryRq.PayeeEntity = new() { ListID = vendor.ListID };
                Assert.IsTrue(qryRq.IsEntityValid());

                var result = QB.ExecuteQbRequest(qryRq);
                QbBillToPaysView billsToPay = new(result);
                Assert.IsTrue(billsToPay.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(billsToPay.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void TestBillingRateModel()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }
                #endregion

                #region Query Test
                BillingRateQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                var result = QB.ExecuteQbRequest(qryRq);
                QbBillingRatesView billingRates = new(result);
                Regex statusCodes =  new(@"^0$|^3250$");
                Assert.IsTrue(statusCodes.IsMatch(billingRates.StatusCode));
                Assert.IsTrue(string.IsNullOrEmpty(billingRates.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
