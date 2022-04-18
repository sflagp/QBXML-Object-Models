using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class ZCustomerCleanupTests
    {
        [TestMethod]
        //[Ignore("Do not cleanup transactions")]
        public void TestCleanupModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                TransactionQueryRq qryRq;
                TransactionRs qryRs;
                Regex validCodes = new(@"^0$|^1$");
                Regex validDelCodes = new(@"^0$|^3160$");
                #endregion

                #region Cleanup transactions
                qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.TxnModifiedDateRangeFilter = new()
                {
                    FromModifiedDate = DateTime.Today.AddDays(-7),
                    ToModifiedDate = DateTime.Today
                };
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(validCodes.IsMatch(qryRs.StatusCode));

                if (qryRs.TotalTransactions > 0)
                {
                    Assert.AreNotEqual(0, qryRs.TotalTransactions);
                    foreach (TransactionRetDto t in qryRs.Transactions)
                    {
                        TxnDelRq delTxnRq = new() { TxnDelType = t.TxnType, TxnID = t.TxnID };
                        Assert.IsTrue(delTxnRq.IsEntityValid());

                        string result = QB.ExecuteQbRequest(delTxnRq);
                        TxnDelRs delTxnRs = new(result);
                        Assert.IsTrue(validDelCodes.IsMatch(delTxnRs.StatusCode));
                        Assert.IsTrue(string.IsNullOrEmpty(delTxnRs.ParseError));
                    }
                }
                #endregion

                #region Cleanup lists
                ListDelRq delListRq;

                #region Account List
                AccountQueryRq acctRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                AccountRs acctVw = new(QB.ExecuteQbRequest(acctRq));
                if (acctVw.TotalAccounts > 0)
                {
                    AccountRetDto acct = acctVw.Accounts.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.Account, ListID = acct.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region BillingRate List
                BillingRateQueryRq billingRateRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                BillingRateRs billingRateVw = new(QB.ExecuteQbRequest(billingRateRq));
                if (billingRateVw.TotalBillingRates > 0)
                {
                    BillingRateRetDto billingRate = billingRateVw.BillingRates.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.BillingRate, ListID = billingRate.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region Class lists
                ClassQueryRq clsRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                ClassRs clsVw = new(QB.ExecuteQbRequest(clsRq));
                if (clsVw.TotalClasses > 0)
                {
                    ClassRetDto cls = clsVw.Classes.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.Class, ListID = cls.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region Currency List
                CurrencyQueryRq currencyRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                CurrencyRs currencyVw = new(QB.ExecuteQbRequest(currencyRq));
                if (currencyVw.TotalCurrencys > 0)
                {
                    CurrencyRetDto currency = currencyVw.Currencys.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.Currency, ListID = currency.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region Customer List
                CustomerQueryRq customerRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                CustomerRs customerVw = new(QB.ExecuteQbRequest(customerRq));
                if (customerVw.TotalCustomers > 0)
                {
                    CustomerRetDto customer = customerVw.Customers.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.Customer, ListID = customer.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region CustomerMsg List
                CustomerMsgQueryRq customerMsgRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                CustomerMsgRs customerMsgVw = new(QB.ExecuteQbRequest(customerMsgRq));
                if (customerMsgVw.TotalCustomerMsgs > 0)
                {
                    CustomerMsgRetDto customerMsg = customerMsgVw.CustomerMsgs.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.CustomerMsg, ListID = customerMsg.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region CustomerType List
                CustomerTypeQueryRq customerTypeRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                CustomerTypeRs customerTypeVw = new(QB.ExecuteQbRequest(customerTypeRq));
                if (customerTypeVw.TotalCustomerTypes > 0)
                {
                    CustomerTypeRetDto customerType = customerTypeVw.CustomerTypes.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.CustomerType, ListID = customerType.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region Employee List
                EmployeeQueryRq EmployeeRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                EmployeeRs EmployeeVw = new(QB.ExecuteQbRequest(EmployeeRq));
                if (EmployeeVw.TotalEmployees > 0)
                {
                    EmployeeRetDto employee = EmployeeVw.Employees.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.Employee, ListID = employee.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region ItemInventory List
                ItemInventoryQueryRq itemRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                ItemInventoryRs itemVw = new(QB.ExecuteQbRequest(itemRq));
                if (itemVw.TotalItemsInventory > 0)
                {
                    foreach(var item in itemVw.ItemInventory)
                    {
                        delListRq = new() { ListDelType = ListDelType.ItemInventory, ListID = item.ListID };
                        Assert.IsTrue(delListRq.IsEntityValid());

                        string result = QB.ExecuteQbRequest(delListRq);
                        ListDelRs delListRs = new(result);
                        Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                        Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                    }
                }
                #endregion
                #region JobType List
                JobTypeQueryRq jobTypeRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                JobTypeRs jobTypeVw = new(QB.ExecuteQbRequest(jobTypeRq));
                if (jobTypeVw.TotalJobTypes > 0)
                {
                    JobTypeRetDto jobType = jobTypeVw.JobTypes.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.JobType, ListID = jobType.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region OtherName List
                OtherNameQueryRq otherNameRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                OtherNameRs otherNameVw = new(QB.ExecuteQbRequest(otherNameRq));
                if (otherNameVw.TotalOtherNames > 0)
                {
                    OtherNameRetDto otherName = otherNameVw.OtherNames.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.OtherName, ListID = otherName.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region PaymentMethod List
                PaymentMethodQueryRq paymentMethodRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                PaymentMethodRs paymentMethodVw = new(QB.ExecuteQbRequest(paymentMethodRq));
                if (paymentMethodVw.TotalPaymentMethods > 0)
                {
                    PaymentMethodRetDto paymentMethod = paymentMethodVw.PaymentMethods.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.PaymentMethod, ListID = paymentMethod.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region PriceLevel List
                PriceLevelQueryRq priceLevelRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                PriceLevelRs priceLevelVw = new(QB.ExecuteQbRequest(priceLevelRq));
                if (priceLevelVw.TotalPriceLevels > 0)
                {
                    PriceLevelRetDto priceLevel = priceLevelVw.PriceLevels.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.PriceLevel, ListID = priceLevel.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region SalesRep List
                SalesRepQueryRq salesRepRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                SalesRepRs salesRepVw = new(QB.ExecuteQbRequest(salesRepRq));
                if (salesRepVw.TotalSalesReps > 0)
                {
                    SalesRepRetDto salesRep = salesRepVw.SalesReps.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.SalesRep, ListID = salesRep.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region SalesTaxCode List
                SalesTaxCodeQueryRq SalesTaxCodeRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                SalesTaxCodeRs SalesTaxCodeVw = new(QB.ExecuteQbRequest(SalesTaxCodeRq));
                if (SalesTaxCodeVw.TotalSalesTaxCodes > 0)
                {
                    SalesTaxCodeRetDto SalesTaxCode = SalesTaxCodeVw.SalesTaxCodes.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.SalesTaxCode, ListID = SalesTaxCode.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region ShipMethod List
                ShipMethodQueryRq shipMethodRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                ShipMethodRs shipMethodVw = new(QB.ExecuteQbRequest(shipMethodRq));
                if (shipMethodVw.TotalShipMethods > 0)
                {
                    ShipMethodRetDto shipMethod = shipMethodVw.ShipMethods.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.ShipMethod, ListID = shipMethod.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region ToDo List
                ToDoQueryRq toDoRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                ToDoRs toDoVw = new(QB.ExecuteQbRequest(toDoRq));
                if (toDoVw.TotalToDos > 0)
                {
                    ToDoRetDto toDo = toDoVw.ToDos.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.ToDo, ListID = toDo.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region Vehicle List
                VehicleQueryRq vehicleRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                VehicleRs vehicleVw = new(QB.ExecuteQbRequest(vehicleRq));
                if (vehicleVw.TotalVehicles > 0)
                {
                    VehicleRetDto vehicle = vehicleVw.Vehicles.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.Vehicle, ListID = vehicle.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    VehicleRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region Vendor List
                VendorQueryRq vendorRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                VendorRs vendorVw = new(QB.ExecuteQbRequest(vendorRq));
                if (vendorVw.TotalVendors > 0)
                {
                    VendorRetDto vendor = vendorVw.Vendors.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.Vendor, ListID = vendor.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region VendorType List
                VendorTypeQueryRq vendorTypeRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                VendorTypeRs vendorTypeVw = new(QB.ExecuteQbRequest(vendorTypeRq));
                if (vendorTypeVw.TotalVendorTypes > 0)
                {
                    VendorTypeRetDto vendorType = vendorTypeVw.VendorTypes.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.VendorType, ListID = vendorType.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #region WorkersCompCode List
                WorkersCompCodeQueryRq workersCompCodeRq = new() { NameFilter = new() { Name = "Qb", MatchCriterion = MatchCriterion.StartsWith } };
                WorkersCompCodeRs workersCompCodeVw = new(QB.ExecuteQbRequest(workersCompCodeRq));
                if (workersCompCodeVw.TotalWorkersCompCodes > 0)
                {
                    WorkersCompCodeRetDto workersCompCode = workersCompCodeVw.WorkersCompCodes.FirstOrDefault();
                    delListRq = new() { ListDelType = ListDelType.WorkersCompCode, ListID = workersCompCode.ListID };
                    Assert.IsTrue(delListRq.IsEntityValid());

                    string result = QB.ExecuteQbRequest(delListRq);
                    ListDelRs delListRs = new(result);
                    Assert.IsTrue(validDelCodes.IsMatch(delListRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(delListRs.ParseError));
                }
                #endregion
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
