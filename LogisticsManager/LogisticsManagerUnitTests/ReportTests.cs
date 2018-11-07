using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticsManager;

namespace LogisticsManagerUnitTests
{
    [TestClass]
    public class ReportTests
    {
        //Construct Report
        Report report = new Report();

        /// <summary>
        /// Testing assigning Id to a report
        /// </summary>
        [TestMethod]
        public void TestID()
        {
            int expected = 1;
            report.Id = 1;
            int actual = report.Id;

            Assert.AreEqual(expected,actual);
            Assert.AreNotEqual(2,actual);
        }

        /// <summary>
        /// Testing assigning Type to a report
        /// </summary>
        [TestMethod]
        public void TestType()
        {
            string expected = "Other";
            report.Type = "Other";
            string actual = report.Type;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual("wrong", actual);
        }

        /// <summary>
        /// Testing assigning desc to a report
        /// </summary>
        [TestMethod]
        public void TestDesc()
        {
            string expected = "description";
            report.Desc = "description";
            string actual = report.Desc;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual("wrong", actual);
        }

        /// <summary>
        /// Testing assigning company Id to a report
        /// </summary>
        [TestMethod]
        public void TestCompanyID()
        {
            int expected = 1;
            report.CompanyID = 1;
            int actual = report.CompanyID;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning from user Id to a report
        /// </summary>
        [TestMethod]
        public void TestFromUserId()
        {
            int expected = 1;
            report.FromUserID = 1;
            int actual = report.FromUserID;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning creation date to a report
        /// </summary>
        [TestMethod]
        public void TestCreationDate()
        {
            DateTime currentTime = DateTime.Now;

            DateTime expected = currentTime;
            report.CreationDate = currentTime;
            DateTime actual = report.CreationDate;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(DateTime.Parse("1/1/0001 12:00:00 AM"), actual);
        }

    }
}
