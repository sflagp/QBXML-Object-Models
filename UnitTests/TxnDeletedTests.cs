using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using QbModels.ENUM;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class TxnDeletedQueryTests
    {
        [TestMethod]
        public void TestTxnDeletedModel()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                TxnDeletedRs qryRs;

                Regex acceptableCodes = new(@"^0$|^1$");
                string result;
                #endregion

                #region Cycle through Transaction Types
                TxnDeletedQueryRq qryRq = new();
                qryRq.DeletedDateRangeFilter = new() { FromDeletedDate = DateTime.Today.AddDays(-7), ToDeletedDate = DateTime.Today };
                Assert.IsFalse(qryRq.IsEntityValid());

                foreach(TxnDelType delType in Enum.GetValues(typeof(TxnDelType)))
                {
                    if (delType == TxnDelType.None) continue;
                    qryRq.TxnDelType = delType;
                    Assert.IsTrue(qryRq.IsEntityValid());

                    result = QB.ExecuteQbRequest(qryRq);
                    qryRs = new(result);
                    Assert.IsTrue(acceptableCodes.IsMatch(qryRs.StatusCode));
                    Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));

                    if (qryRs.StatusCode == "0")
                    {
                        Assert.IsTrue(qryRs.TotalTxnsDeleted > 0);
                    }
                    else
                    {
                        Assert.IsTrue(qryRs.TotalTxnsDeleted == 0);
                    }
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
