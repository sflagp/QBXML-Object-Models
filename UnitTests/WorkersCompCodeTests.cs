using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class WorkersCompCodeTests
    {
        [TestMethod]
        public void TestWorkersCompCodeModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbWorkersCompCodesView qryRs, addRs = new(""), modRs;
                WorkersCompCodeAddRq addRq = new();
                WorkersCompCodeModRq modRq = new();
                string addRqName = $"QbProcessor.WorkersCompCode";
                #endregion

                #region Query Test
                WorkersCompCodeQueryRq qryRq = new();
                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = "StartsWith" };
                qryRq.ActiveStatus = "All";
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRs = new(QB.ExecuteQbRequest(qryRq));
                if (qryRs.StatusCode == "3250") Assert.Inconclusive(qryRs.StatusMessage);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalWorkersCompCodes == 0)
                {
                    addRq.Name = addRqName;
                    addRq.IsActive = true;
                    addRq.RateEntry = new();
                    addRq.RateEntry.Add(new() { Rate = "100.00", EffectiveDate = DateTime.Today.AddDays(-7) });
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                WorkersCompCodeRetDto acct = qryRs.TotalWorkersCompCodes == 0 ? addRs.WorkersCompCodes[0] : qryRs.WorkersCompCodes[0];
                modRq.ListID = acct.ListID;
                modRq.EditSequence = acct.EditSequence;
                modRq.IsActive = true;
                modRq.Desc = $"{addRqName} modified by {modRq.GetType().Name} on {DateTime.Now}";
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
