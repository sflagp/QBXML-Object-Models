using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Linq;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class ItemInventoryTests
    {
        [TestMethod]
        public void TestItemInventoryModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                ItemInventoryRs qryRs, addRs = new(""), modRs;
                ItemInventoryAddRq addRq = new();
                ItemInventoryModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                ItemInventoryQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                if (qryRs.StatusCode == "3231") Assert.Inconclusive(qryRs.StatusMessage);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                Random rdm = new();

                AccountQueryRq accountsRq = new() { AccountType = AccountType.Income };
                AccountRs accounts = new(QB.ExecuteQbRequest(accountsRq));
                AccountRetDto account = accounts.Accounts[rdm.Next(0, accounts.Accounts.Count)];

                AccountQueryRq assetsRq = new() { AccountType = AccountType.OtherAsset };
                AccountRs assets = new(QB.ExecuteQbRequest(assetsRq));
                AccountRetDto asset = assets.Accounts[rdm.Next(0, assets.Accounts.Count)];

                AccountQueryRq cogsRq = new() { FullName = new() { "Company COGS" }, MaxReturned = null };
                AccountRs cogss = new(QB.ExecuteQbRequest(cogsRq));
                AccountRetDto cogs = cogss.Accounts.FirstOrDefault();

                for (int i = 1; i <= 5; i++)
                {
                    if (!qryRs.ItemInventory.Any(item => item.ManufacturerPartNumber.Equals($"12345-0{i}")))
                    {
                        string desc = $"{addRqName}.InventoryItem_0{i}";
                        addRq = new();
                        addRq.Name = desc;
                        addRq.IsActive = true;
                        addRq.ManufacturerPartNumber = $"12345-0{i}";
                        addRq.PurchaseDesc = desc;
                        addRq.PurchaseCost = 1M;
                        addRq.SalesDesc = desc;
                        addRq.SalesPrice = 2M;
                        addRq.IncomeAccount = new() { ListID = account.ListID };
                        addRq.AssetAccount = new() { ListID = asset.ListID };
                        addRq.COGSAccount = new() { ListID = cogs.ListID };
                        Assert.IsTrue(addRq.IsEntityValid());

                        result = QB.ExecuteQbRequest(addRq);
                        addRs = new(result);
                        if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                        Assert.IsTrue(addRs.StatusCode == "0");
                        Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    }
                }
                #endregion

                #region Mod Test
                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                foreach(ItemInventoryRetDto item in qryRs.ItemInventory)
                {
                    modRq = new();
                    modRq.ListID = item.ListID;
                    modRq.EditSequence = item.EditSequence;
                    modRq.Name = item.Name;
                    modRq.PurchaseDesc = $"{modRq.GetType().Name} updated on {DateTime.Now}";
                    modRq.PurchaseCost = 1.11M;
                    modRq.SalesPrice = 2.22M;
                    Assert.IsTrue(modRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(modRq);
                    modRs = new(result);
                    if (modRs.StatusCode == "3250") Assert.Inconclusive(modRs.StatusMessage);
                    Assert.IsTrue(modRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(modRs.ParseError));
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
