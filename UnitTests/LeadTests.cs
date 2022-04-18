using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class LeadTests
    {
        [TestMethod]
        public void TestLeadModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                LeadRs qryRs, addRs = new(""), modRs;
                LeadAddRq addRq = new();
                LeadModRq modRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                LeadQueryRq qryRq = new();
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
                if (qryRs.TotalLeads == 0)
                {
                    addRq.FullName = $"{addRqName}.{addRq.GetType().Name}";
                    addRq.Status = LeadStatus.Cold;
                    addRq.MainPhone = "305-775-4754";
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion

                #region Mod Test
                LeadRetDto Lead = qryRs.TotalLeads == 0 ? addRs.Leads[0] : qryRs.Leads[0];
                modRq.ListID = Lead.ListID;
                modRq.EditSequence = Lead.EditSequence;
                modRq.FullName = $"{addRqName}.{modRq.GetType().Name}";
                modRq.Status = LeadStatus.Hot;
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
