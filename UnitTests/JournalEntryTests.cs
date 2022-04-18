using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class JournalEntryTests
    {
        [TestMethod]
        public void TestJournalEntryQueryRq()
        {
            JournalEntryQueryRq journalEntryRq = new();
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.TxnID = new() { "JournalEntryQueryRq.TxnID" };
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.TxnID = null;
            journalEntryRq.RefNumber = new() { "JournalEntryQueryRq.FullName" };
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.TxnDateRangeFilter = new();
            journalEntryRq.TxnDateRangeFilter.FromTxnDate = DateTime.Now.AddDays(-365);
            journalEntryRq.TxnDateRangeFilter.ToTxnDate = DateTime.Now;
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.ModifiedDateRangeFilter = new();
            journalEntryRq.ModifiedDateRangeFilter.FromModifiedDate = DateTime.Now.AddDays(-365);
            journalEntryRq.ModifiedDateRangeFilter.ToModifiedDate = DateTime.Now;
            Assert.IsFalse(journalEntryRq.IsEntityValid());

            journalEntryRq.TxnDateRangeFilter = null;
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            var model = new QryRqModel<JournalEntryQueryRq>();
            model.SetRequest(journalEntryRq, "QryRq");
            Assert.IsTrue(journalEntryRq.ToString().Contains("<JournalEntryQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<JournalEntryQueryRq>"));
        }

        [TestMethod]
        public void TestJournalEntryAddRq()
        {
            JournalEntryAddRq journalEntryRq = new();
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.TxnDate = DateTime.Now;
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.IsHomeCurrencyAdjustment = true;
            journalEntryRq.IsAmountsEnteredInHomeCurrency = true;
            Assert.IsFalse(journalEntryRq.IsEntityValid());

            journalEntryRq.IsAmountsEnteredInHomeCurrency = null;
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.CreditLine = new();
            journalEntryRq.DebitLine = new();
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.DebitLine = null;
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            var model = new AddRqModel<JournalEntryAddRq>("JournalEntryAdd");
            model.SetRequest(journalEntryRq, "AddRq");
            Assert.IsTrue(journalEntryRq.ToString().Contains("<JournalEntryAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<JournalEntryAddRq>"));
        }

        [TestMethod]
        public void TestJournalEntryModRq()
        {
            JournalEntryModRq journalEntryRq = new();
            Assert.IsFalse(journalEntryRq.IsEntityValid());

            journalEntryRq.TxnID = "JournalEntryModRq.TxnID";
            journalEntryRq.EditSequence = "JournalEntryModRq.EditSequence";
            journalEntryRq.TxnDate = DateTime.Now;
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.JournalLineMod = new();
            journalEntryRq.JournalLineMod.Add(new() { TxnLineID = "JournalEntryModRq.TxnLineID" });
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            journalEntryRq.JournalLineMod[0].BillableStatus = BillStatus.Billable;
            Assert.IsTrue(journalEntryRq.IsEntityValid());

            var model = new ModRqModel<JournalEntryModRq>("JournalEntryMod");
            model.SetRequest(journalEntryRq, "ModRq");
            Assert.IsTrue(journalEntryRq.ToString().Contains("<JournalEntryModRq>"));
            Assert.IsTrue(model.ToString().Contains("<JournalEntryModRq>"));
        }
    }
}