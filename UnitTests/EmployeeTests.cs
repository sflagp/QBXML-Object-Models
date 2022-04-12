using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Linq;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void TestEmployeeModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbEmployeesView qryRs, addRs = new(""), modRs;
                EmployeeAddRq addRq = new();
                EmployeeModRq modRq = new();
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                EmployeeQueryRq qryRq = new();
                qryRq.NameRangeFilter = new() { FromName = "QbP", ToName = "QbR" };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalEmployees == 0)
                {
                    addRq.FirstName = addRqName;
                    addRq.LastName = addRq.GetType().Name;
                    addRq.IsActive = true;
                    addRq.EmployeeAddress = new()
                    {
                        Addr1 = "3648 Kapalua Way",
                        City = "Raleigh",
                        State = "NC",
                        PostalCode = "27610"
                    };
                    addRq.Phone = "305-775-4754";
                    addRq.Notes = addRq.GetType().Name;
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                EmployeeRetDto acct = qryRs.TotalEmployees == 0 ? addRs.Employees[0] : qryRs.Employees[0];
                modRq.ListID = acct.ListID;
                modRq.EditSequence = acct.EditSequence;
                modRq.Notes = $"{modRq.GetType().Name} on {DateTime.Now}";
                modRq.IsActive = true;
                modRq.Description = $"{addRqName}.{modRq.GetType().Name}";
                Assert.IsTrue(modRq.IsEntityValid());

                modRs = new(QB.ExecuteQbRequest(modRq));
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));

                EmployeeRetDto employee = modRs.Employees.FirstOrDefault();
                Assert.AreEqual($"{addRqName}.{modRq.GetType().Name}", employee.Description);
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
