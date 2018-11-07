using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticsManager;

namespace LogisticsManagerUnitTests
{
    [TestClass]
    public class TaskTests
    {
        //Construct Task
        Task task = new Task();

        /// <summary>
        /// Testing assigning Id to a task
        /// </summary>
        [TestMethod]
        public void TestID()
        {
            int expected = 1;
            task.Id = 1;
            int actual = task.Id;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2,actual);
        }

        /// <summary>
        /// Testing assigning company Id to a task
        /// </summary>
        [TestMethod]
        public void TestCompanyID()
        {
            int expected = 1;
            task.CompanyID = 1;
            int actual = task.CompanyID;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning access level to a task
        /// </summary>
        [TestMethod]
        public void TestAccessLevel()
        {
            int expected = 10;
            task.AccessLevel = 10;
            int actual = task.AccessLevel;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning name to a task
        /// </summary>
        [TestMethod]
        public void TestName()
        {
            string expected = "Task01";
            task.Name = "Task01";
            string actual = task.Name;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual("Wrong", actual);
        }

        /// <summary>
        /// Testing assigning desc to a task
        /// </summary>
        [TestMethod]
        public void TestDesc()
        {
            string expected = "description";
            task.Desc = "description";
            string actual = task.Desc;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual("Wrong", actual);
        }

    }
}
