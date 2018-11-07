using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticsManager;

namespace LogisticsManagerUnitTests
{
    [TestClass]
    public class CompanyTests
    {
        //Construct Company
        Company company = new Company();

        /// <summary>
        /// Testing that company ID assigns correctly
        /// </summary>
        [TestMethod]
        public void TestID()
        {
            int expectedID = 1;
            company.Id = 1;
            int actualID = company.Id;

            Assert.AreEqual(expectedID, actualID);
        }

        /// <summary>
        /// Testing that company Name assigns correctly
        /// </summary>
        [TestMethod]
        public void TestName()
        {
            string expectedName = "Company Inc.";
            company.Name = "Company Inc.";
            string actualName = company.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        /// <summary>
        /// Testing that global company variable assigns correctly
        /// </summary>
        [TestMethod]
        public void TestCompanyGlobal()
        {
            Company expected = company;
            Constants.company = company;
            Company actual = Constants.company;

            Assert.AreEqual(expected, actual);
        }
    }
}
