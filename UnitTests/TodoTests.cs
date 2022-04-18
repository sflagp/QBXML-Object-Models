using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class TodoTests
    {
        [TestMethod]
        public void TestTodoModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                ToDoRs qryRs, addRs = new(""), modRs;
                ToDoAddRq addRq = new();
                ToDoModRq modRq = new();
                Random rdm = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                ToDoQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.DoneStatus = DoneStatus.NotDoneOnly;
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                if (qryRs.StatusCode == "3231") Assert.Inconclusive(qryRs.StatusMessage);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalToDos == 0)
                {
                    CustomerQueryRq custRq = new();
                    CustomerRs customers = new(QB.ExecuteQbRequest(custRq));
                    CustomerRetDto cust = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    addRq.Notes = $"{addRqName}.{addRq.GetType().Name}";
                    addRq.Customer = new() { ListID = cust.ListID };
                    addRq.Notes = $"Requested by {addRqName} on {DateTime.Now}";
                    addRq.IsActive = true;
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                ToDoRetDto Todo = qryRs.TotalToDos == 0 ? addRs.ToDos[0] : qryRs.ToDos[0];

                EmployeeQueryRq empRq = new();
                EmployeeRs employees = new(QB.ExecuteQbRequest(empRq));
                EmployeeRetDto emp = employees.Employees[rdm.Next(0, employees.Employees.Count)];

                modRq.ListID = Todo.ListID;
                modRq.EditSequence = Todo.EditSequence;
                modRq.Employee = new() { ListID = emp.ListID };
                modRq.Notes = $"Completed by {addRqName}.{modRq.GetType().Name} on {DateTime.Now}";
                modRq.IsDone = true;
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
