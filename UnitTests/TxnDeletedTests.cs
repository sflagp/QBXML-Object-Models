using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbHelpers;
using QbModels;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class TxnDeletedQueryTests
    {
        [TestMethod]
        public void TestTxnDeletedModel()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbTxnDeletedView qryRs;

                Regex acceptableCodes = new(@"^0$|^1$");
                string delTypes = @"^ARRefundCreditCard$|^Bill$|^BillPaymentCheck$|^BillPaymentCreditCard$|^BuildAssembly$|^Charge$|^Check$|^CreditCardCharge$|^CreditCardCredit$|^CreditMemo$|^Deposit$|^Estimate$|^InventoryAdjustment$|^Invoice$|^ItemReceipt$|^JournalEntry$|^PayrollLiabilityAdjustment$|^PayrollPriorPayment$|^PayrollYearToDateAdjustment$|^PurchaseOrder$|^ReceivePayment$|^SalesOrder$|^SalesReceipt$|^SalesTaxPaymentCheck$|^TimeTracking$|^TransferInventory$|^VehicleMileage$|^VendorCredit$";
                string[] txnDelTypes = delTypes.Replace(@"$", "").Replace("^", "").Split("|");
                string result;
                #endregion

                #region Cycle through Transaction Types
                TxnDeletedQueryRq qryRq = new();
                qryRq.DeletedDateRangeFilter = new() { FromDeletedDate = DateTime.Today.AddDays(-7), ToDeletedDate = DateTime.Today };
                Assert.IsFalse(qryRq.IsEntityValid());

                foreach(string delType in txnDelTypes)
                {
                    qryRq.TxnDelType = delType;
                    Assert.IsTrue(qryRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(qryRq);
                    qryRs = new(result);
                    Assert.IsTrue(acceptableCodes.IsMatch(qryRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));

                    if (qryRs.StatusCode == "0")
                    {
                        Assert.IsTrue(qryRs.TotalTxnsDeleted > 0);
                    }
                    else
                    {
                        Assert.IsTrue(qryRs.TotalTxnsDeleted == 0);
                    }
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
