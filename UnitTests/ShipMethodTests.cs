using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace QbProcessor.TEST
{
    [TestClass]
    public class ShipMethodTests
    {
        [TestMethod]
        public void TestShipMethodModel()
        {
            using (QBProcessor.QbProcessor QB = new())
            {
                #region Properties
                if (QB == null)
                {
                    throw new Exception("Quickbooks not loaded or error connecting to Quickbooks.");
                }

                QbShipMethodsView qryRs, addRs;
                ShipMethodQueryRq qryRq;
                ShipMethodAddRq addRq;

                string addRqName = "QbProcessor";
                string result;
                #endregion

                #region Query Test
                qryRq = new();
                Assert.IsTrue(qryRq.IsEntityValid());

                result = QB.ExecuteQbRequest(qryRq);
                qryRs = new(result);
                Regex statusCodes =  new(@"^0$|^3250$");
                Assert.IsTrue(statusCodes.IsMatch(qryRs.StatusCode));
                Assert.IsTrue(string.IsNullOrEmpty(qryRs.ParseError));
                if (qryRs.StatusCode == "3250") Assert.Inconclusive(qryRs.StatusMessage);
                #endregion

                #region Add Test
                if (qryRs.TotalShipMethods == 0)
                {
                    addRq = new()
                    {
                        Name = addRqName,
                        IsActive = true
                    };
                    Assert.IsTrue(addRq.IsEntityValid());

                    addRs = new(QB.ExecuteQbRequest(addRq));
                    Assert.IsTrue(addRs.StatusCode == "0");
                    Assert.IsTrue(string.IsNullOrEmpty(addRs.ParseError));
                    Assert.IsTrue(addRs.TotalShipMethods > 0);
                }
                #endregion
            }
            Thread.Sleep(2000);
        }
    }
}
