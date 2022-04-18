using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class JobTypeTests
    {
        [TestMethod]
        public void TestJobTypeModels()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                JobTypeRs qryRs, addRs;
                JobTypeAddRq addRq = new();
                string addRqName = $"QbProcessor";
                string result;
                #endregion

                #region Query Test
                JobTypeQueryRq qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                qryRq.NameFilter = new() { Name = addRqName, MatchCriterion = MatchCriterion.StartsWith };
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Assert.IsTrue(qryRs.StatusSeverity == "Info");
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                #endregion

                #region Add Test
                if (qryRs.TotalJobTypes == 0)
                {
                    addRq.Name = addRqName;
                    addRq.IsActive = true;
                    Assert.IsTrue(addRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(addRq);
                    addRs = new(result);
                    if (addRs.StatusCode == "3250") Assert.Inconclusive(addRs.StatusMessage);
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
