using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class CreditCardChargeTests
    {
        [TestMethod]
        public void TestCreditCardChargeQueryRq()
        {
            CreditCardChargeQueryRq ccChargeRq = new();
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            ccChargeRq.TxnID = new() { "CreditCardChargeQueryRq.TxnID" };
            ccChargeRq.RefNumber = new() { "CreditCardChargeQueryRq.RefNumber" };
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.RefNumber = null;
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            ccChargeRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            ccChargeRq.RefNumberFilter = new();
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.RefNumberFilter.RefNumber = "RefNumberFilter.RefNumber";
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.RefNumberFilter.MatchCriterion = MatchCriterion.None;
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.RefNumberFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            var model = new QryRqModel<CreditCardChargeQueryRq>();
            model.SetRequest(ccChargeRq, "QryRq");
            Assert.IsTrue(ccChargeRq.ToString().Contains("<CreditCardChargeQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditCardChargeQueryRq>"));
        }

        [TestMethod]
        public void TestCreditCardChargeAddRq()
        {
            CreditCardChargeAddRq ccChargeRq = new();
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.Account = new();
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            ccChargeRq.PayeeEntity = new();
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            ccChargeRq.ItemLine = new();
            ccChargeRq.ItemGroupLine = new();
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.ItemGroupLine = null;
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            ccChargeRq.ItemLine = null;
            ccChargeRq.ItemGroupLine = new() { ItemGroup = new() };
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            var model = new AddRqModel<CreditCardChargeAddRq>("CreditCardChargeAdd");
            model.SetRequest(ccChargeRq, "AddRq");
            Assert.IsTrue(ccChargeRq.ToString().Contains("<CreditCardChargeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditCardChargeAddRq>"));
        }

        [TestMethod]
        public void TestCreditCardChargeModRq()
        {
            CreditCardChargeModRq ccChargeRq = new();
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.TxnID = "CreditCardChargeModRq.TxnID";
            ccChargeRq.EditSequence = null;
            ccChargeRq.TxnDate = DateTime.Now;
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.TxnID = null;
            ccChargeRq.EditSequence = "CreditCardChargeModRq.EditSequence";
            Assert.IsFalse(ccChargeRq.IsEntityValid());

            ccChargeRq.TxnID = "CreditCardChargeModRq.TxnID";
            ccChargeRq.EditSequence = "CreditCardChargeModRq.EditSequence";
            ccChargeRq.Account = new();
            Assert.IsTrue(ccChargeRq.IsEntityValid());

            var model = new ModRqModel<CreditCardChargeModRq>("CreditCardChargeMod");
            model.SetRequest(ccChargeRq, "ModRq");
            Assert.IsTrue(ccChargeRq.ToString().Contains("<CreditCardChargeModRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditCardChargeModRq>"));
        }
    }

    [TestClass]
    public class CreditCardCreditTests
    {
        [TestMethod]
        public void TestCreditCardCreditQueryRq()
        {
            CreditCardCreditQueryRq ccCreditRq = new();
            Assert.IsTrue(ccCreditRq.IsEntityValid());

            ccCreditRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(ccCreditRq.IsEntityValid());

            ccCreditRq.RefNumberFilter = new();
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.RefNumberFilter.RefNumber = "RefNumberFilter.RefNumber";
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.RefNumberFilter.MatchCriterion = MatchCriterion.None;
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.RefNumberFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(ccCreditRq.IsEntityValid());

            var model = new QryRqModel<CreditCardCreditQueryRq>();
            model.SetRequest(ccCreditRq, "QryRq");
            Assert.IsTrue(ccCreditRq.ToString().Contains("<CreditCardCreditQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditCardCreditQueryRq>"));
        }

        [TestMethod]
        public void TestCreditCardCreditAddRq()
        {
            CreditCardCreditAddRq ccCreditRq = new();
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.ItemLine = new();
            ccCreditRq.ItemGroupLine = new();
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.Account = new();
            ccCreditRq.ItemGroupLine = null;
            Assert.IsTrue(ccCreditRq.IsEntityValid());

            ccCreditRq.ItemLine = null;
            ccCreditRq.ItemGroupLine = new() { ItemGroup = new() };
            Assert.IsTrue(ccCreditRq.IsEntityValid());

            var model = new AddRqModel<CreditCardCreditAddRq>("CreditCardCreditAdd");
            model.SetRequest(ccCreditRq, "AddRq");
            Assert.IsTrue(ccCreditRq.ToString().Contains("<CreditCardCreditAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditCardCreditAddRq>"));
        }

        [TestMethod]
        public void TestCreditCardCreditModRq()
        {
            CreditCardCreditModRq ccCreditRq = new();
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.TxnID = "CreditCardCreditModRq.TxnID";
            ccCreditRq.EditSequence = null;
            ccCreditRq.TxnDate = DateTime.Now;
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.TxnID = null;
            ccCreditRq.EditSequence = "CreditCardCreditModRq.EditSequence";
            Assert.IsFalse(ccCreditRq.IsEntityValid());

            ccCreditRq.Account = new();
            ccCreditRq.TxnID = "CreditCardCreditModRq.TxnID";
            ccCreditRq.EditSequence = "CreditCardCreditModRq.EditSequence";
            Assert.IsTrue(ccCreditRq.IsEntityValid());

            var model = new ModRqModel<CreditCardCreditModRq>("CreditCardCreditMod");
            model.SetRequest(ccCreditRq, "ModRq");
            Assert.IsTrue(ccCreditRq.ToString().Contains("<CreditCardCreditModRq>"));
            Assert.IsTrue(model.ToString().Contains("<CreditCardCreditModRq>"));
        }
    }

    [TestClass]
    public class BillPaymentCreditCardTests
    {
        [TestMethod]
        public void TestBillPaymentCreditCardQueryRq()
        {
            BillPaymentCreditCardQueryRq ccBillPmtRq = new();
            Assert.IsTrue(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.RefNumberFilter = new();
            Assert.IsFalse(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.RefNumberFilter.RefNumber = "RefNumberFilter.RefNumber";
            Assert.IsFalse(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.RefNumberFilter.MatchCriterion = MatchCriterion.None;
            Assert.IsFalse(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.RefNumberFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(ccBillPmtRq.IsEntityValid());

            var model = new QryRqModel<BillPaymentCreditCardQueryRq>();
            model.SetRequest(ccBillPmtRq, "QryRq");
            Assert.IsTrue(ccBillPmtRq.ToString().Contains("<BillPaymentCreditCardQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillPaymentCreditCardQueryRq>"));
        }

        [TestMethod]
        public void TestBillPaymentCreditCardAddRq()
        {
            BillPaymentCreditCardAddRq ccBillPmtRq = new();
            Assert.IsFalse(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.PayeeEntity = new();
            ccBillPmtRq.AppliedToTxnAdd = new();
            ccBillPmtRq.AppliedToTxnAdd.Add(new() { TxnID = "AppliedToTxnAdd.TxnID" });
            ccBillPmtRq.CreditCardAccount = new();
            Assert.IsTrue(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.AppliedToTxnAdd = null;
            Assert.IsFalse(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.AppliedToTxnAdd = new();
            Assert.IsFalse(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.AppliedToTxnAdd.Add( new());
            Assert.IsFalse(ccBillPmtRq.IsEntityValid());

            ccBillPmtRq.AppliedToTxnAdd.Clear();
            ccBillPmtRq.AppliedToTxnAdd.Add(new() { TxnID = "AppliedToTxnAdd.TxnID" });
            Assert.IsTrue(ccBillPmtRq.IsEntityValid());

            var model = new AddRqModel<BillPaymentCreditCardAddRq>("BillPaymentCreditCardAdd");
            model.SetRequest(ccBillPmtRq, "AddRq");
            Assert.IsTrue(ccBillPmtRq.ToString().Contains("<BillPaymentCreditCardAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<BillPaymentCreditCardAddRq>"));
        }
    }

    [TestClass]
    public class ARRefundCreditCardTests
    {
        [TestMethod]
        public void TestARRefundCreditCardQueryRq()
        {
            ARRefundCreditCardQueryRq ccARRefundRq = new();
            Assert.IsTrue(ccARRefundRq.IsEntityValid());

            ccARRefundRq.PaidStatus = PaidStatus.All;
            Assert.IsTrue(ccARRefundRq.IsEntityValid());

            ccARRefundRq.RefNumberFilter = new();
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.RefNumberFilter.RefNumber = "RefNumberFilter.RefNumber";
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.RefNumberFilter.MatchCriterion = MatchCriterion.None;
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.RefNumberFilter.MatchCriterion = MatchCriterion.StartsWith;
            Assert.IsTrue(ccARRefundRq.IsEntityValid());

            var model = new QryRqModel<ARRefundCreditCardQueryRq>();
            model.SetRequest(ccARRefundRq, "QryRq");
            Assert.IsTrue(ccARRefundRq.ToString().Contains("<ARRefundCreditCardQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<ARRefundCreditCardQueryRq>"));
        }

        [TestMethod]
        public void TestARRefundCreditCardAddRq()
        {
            ARRefundCreditCardAddRq ccARRefundRq = new();
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.Customer = new();
            ccARRefundRq.RefundAppliedToTxnAdd = new();
            ccARRefundRq.RefundAppliedToTxnAdd.Add(new() { TxnID = "AppliedToTxnAdd.TxnID", RefundAmount = 123.45M });
            Assert.IsTrue(ccARRefundRq.IsEntityValid());

            ccARRefundRq.Customer = null;
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.Customer = new();
            ccARRefundRq.RefundAppliedToTxnAdd = new();
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.RefundAppliedToTxnAdd.Add(new());
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.RefundAppliedToTxnAdd.Clear();
            ccARRefundRq.RefundAppliedToTxnAdd.Add(new() { TxnID = "AppliedToTxnAdd.TxnID" });
            Assert.IsFalse(ccARRefundRq.IsEntityValid());

            ccARRefundRq.RefundAppliedToTxnAdd[0].RefundAmount = 123.45M;
            Assert.IsTrue(ccARRefundRq.IsEntityValid());

            var model = new AddRqModel<ARRefundCreditCardAddRq>("ARRefundCreditCardAdd");
            model.SetRequest(ccARRefundRq, "AddRq");
            Assert.IsTrue(ccARRefundRq.ToString().Contains("<ARRefundCreditCardAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<ARRefundCreditCardAddRq>"));
        }
    }
}
