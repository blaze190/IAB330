using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticsManager;

namespace LogisticsManagerUnitTests
{
    [TestClass]
    public class ClockTests
    {
        //Construct Clock
        Clock clock = new Clock();

        /// <summary>
        /// Testing assigning ID to a clock
        /// </summary>
        [TestMethod]
        public void TestID()
        {
            int expected = 1;
            clock.Id = 1;
            int actual = clock.Id;

            Assert.AreEqual(expected,actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning Clock In to a clock
        /// </summary>
        [TestMethod]
        public void TestClockIn()
        {
            DateTime currentTime = DateTime.Now;

            DateTime expected = currentTime;
            clock.ClockIn = currentTime;
            DateTime actual = clock.ClockIn;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(DateTime.Parse("1/1/0001 12:00:00 AM"), actual);
        }

        /// <summary>
        /// Testing assigning Clock Out to a clock
        /// </summary>
        [TestMethod]
        public void TestClockOut()
        {
            DateTime currentTime = DateTime.Now;

            DateTime expected = currentTime;
            clock.ClockOut = currentTime;
            DateTime actual = clock.ClockOut;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(DateTime.Parse("1/1/0001 12:00:00 AM"), actual);
        }

        /// <summary>
        /// Testing assigning user id to a clock
        /// </summary>
        [TestMethod]
        public void TestUserID()
        {
            int expected = 1;
            clock.UserID = 1;
            int actual = clock.UserID;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }
    }
}
