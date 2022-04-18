using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbHelpers;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class TimeTrackingTests
    {
        [TestMethod]
        public void TestTimeTrackingModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                TimeTrackingRs qryRs, addRs = new(""), modRs;
                TimeTrackingAddRq addRq = new();
                TimeTrackingModRq modRq = new();
                EmployeeRetDto emp;
                string addRqName = $"QbProcessor";
                Random rdm = new();
                #endregion

                #region Query Test
                EmployeeQueryRq empRq = new();
                EmployeeRs emps = new(QB.ExecuteQbRequest(empRq));
                emp = emps.Employees[rdm.Next(0, emps.Employees.Count)];

                TimeTrackingQueryRq qryRq = new();
                qryRq.TimeTrackingEntityFilter = new() { ListID = emp.ListID };
                qryRq.TxnDateRangeFilter = new() { FromTxnDate = DateTime.Today.AddDays(-2), ToTxnDate = DateTime.Today };
                Assert.IsTrue(qryRq.IsEntityValid());

                string strRs = QB.ExecuteQbRequest(qryRq);
                qryRs = new(strRs);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalTimeTracking == 0)
                {
                    CustomerQueryRq customerRq = new();
                    CustomerRs customers = new(QB.ExecuteQbRequest(customerRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    ItemNonInventoryQueryRq itemRq = new();
                    ItemNonInventoryRs items = new(QB.ExecuteQbRequest(itemRq));
                    ItemNonInventoryRetDto item = items.ItemsNonInventory[rdm.Next(0, items.ItemsNonInventory.Count)];

                    addRq.TxnDate = DateTime.Now;
                    addRq.Entity = new() { ListID = emp.ListID };
                    addRq.Customer = new() { ListID = customer.ListID };
                    addRq.ItemService = new() { ListID = item.ListID };
                    addRq.Duration = 1.11M;
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    Assert.IsTrue(addRs.TotalTimeTracking > 0);
                }
                #endregion

                #region Mod Test
                TimeTrackingRetDto timeTracking = qryRs.TotalTimeTracking > 0 ? qryRs.TimeTracking[0] : addRs.TimeTracking[0];
                VendorQueryRq vendRq = new();
                VendorRs vendors = new(QB.ExecuteQbRequest(vendRq));
                VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.TotalVendors)];

                modRq.TxnID = timeTracking.TxnID;
                modRq.EditSequence = timeTracking.EditSequence;
                modRq.Entity = new() { ListID = vendor.ListID };
                modRq.Duration = timeTracking.Duration.FromQbTime() + 0.05M;
                modRq.TxnDate = DateTime.Now;
                modRq.Notes = $"{addRqName} modified on {DateTime.Now} by {modRq.GetType().Name}";
                modRq.BillableStatus = BillStatus.Billable;
                Assert.IsTrue(modRq.IsEntityValid());

                modRs = new(QB.ExecuteQbRequest(modRq));
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
