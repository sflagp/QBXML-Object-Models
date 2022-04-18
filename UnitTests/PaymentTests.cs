using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;
using System.Linq;

namespace QbModels.Tests
{
    [TestClass]
    public class ReceivePaymentTests
    {
        [TestMethod]
        public void TestReceivePaymentQueryRq()
        {
            ReceivePaymentQueryRq receivePaymentRq = new();
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.TxnID = new() { "DepositQueryRq.TxnID" };
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.TxnID = null;
            receivePaymentRq.RefNumber = new() { "DepositQueryRq.FullName" };
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.TxnDateRangeFilter = new();
            receivePaymentRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            receivePaymentRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.ModifiedDateRangeFilter = new();
            receivePaymentRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            receivePaymentRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(receivePaymentRq.IsEntityValid());

            receivePaymentRq.TxnDateRangeFilter = null;
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.EntityFilter = new();
            receivePaymentRq.EntityFilter.ListID = new();
            receivePaymentRq.EntityFilter.FullName = new();
            Assert.IsFalse(receivePaymentRq.IsEntityValid());

            receivePaymentRq.EntityFilter.ListID = null;
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            var model = new QryRqModel<ReceivePaymentQueryRq>();
            model.SetRequest(receivePaymentRq, "QryRq");
            Assert.IsTrue(receivePaymentRq.ToString().Contains("<ReceivePaymentQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<ReceivePaymentQueryRq>"));
        }

        [TestMethod]
        public void TestReceivePaymentAddRq()
        {
            ReceivePaymentAddRq receivePaymentRq = new();
            Assert.IsFalse(receivePaymentRq.IsEntityValid());

            receivePaymentRq.Customer = new();
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.CreditCardTxnInfo = new();
            receivePaymentRq.CreditCardTxnInfo.CreditCardTxnInputInfo = new();
            receivePaymentRq.CreditCardTxnInfo.CreditCardTxnResultInfo = new();
            Assert.IsFalse(receivePaymentRq.IsEntityValid());
            Assert.IsTrue(receivePaymentRq.GetErrorsList().Any(e => e.Contains("CreditCardTransID", StringComparison.OrdinalIgnoreCase)));

            receivePaymentRq.CreditCardTxnInfo = null;
            receivePaymentRq.AppliedToTxn = new();
            receivePaymentRq.AppliedToTxn.Add(new());
            Assert.IsFalse(receivePaymentRq.IsEntityValid());

            receivePaymentRq.AppliedToTxn[0].TxnID = "AppliedToTxn.TxnID";
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.IsAutoApply = false;
            Assert.IsFalse(receivePaymentRq.IsEntityValid());

            receivePaymentRq.IsAutoApply = null;
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            var model = new AddRqModel<ReceivePaymentAddRq>("ReceivePaymentAdd");
            model.SetRequest(receivePaymentRq, "AddRq");
            Assert.IsTrue(receivePaymentRq.ToString().Contains("<ReceivePaymentAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<ReceivePaymentAddRq>"));
        }

        [TestMethod]
        public void TestReceivePaymentModRq()
        {
            ReceivePaymentModRq receivePaymentRq = new();
            Assert.IsFalse(receivePaymentRq.IsEntityValid());

            receivePaymentRq.TxnID = "ReceivePaymentModRq.TxnID";
            receivePaymentRq.EditSequence = "ReceivePaymentModRq.EditSequence";
            receivePaymentRq.TxnDate = DateTime.Now;
            receivePaymentRq.Customer = new();
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            receivePaymentRq.CreditCardTxnInfo = new();
            receivePaymentRq.CreditCardTxnInfo.CreditCardTxnInputInfo = new();
            receivePaymentRq.CreditCardTxnInfo.CreditCardTxnResultInfo = new();
            Assert.IsFalse(receivePaymentRq.IsEntityValid());
            Assert.IsTrue(receivePaymentRq.GetErrorsList().Any(e => e.Contains("CreditCardTransID", StringComparison.OrdinalIgnoreCase)));

            receivePaymentRq.CreditCardTxnInfo = null;
            receivePaymentRq.AppliedToTxn = new();
            receivePaymentRq.AppliedToTxn.Add(new());
            Assert.IsFalse(receivePaymentRq.IsEntityValid());

            receivePaymentRq.AppliedToTxn[0].TxnID = "AppliedToTxn.TxnID";
            Assert.IsTrue(receivePaymentRq.IsEntityValid());

            var model = new ModRqModel<ReceivePaymentModRq>("ReceivePaymentMod");
            model.SetRequest(receivePaymentRq, "ModRq");
            Assert.IsTrue(receivePaymentRq.ToString().Contains("<ReceivePaymentModRq>"));
            Assert.IsTrue(model.ToString().Contains("<ReceivePaymentModRq>"));
        }
    }

    [TestClass]
    public class PaymentMethodTests
    {
        [TestMethod]
        public void TestPaymentMethodQueryRq()
        {
            PaymentMethodQueryRq paymentMethodRq = new();
            Assert.IsTrue(paymentMethodRq.IsEntityValid());

            paymentMethodRq.ListID = new() { "EmployeeQueryRq.ListID" };
            paymentMethodRq.MaxReturned = -1;
            Assert.IsTrue(paymentMethodRq.IsEntityValid());

            paymentMethodRq.ListID = null;
            paymentMethodRq.FullName = new() { "EmployeeQueryRq.FullName" };
            Assert.IsTrue(paymentMethodRq.IsEntityValid());

            paymentMethodRq.FullName = null;
            paymentMethodRq.NameFilter = new();
            paymentMethodRq.MaxReturned = 99999;
            Assert.IsFalse(paymentMethodRq.IsEntityValid());

            paymentMethodRq.NameFilter.MatchCriterion = MatchCriterion.None;
            paymentMethodRq.NameFilter.Name = "A";
            Assert.IsFalse(paymentMethodRq.IsEntityValid());

            paymentMethodRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(paymentMethodRq.IsEntityValid());

            paymentMethodRq.PaymentMethodType = PaymentMethodType.None;
            Assert.IsFalse(paymentMethodRq.IsEntityValid());

            paymentMethodRq.PaymentMethodType = PaymentMethodType.Cash;
            Assert.IsTrue(paymentMethodRq.IsEntityValid());

            var model = new QryRqModel<PaymentMethodQueryRq>();
            model.SetRequest(paymentMethodRq, "QryRq");
            Assert.IsTrue(paymentMethodRq.ToString().Contains("<PaymentMethodQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<PaymentMethodQueryRq>"));
        }

        [TestMethod]
        public void TestPaymentMethodAddRq()
        {
            PaymentMethodAddRq paymentMethodRq = new();
            Assert.IsFalse(paymentMethodRq.IsEntityValid());

            paymentMethodRq.Name = "PaymentMethodAddRq.Name";
            Assert.IsTrue(paymentMethodRq.IsEntityValid());

            paymentMethodRq.PaymentMethodType = PaymentMethodType.None;
            Assert.IsFalse(paymentMethodRq.IsEntityValid());

            paymentMethodRq.PaymentMethodType = PaymentMethodType.Cash;
            Assert.IsTrue(paymentMethodRq.IsEntityValid());

            var model = new AddRqModel<PaymentMethodAddRq>("PaymentMethodAdd");
            model.SetRequest(paymentMethodRq, "AddRq");
            Assert.IsTrue(paymentMethodRq.ToString().Contains("<PaymentMethodAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<PaymentMethodAddRq>"));
        }
    }
}
