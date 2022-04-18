using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class InvoiceTests
    {
        [TestMethod]
        public void TestInvoiceModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                InvoiceRs qryRs, addRs = new(""), modRs;
                InvoiceAddRq addRq = new();
                InvoiceModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                InvoiceQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalInvoices == 0)
                {
                    Random rdm = new();

                    AccountQueryRq accountsRq = new();
                    accountsRq.AccountType = AccountType.AccountsReceivable;
                    AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                    AccountRetDto account = accounts.Accounts[rdm.Next(0, accounts.Accounts.Count)];

                    CustomerQueryRq customerRq = new();
                    CustomerRs customers = new(QB.ExecuteQbRequest(customerRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    ItemNonInventoryQueryRq itemsRq = new();
                    ItemNonInventoryRs items = new(QB.ExecuteQbRequest(itemsRq));

                    ItemOtherChargeQueryRq chargeRq = new();
                    ItemOtherChargeRs charges = new(QB.ExecuteQbRequest(chargeRq));

                    addRq.Customer = new() { ListID = customer.ListID };
                    addRq.ARAccount = new() { ListID = account.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.InvoiceLine = new();
                    addRq.InvoiceLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemsNonInventory[rdm.Next(0, items.ItemsNonInventory.Count)].ListID },
                        Rate = 12.34M,
                        Quantity = 5,
                        Desc = $"#1 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.InvoiceLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemsNonInventory[rdm.Next(0, items.ItemsNonInventory.Count)].ListID },
                        Rate = 20M,
                        Quantity = 1,
                        Desc = $"#2 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.InvoiceLine.Add(new()
                    {
                        Item = new() { ListID = charges.ItemOtherCharges[rdm.Next(0, charges.ItemOtherCharges.Count)].ListID },
                        Rate = 250,
                        Amount = 123.45M,
                        Desc = $"#4 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.InvoiceLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemsNonInventory[rdm.Next(0, items.ItemsNonInventory.Count)].ListID },
                        Rate = 11M,
                        Quantity = 30,
                        Desc = $"#3 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                InvoiceRetDto Invoice = qryRs.TotalInvoices == 0 ? addRs.Invoices[0] : qryRs.Invoices[0];
                modRq.TxnID = Invoice.TxnID;
                modRq.EditSequence = Invoice.EditSequence;
                modRq.TxnDate = Invoice.TxnDate;
                modRq.Customer = Invoice.Customer;
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
}
