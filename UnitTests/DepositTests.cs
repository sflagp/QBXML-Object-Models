using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class DepositTests
    {
        [TestMethod]
        public void TestDepositModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbDepositsView qryRs, addRs = new(""), modRs;
                DepositAddRq addRq = new();
                DepositModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                DepositQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.ModifiedDateRangeFilter = new() { FromModifiedDate = DateTime.Today.AddDays(-90), ToModifiedDate = DateTime.Today };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalDeposits == 0)
                {
                    Random rdm = new();

                    AccountQueryRq bankRq = new() { AccountType = "Bank" };
                    QbAccountsView banks = new(QB.ExecuteQbRequest(bankRq));
                    AccountRetDto bank = banks.Accounts[rdm.Next(0, banks.Accounts.Count)];

                    AccountQueryRq acctRq = new() { AccountType = "AccountsReceivable" };
                    QbAccountsView accts = new(QB.ExecuteQbRequest(acctRq));
                    AccountRetDto acct = accts.Accounts[rdm.Next(0, accts.Accounts.Count)];

                    CustomerQueryRq custRq = new();
                    QbCustomersView customers = new(QB.ExecuteQbRequest(custRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    addRq.DepositToAccount = new() { ListID = bank.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.DepositLine = new();
                    addRq.DepositLine.Add(new());
                    addRq.DepositLine[0].Entity = new() { ListID = customer.ListID };
                    addRq.DepositLine[0].Account = new() { ListID = acct.ListID };
                    addRq.DepositLine[0].Memo = $"{addRqName}.{addRq.GetType().Name} on {DateTime.Now}";
                    addRq.DepositLine[0].Amount = 123.45M;
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                DepositRetDto Deposit = qryRs.TotalDeposits == 0 ? addRs.Deposits[0] : qryRs.Deposits[0];
                modRq.TxnID = Deposit.TxnID;
                modRq.EditSequence = Deposit.EditSequence;
                modRq.TxnDate = DateTime.Now;
                modRq.Memo = $"{addRqName}.{modRq.GetType().Name} on {DateTime.Now}";
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
