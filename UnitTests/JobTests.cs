using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class JobTypeTests
    {
        [TestMethod]
        public void TestJobTypeQueryRq()
        {
            JobTypeQueryRq custTypeRq = new();
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = new() { "JobTypeQueryRq.ListID" };
            custTypeRq.MaxReturned = -1;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.ListID = null;
            custTypeRq.FullName = new() { "JobTypeQueryRq.FullName" };
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.FullName = null;
            custTypeRq.NameFilter = new();
            custTypeRq.MaxReturned = 99999;
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.NameFilter.MatchCriterion = MatchCriterion.None;
            custTypeRq.NameFilter.Name = "A";
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            custTypeRq.NameRangeFilter = new();
            custTypeRq.NameRangeFilter.FromName = "A";
            custTypeRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.NameFilter = null;
            Assert.IsTrue(custTypeRq.IsEntityValid());

            var model = new QryRqModel<JobTypeQueryRq>();
            model.SetRequest(custTypeRq, "QryRq");
            Assert.IsTrue(model.ToString().Contains("<JobTypeQueryRq>"));
            Assert.IsTrue(custTypeRq.ToString().Contains("<JobTypeQueryRq>"));
        }

        [TestMethod]
        public void TestJobTypeAddRq()
        {
            JobTypeAddRq custTypeRq = new();
            Assert.IsFalse(custTypeRq.IsEntityValid());

            custTypeRq.Name = "JobTypeAddRq.JobType.Name";
            Assert.IsTrue(custTypeRq.IsEntityValid());

            var model = new AddRqModel<JobTypeAddRq>("JobTypeAdd");
            model.SetRequest(custTypeRq, "AddRq");
            Assert.IsTrue(custTypeRq.ToString().Contains("<JobTypeAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<JobTypeAddRq>"));
        }
    }
}