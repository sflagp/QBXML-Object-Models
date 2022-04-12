using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class ListDeletedQueryTests
    {
        [TestMethod]
        public void TestListDeletedModel()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbListDeletedView qryRs;

                Regex acceptableCodes = new(@"^0$|^1$");
                string delTypes = @"^Account$|^BillingRate$|^Class$|^Currency$|^Customer$|^CustomerMsg$|^CustomerType$|^DateDrivenTerms$|^Employee$|^InventorySite$|^ItemDiscount$|^ItemFixedAsset$|^ItemGroup$|^ItemInventory$|^ItemInventoryAssembly$|^ItemNonInventory$|^ItemOtherCharge$|^ItemPayment$|^ItemSalesTax$|^ItemSalesTaxGroup$|^ItemService$|^ItemSubtotal$|^JobType$|^OtherName$|^PaymentMethod$|^PayrollItemNonWage$|^PayrollItemWage$|^PriceLevel$|^SalesRep$|^SalesTaxCode$|^ShipMethod$|^StandardTerms$|^ToDo$|^UnitOfMeasureSet$|^Vehicle$|^Vendor$|^VendorType$|^WorkersCompCode$";
                string[] ListDelTypes = delTypes.Replace(@"$", "").Replace("^", "").Split("|");
                string result;
                #endregion

                #region Cycle through Transaction Types
                ListDeletedQueryRq qryRq = new();
                qryRq.DeletedDateRangeFilter = new() { FromDeletedDate = DateTime.Today.AddDays(-7), ToDeletedDate = DateTime.Today };
                Assert.IsFalse(qryRq.IsEntityValid());

                foreach(string delType in ListDelTypes)
                {
                    qryRq.ListDelType = delType;
                    Assert.IsTrue(qryRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(qryRq);
                    qryRs = new(result);
                    Assert.IsTrue(acceptableCodes.IsMatch(qryRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));

                    if (qryRs.StatusCode == "0")
                    {
                        Assert.IsTrue(qryRs.TotalListsDeleted > 0);
                    }
                    else
                    {
                        Assert.IsTrue(qryRs.TotalListsDeleted == 0);
                    }
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
