using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class CreditCardChargeTests
    {
        [TestMethod]
        public void TestCreditCardChargeModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                CreditCardChargeRs qryRs, addRs = new(""), modRs;
                CreditCardChargeAddRq addRq = new();
                CreditCardChargeModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                CreditCardChargeQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalCreditCardCharges == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    accountsRq.AccountType = AccountType.CreditCard;
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto account = accounts.Accounts[rdm.Next(0, accounts.Accounts.Count)];

                    ItemQueryRq itemsRq = new();
                    ItemRs items = new(QB.ExecuteQbRequest(itemsRq));
                    ItemOtherChargeRetDto item = items.OtherChargeItems[rdm.Next(0, items.PaymentItems.Count)];

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    CustomerQueryRq customerRq = new();
                    CustomerRs customers = new(QB.ExecuteQbRequest(customerRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    addRq.Account = new() { ListID = account.ListID };
                    addRq.PayeeEntity = new() { ListID = vendor.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.ItemLine = new();
                    addRq.ItemLine.Add(new()
                    {
                        Item = new() { ListID = item.ListID },
                        Amount = 123.45M
                    });
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                CreditCardChargeRetDto creditCardCharge = qryRs.TotalCreditCardCharges == 0 ? addRs.CreditCardCharges[0] : qryRs.CreditCardCharges[0];
                modRq.TxnID = creditCardCharge.TxnID;
                modRq.EditSequence = creditCardCharge.EditSequence;
                modRq.Account = creditCardCharge.Account;
                modRq.TxnDate = creditCardCharge.TxnDate;
                modRq.Memo = $"QbProcessor.{addRq.GetType().Name} on {DateTime.Now}";
                Assert.IsTrue(modRq.IsEntityValid());

                modRq.TxnDate = default;
                result = QB.ExecuteQbRequest(modRq);
                modRs = new(result);
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }
    }

    [TestClass]
    public class CreditCardCreditTests
    {
        [TestMethod]
        public void TestCreditCardCreditModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                CreditCardCreditRs qryRs, addRs = new(""), modRs;
                CreditCardCreditAddRq addRq = new();
                CreditCardCreditModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                CreditCardCreditQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalCreditCardCredits == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    accountsRq.AccountType = AccountType.CreditCard;
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto account = accounts.Accounts[rdm.Next(0, accounts.Accounts.Count)];

                    ItemQueryRq itemsRq = new();
                    ItemRs items = new(QB.ExecuteQbRequest(itemsRq));
                    ItemOtherChargeRetDto item = items.OtherChargeItems[rdm.Next(0, items.PaymentItems.Count)];

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    CustomerQueryRq customerRq = new();
                    CustomerRs customers = new(QB.ExecuteQbRequest(customerRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    addRq.Account = new() { ListID = account.ListID };
                    addRq.PayeeEntity = new() { ListID = vendor.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.ItemLine = new();
                    addRq.ItemLine.Add(new()
                    {
                        Item = new() { ListID = item.ListID },
                        Amount = 123.45M
                    });
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                CreditCardCreditRetDto creditCardCredit = qryRs.TotalCreditCardCredits == 0 ? addRs.CreditCardCredits[0] : qryRs.CreditCardCredits[0];
                modRq.TxnID = creditCardCredit.TxnID;
                modRq.EditSequence = creditCardCredit.EditSequence;
                modRq.Account = creditCardCredit.Account;
                modRq.TxnDate = creditCardCredit.TxnDate;
                modRq.PayeeEntity = creditCardCredit.PayeeEntity;
                modRq.Memo = $"QbProcessor.{modRq.GetType().Name} on {DateTime.Now}";
                Assert.IsTrue(modRq.IsEntityValid());

                modRq.TxnDate = default;
                result = QB.ExecuteQbRequest(modRq);
                modRs = new(result);
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                #endregion
            }
            Thread.Sleep(2000);
        }
    }

    [TestClass]
    public class BillPaymentCreditCardTests
    {
        [TestMethod]
        public void TestBillPaymentCreditCardModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                BillPaymentCreditCardRs qryRs, addRs;
                BillPaymentCreditCardAddRq addRq = new();
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                BillPaymentCreditCardQueryRq qryRq = new();
                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalBillPaymentCreditCards == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto account = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.AccountsPayable);
                    AccountRetDto card = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.CreditCard);

                    BillQueryRq billsRq = new();
                    BillRs bills = new(QB.ExecuteQbRequest(billsRq));
                    BillRetDto bill = bills.Bills[rdm.Next(0, bills.Bills.Count)];

                    VendorQueryRq vendorRq = new();
                    VendorRs vendors = new(QB.ExecuteQbRequest(vendorRq));
                    VendorRetDto vendor = vendors.Vendors[rdm.Next(0, vendors.Vendors.Count)];

                    addRq.PayeeEntity = new() { ListID = vendor.ListID };
                    addRq.APAccount = new() { ListID = account.ListID };
                    addRq.CreditCardAccount = new() { ListID = card.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.AppliedToTxnAdd = new();
                    addRq.AppliedToTxnAdd.Add(new AppliedToTxnAddDto() { TxnID = bill.TxnID, PaymentAmount = 1M });
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Regex responses = new(@"^0$|^3120$|^3250$");
                    Assert.IsTrue(responses.IsMatch(addRs.StatusCode));
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }

    [TestClass]
    public class ARRefundCreditCardTests
    {
        [TestMethod]
        public void TestARRefundCreditCardModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                ARRefundCreditCardRs qryRs, addRs;
                ARRefundCreditCardAddRq addRq = new();
                string addRqName = $"QbProcessor";
                #endregion

                #region Query Test
                ARRefundCreditCardQueryRq qryRq = new();
                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalARRefundCreditCards == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    string acctQryRs = QB.ExecuteQbRequest(accountsRq);
                    AccountRs accounts = new(acctQryRs);
                    AccountRetDto account = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.AccountsReceivable);
                    AccountRetDto bank = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.Bank);

                    InvoiceQueryRq invoicesRq = new() { MaxReturned = 100, IncludeLinkedTxns = true };
                    InvoiceRs invoicesRs = new(QB.ExecuteQbRequest(invoicesRq));
                    List<InvoiceRetDto> invoices = invoicesRs.Invoices.Where(i => i.LinkedTxn.Count > 0).ToList();
                    InvoiceRetDto invoice = invoices[rdm.Next(0, invoices.Count)];

                    ReceivePaymentQueryRq pmtRq = new() { MaxReturned = 50 };
                    ReceivePaymentRs payments = new(QB.ExecuteQbRequest(pmtRq));
                    ReceivePaymentRetDto pmt = payments.ReceivePayments?[rdm.Next(0, payments.ReceivePayments.Count)];

                    CustomerQueryRq customerRq = new();
                    customerRq.ListID = new() { pmt.Customer.ListID };
                    string custRs = QB.ExecuteQbRequest(customerRq);
                    CustomerRs customers = new(custRs);
                    if (customers.Customers.Count == 0) Assert.Inconclusive("Customer not found.");
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    addRq.Customer = new() { ListID = customer.ListID };
                    addRq.ARAccount = new() { ListID = account.ListID };
                    addRq.RefundFromAccount = new() { ListID = bank?.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.RefundAppliedToTxnAdd = new();
                    addRq.RefundAppliedToTxnAdd.Add(new() { TxnID = invoice.LinkedTxn[0].TxnID, RefundAmount = invoice.AppliedAmount });
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    if (addRs.StatusCode == "3120") Assert.Inconclusive(addRs.StatusMessage);
                    Regex responses = new(@"^0$|^3120$|^3250$");
                    Assert.IsTrue(responses.IsMatch(addRs.StatusCode));
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
