using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestAccountModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbAccountsView qryRs, addRs = new(""), modRs;
                AccountAddRq addRq = new();
                AccountModRq modRq = new();
                string addRqName = $"QbProcessor {addRq.GetType().Name}";
                string result;
                #endregion

                #region Query Test
                AccountQueryRq qryRq = new();
                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = "StartsWith" };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalAccounts == 0)
                {
                    addRq.Name = addRqName;
                    addRq.IsActive = true;
                    addRq.AccountType = "OtherExpense";
                    addRq.Desc = addRq.GetType().Name;
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                AccountRetDto acct = qryRs.TotalAccounts == 0 ? addRs.Accounts[0] : qryRs.Accounts[0];
                modRq.ListID = acct.ListID;
                modRq.EditSequence = acct.EditSequence;
                modRq.Name = acct.Name;
                modRq.Desc = modRq.GetType().Name;
                Assert.IsTrue(modRq.IsEntityValid());

                result = QB.ExecuteQbRequest(modRq);
                modRs = new(result);
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(modRs.Accounts[0].Desc == modRq.GetType().Name);

                modRq.ListID = modRs.Accounts[0].ListID;
                modRq.EditSequence = modRs.Accounts[0].EditSequence;
                modRq.Desc = $"Modified by {modRq.GetType().Name} on {DateTime.Now}";
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
