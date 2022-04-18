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
    public class CheckTests
    {
        [TestMethod]
        public void TestCheckModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                CheckRs qryRs, addRs = new(""), modRs;
                CheckAddRq addRq = new();
                CheckModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                CheckQueryRq qryRq = new();
                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalChecks == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto account = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.Bank);

                    ItemQueryRq itemsRq = new();
                    result = QB.ExecuteQbRequest(itemsRq);
                    ItemRs items = new(result);
                    ItemOtherChargeRetDto item = items.OtherChargeItems[rdm.Next(0, items.PaymentItems.Count)];

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    addRq.Account = new() { ListID = account.ListID };
                    addRq.PayeeEntity = new() { ListID = vendor.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.ItemLine = new();
                    addRq.ItemLine.Add( new() { Item = new() { ListID = item.ListID }, Amount = 12.34M });
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                CheckRetDto Check = qryRs.TotalChecks == 0 ? addRs.Checks[0] : qryRs.Checks[0];
                modRq.TxnID = Check.TxnID;
                modRq.EditSequence = Check.EditSequence;
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
    }

    [TestClass]
    public class BillPaymentCheckTests
    {
        [TestMethod]
        public void TestBillPaymentCheckModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                BillPaymentCheckRs qryRs, addRs = new(""), modRs;
                BillPaymentCheckAddRq addRq = new();
                BillPaymentCheckModRq modRq = new();
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                BillPaymentCheckQueryRq qryRq = new();
                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalChecks == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto account = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.AccountsPayable);
                    AccountRetDto bank = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.Bank);

                    BillQueryRq billsRq = new() { PaidStatus = PaidStatus.NotPaidOnly };
                    BillRs bills = new(QB.ExecuteQbRequest(billsRq));
                    BillRetDto bill = bills.Bills[rdm.Next(0, bills.Bills.Count)];

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    addRq.PayeeEntity = new() { ListID = vendor.ListID };
                    addRq.APAccount = new() { ListID = account.ListID };
                    addRq.BankAccount = new() { ListID = bank.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.AppliedToTxn = new();
                    addRq.AppliedToTxn.Add(new AppliedToTxnAddDto(){ TxnID = bill.TxnID, PaymentAmount = bill.AmountDue, DiscountAmount = 10.00M });
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Regex responses = new(@"^0$|^3120$|^3250$");
                    Assert.IsTrue(responses.IsMatch(addRs.StatusCode));
                }
                #endregion

                #region Mod Test
                if(qryRs.TotalChecks > 0 || addRs?.StatusCode == "0")
                {
                    BillPaymentCheckRetDto Check = qryRs.TotalChecks == 0 ? addRs.Checks[0] : qryRs.Checks[0];
                    modRq.TxnID = Check.TxnID;
                    modRq.EditSequence = Check.EditSequence;
                    modRq.TxnDate = DateTime.Now;
                    modRq.Memo = $"QbProcessor.{modRq.GetType().Name} on {DateTime.Now}";
                    Assert.IsTrue(modRq.IsEntityValid());

                    modRs = new(QB.ExecuteQbRequest(modRq));
                    Assert.IsTrue(modRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }


    [TestClass]
    public class SalesTaxPaymentCheckTests
    {
        [TestMethod]
        public void TestSalesTaxPaymentCheckModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                SalesTaxPaymentCheckRs qryRs, addRs = new(""), modRs;
                SalesTaxPaymentCheckAddRq addRq = new();
                SalesTaxPaymentCheckModRq modRq = new();
                Regex responses = new(@"^0$|^3250$");
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                SalesTaxPaymentCheckQueryRq qryRq = new();
                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                Assert.IsTrue(responses.IsMatch(qryRs.StatusCode));
                #endregion

                #region Add Test
                if (qryRs.StatusCode == "0" && qryRs.TotalSalesTaxPaymentChecks == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto bank = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.Bank);

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    addRq.PayeeEntity = new() { ListID = vendor.ListID };
                    addRq.BankAccount = new() { ListID = bank.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(responses.IsMatch(addRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                if (qryRs.StatusCode == "0" && (qryRs.TotalSalesTaxPaymentChecks > 0 || addRs?.StatusCode == "0"))
                {
                    SalesTaxPaymentCheckRetDto check = qryRs.TotalSalesTaxPaymentChecks == 0 ? addRs.SalesTaxPaymentChecks[0] : qryRs.SalesTaxPaymentChecks[0];
                    modRq.TxnID = check.TxnID;
                    modRq.EditSequence = check.EditSequence;
                    modRq.TxnDate = DateTime.Now;
                    modRq.Memo = $"QbProcessor.{modRq.GetType().Name} on {DateTime.Now}";
                    Assert.IsTrue(modRq.IsEntityValid());

                    modRs = new(QB.ExecuteQbRequest(modRq));
                    Assert.IsTrue(modRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
