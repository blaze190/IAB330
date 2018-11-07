using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticsManager;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace LogisticsManagerUnitTests
{
    [TestClass]
    public class UserTests
    {
        //Construct User
        User user = new User();

        /// <summary>
        /// Testing assigning a user's username
        /// </summary>
        [TestMethod]
        public void TestUsername()
        {
            string expected = "username123";
            user.Username = "username123";
            string actual = user.Username;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual("wrong", actual);
        }

        /// <summary>
        /// Testing assigning a user's ID
        /// </summary>
        [TestMethod]
        public void TestID()
        {
            int expected = 1;
            user.Id = 1;
            int actual = user.Id;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning a user's access level
        /// </summary>
        [TestMethod]
        public void TestAccessLevel()
        {
            int expected = 10;
            user.AccessLevel = 10;
            int actual = user.AccessLevel;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning a user's company id
        /// </summary>
        [TestMethod]
        public void TestCompanyID()
        {
            Company company = new Company();
            company.Id = 1;

            int expected = 1;
            user.CompanyID = company.Id;
            int actual = user.CompanyID;

            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(2, actual);
        }

        /// <summary>
        /// Testing assigning a global user variable
        /// </summary>
        [TestMethod]
        public void TestUserGlobal()
        {
            User expected = user;
            Constants.user = user;
            User actual = Constants.user;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing assigning, hashing and salting a user's password
        /// </summary>
        [TestMethod]
        public void TestHashPassword()
        {
            string plaintextPassword = "password";

            //set password
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(plaintextPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            string expectedPassword = savedPasswordHash;
            user.Password = savedPasswordHash;
            string actualPassword = user.Password;

            //Assert
            Assert.AreEqual(expectedPassword,actualPassword);
            Assert.AreNotEqual(plaintextPassword, actualPassword);

        }

        /// <summary>
        /// Testing if a user's password can be unhashed correctly
        /// </summary>
        [TestMethod]
        public void TestUnhashPassword()
        {
            string plaintextPassword = "password";

            //hash password
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(plaintextPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            user.Password = savedPasswordHash;

            //unhash password
            string actualSavedPasswordHash = user.Password;
            byte[] actualHashBytes = Convert.FromBase64String(actualSavedPasswordHash);
            byte[] actualSalt = new byte[16];
            Array.Copy(actualHashBytes, 0, actualSalt, 0, 16);
            var actualPbkdf2 = new Rfc2898DeriveBytes(plaintextPassword, actualSalt, 10000);
            byte[] actualHash = actualPbkdf2.GetBytes(20);

            //Assert
            for (int i = 0; i < 20; i++)
            {
                if (actualHashBytes[i + 16] != actualHash[i])
                    Assert.Fail();
            }
        }
    }
}
