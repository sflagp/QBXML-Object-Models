using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;
using System;

namespace QbModels.Tests
{
    [TestClass]
    public class TodoTests
    {
        [TestMethod]
        public void TestTodoQueryRq()
        {
            ToDoQueryRq todoRq = new();
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.ListID = new() { "TodoQueryRq.ListID" };
            todoRq.DoneStatus = DoneStatus.NotDoneOnly;
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.MaxReturned = 99999;
            todoRq.NameFilter = new();
            todoRq.NameFilter.MatchCriterion = MatchCriterion.None;
            todoRq.NameFilter.Name = "A";
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.NameRangeFilter = new();
            todoRq.NameRangeFilter.FromName = "A";
            todoRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.NameFilter = null;
            Assert.IsTrue(todoRq.IsEntityValid());

            var model = new QryRqModel<ToDoQueryRq>();
            model.SetRequest(todoRq, "QryRq");
            Assert.IsTrue(todoRq.ToString().Contains("<ToDoQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<ToDoQueryRq>"));
        }

        [TestMethod]
        public void TestTodoAddRq()
        {
            ToDoAddRq todoRq = new();
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.Notes = "TodoAddRq.Notes";
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.ToDoType = ToDoType.None;
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.ToDoType = ToDoType.Call;
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.Priority = Priority.None;
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.Priority = Priority.Medium;
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.Customer = new();
            todoRq.Vendor = new();
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.Vendor = null;
            Assert.IsTrue(todoRq.IsEntityValid());

            var model = new AddRqModel<ToDoAddRq>("ToDoAdd");
            model.SetRequest(todoRq, "AddRq");
            Assert.IsTrue(todoRq.ToString().Contains("<ToDoAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<ToDoAddRq>"));
        }

        [TestMethod]
        public void TestTodoModRq()
        {
            ToDoModRq todoRq = new();
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.ListID = "TodoModRq.ListID";
            todoRq.EditSequence = "TodoModRq.EditSequence";
            todoRq.Notes = "TodoModRq.Notes";
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.ToDoType = ToDoType.None;
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.ToDoType = ToDoType.Call;
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.Priority = Priority.None;
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.Priority = Priority.Medium;
            Assert.IsTrue(todoRq.IsEntityValid());

            todoRq.Lead = new();
            todoRq.Customer = new();
            Assert.IsFalse(todoRq.IsEntityValid());

            todoRq.Customer = null;
            Assert.IsTrue(todoRq.IsEntityValid());

            var model = new ModRqModel<ToDoModRq>("ToDoMod");
            model.SetRequest(todoRq, "ModRq");
            Assert.IsTrue(todoRq.ToString().Contains("<ToDoModRq>"));
            Assert.IsTrue(model.ToString().Contains("<ToDoModRq>"));
        }
    }
}