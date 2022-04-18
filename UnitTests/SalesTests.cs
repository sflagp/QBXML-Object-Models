using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class SalesOrderTests
    {
        [TestMethod]
        public void TestSalesOrderModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                SalesOrderRs qryRs, addRs = new(""), modRs;
                SalesOrderAddRq addRq = new();
                SalesOrderModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                SalesOrderQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalSalesOrders == 0)
                {
                    Random rdm = new();

                    CustomerQueryRq customerRq = new();
                    CustomerRs customers = new(QB.ExecuteQbRequest(customerRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    ItemInventoryQueryRq itemsRq = new() { NameFilter = new() { Name = "QbProcessor", MatchCriterion= MatchCriterion.StartsWith } };
                    ItemInventoryRs items = new(QB.ExecuteQbRequest(itemsRq));

                    addRq.Customer = new() { ListID = customer.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.SalesOrderLine = new();
                    addRq.SalesOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[0].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 12.34M,
                        Quantity = 5,
                        Desc = $"#1 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.SalesOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[1].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 20M,
                        Quantity = 1,
                        Desc = $"#2 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.SalesOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[2].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 250,
                        Amount = 123.45M,
                        Desc = $"#3 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.SalesOrderLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[3].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 11M,
                        Quantity = 30,
                        Desc = $"#4 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                SalesOrderRetDto salesOrder = qryRs.TotalSalesOrders == 0 ? addRs.SalesOrders[0] : qryRs.SalesOrders[0];
                modRq.TxnID = salesOrder.TxnID;
                modRq.EditSequence = salesOrder.EditSequence;
                modRq.TxnDate = salesOrder.TxnDate;
                modRq.Customer = salesOrder.Customer;
                modRq.BillAddress = new()
                {
                    Addr1 = "3648 Kapalua Way",
                    City = "Raleigh",
                    State = "NC",
                    PostalCode = "27610"
                };
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
    public class SalesReceiptTests
    {
        [TestMethod]
        public void TestSalesReceiptModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                SalesReceiptRs qryRs, addRs = new(""), modRs;
                SalesReceiptAddRq addRq = new();
                SalesReceiptModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                SalesReceiptQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.RefNumberFilter = new() { RefNumber = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalSalesReceipts == 0)
                {
                    Random rdm = new();

                    CustomerQueryRq customerRq = new();
                    CustomerRs customers = new(QB.ExecuteQbRequest(customerRq));
                    CustomerRetDto customer = customers.Customers[rdm.Next(0, customers.Customers.Count)];

                    ItemInventoryQueryRq itemsRq = new() { NameFilter = new() { Name = "QbProcessor", MatchCriterion = MatchCriterion.StartsWith } };
                    ItemInventoryRs items = new(QB.ExecuteQbRequest(itemsRq));

                    addRq.Customer = new() { ListID = customer.ListID };
                    addRq.TxnDate = DateTime.Now;
                    addRq.RefNumber = addRqName;
                    addRq.SalesReceiptLine = new();
                    addRq.SalesReceiptLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[0].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 12.34M,
                        Quantity = 5,
                        Desc = $"#1 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.SalesReceiptLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[1].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 20M,
                        Quantity = 1,
                        Desc = $"#2 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.SalesReceiptLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[2].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 250,
                        Amount = 123.45M,
                        Desc = $"#3 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    addRq.SalesReceiptLine.Add(new()
                    {
                        Item = new() { ListID = items.ItemInventory[3].ListID },
                        OptionForPriceRuleConflict = OptionForPriceRuleConflict.BasePrice,
                        Rate = 11M,
                        Quantity = 30,
                        Desc = $"#4 QbProcessor.{addRq.GetType().Name} on {DateTime.Now}"
                    });
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    Assert.IsTrue(addRs.StatusCode == "0");
                }
                #endregion

                #region Mod Test
                SalesReceiptRetDto SalesReceipt = qryRs.TotalSalesReceipts == 0 ? addRs.SalesReceipts[0] : qryRs.SalesReceipts[0];
                modRq.TxnID = SalesReceipt.TxnID;
                modRq.EditSequence = SalesReceipt.EditSequence;
                modRq.TxnDate = SalesReceipt.TxnDate;
                modRq.Customer = SalesReceipt.Customer;
                modRq.BillAddress = new()
                {
                    Addr1 = "3648 Kapalua Way",
                    City = "Raleigh",
                    State = "NC",
                    PostalCode = "27610"
                };
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
    public class SalesRepTests
    {
        [TestMethod]
        public void TestSalesRepModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                SalesRepRs qryRs, addRs = new(""), modRs;
                SalesRepAddRq addRq = new();
                SalesRepModRq modRq = new();
                string addRqName = $"QbP";
                #endregion

                #region Query Test
                SalesRepQueryRq qryRq = new();
                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalSalesReps == 0)
                {
                    Random rdm = new();
                    VendorQueryRq vendRq = new();
                    VendorRs vendVw = new(QB.ExecuteQbRequest(vendRq));
                    VendorRetDto vend = vendVw.Vendors[rdm.Next(0, vendVw.Vendors.Count)];

                    addRq.Initial = addRqName;
                    addRq.IsActive = false;
                    addRq.SalesRepEntity = new() { ListID = vend.ListID };
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    Assert.IsTrue(!addRs.SalesReps[0].IsActive);
                }
                #endregion

                #region Mod Test
                SalesRepRetDto acct = qryRs.TotalSalesReps == 0 ? addRs.SalesReps[0] : qryRs.SalesReps[0];
                modRq.ListID = acct.ListID;
                modRq.EditSequence = acct.EditSequence;
                modRq.Initial = acct.Initial;
                modRq.IsActive = true;
                Assert.IsTrue(modRq.IsEntityValid());

                modRs = new(QB.ExecuteQbRequest(modRq));
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                Assert.IsTrue(modRs.SalesReps[0].IsActive);
                #endregion
            }
            Thread.Sleep(2000);
        }
    }

    [TestClass]
    public class SalesTaxCodeTests
    {
        [TestMethod]
        public void TestSalesTaxCodeModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                SalesTaxCodeRs qryRs, addRs = new(""), modRs;
                SalesTaxCodeAddRq addRq = new();
                SalesTaxCodeModRq modRq = new();
                string addRqName = $"QbP";
                #endregion

                #region Query Test
                SalesTaxCodeQueryRq qryRq = new();
                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalSalesTaxCodes == 0)
                {
                    addRq.Name = addRqName;
                    addRq.IsActive = false;
                    addRq.IsTaxable = true;
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    Assert.IsTrue(!addRs.SalesTaxCodes[0].IsActive);
                }
                #endregion

                #region Mod Test
                SalesTaxCodeRetDto acct = qryRs.TotalSalesTaxCodes == 0 ? addRs.SalesTaxCodes[0] : qryRs.SalesTaxCodes[0];
                modRq.ListID = acct.ListID;
                modRq.EditSequence = acct.EditSequence;
                modRq.Name = acct.Name;
                modRq.IsActive = true;
                modRq.Desc = $"{acct.Name} mod {DateTime.Now}.";
                Assert.IsTrue(modRq.IsEntityValid());

                modRs = new(QB.ExecuteQbRequest(modRq));
                Assert.IsTrue(modRs.StatusCode == "0");
                Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                Assert.IsTrue(modRs.SalesTaxCodes[0].IsActive);
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
