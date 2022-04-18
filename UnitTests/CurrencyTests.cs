using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod]
        public void TestCurrencyModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                CurrencyRs qryRs, addRs = new(""), modRs;
                CurrencyAddRq addRq = new();
                CurrencyModRq modRq = new();
                string addRqName = $"QPD";
                #endregion

                #region Query Test
                CurrencyQueryRq qryRq = new();
                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                Regex statusCodes = new(@"^0$|^3250$");
                Assert.IsTrue(statusCodes.IsMatch(qryRs.StatusCode));
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                if (qryRs.StatusCode == "3250") Assert.Inconclusive(qryRs.StatusMessage);
                #endregion

                #region Add Test
                if (qryRs.TotalCurrencys == 0)
                {
                    addRq.Name = addRqName;
                    addRq.IsActive = true;
                    addRq.CurrencyCode = addRqName;
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                CurrencyRetDto acct = qryRs.TotalCurrencys == 0 ? addRs.Currencys[0] : qryRs.Currencys[0];
                modRq.ListID = acct.ListID;
                modRq.EditSequence = acct.EditSequence;
                modRq.Name = acct.Name;
                modRq.CurrencyFormat = new() { ThousandSeparator = ThousandSeparator.Comma };
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
