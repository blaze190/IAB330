using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace LogisticsManagerUITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            
        }

        /// <summary>
        /// Testing if the register button navigates correctly
        /// </summary>
        [Test]
        public void RegisterButton()
        {
            //navigate
            app.Tap("logInbuttonRegister");

            //check if element exists
            app.WaitForElement("registerEntryCompanyName");
            var appResult = app.Query("registerEntryCompanyName").FirstOrDefault();

            Assert.IsTrue(appResult != null);
        }

        /// <summary>
        /// Testing if a user can correcly register
        /// </summary>
        [Test]
        public void Register()
        {

            //enter valid information
            app.Tap("logInbuttonRegister");
            app.EnterText("registerEntryCompanyName", "Generic Inc.");
            app.DismissKeyboard();
            app.EnterText("registerEntryEmail", "generic@example.com");
            app.DismissKeyboard();
            app.EnterText("registerEntryUsername", "generic-admin");
            app.DismissKeyboard();
            app.EnterText("registerEntryPassword", "generic-password");
            app.DismissKeyboard();
            app.EnterText("registerEntryReEnterPassword", "generic-password");
            app.DismissKeyboard();

            //submit
            app.Tap("registerButtonSubmit");

            //check if element exists
            app.WaitForElement("logInEntryUsername");
            var appResult = app.Query("logInEntryUsername").FirstOrDefault();

            Assert.IsTrue(appResult != null);
        }

        /// <summary>
        /// Testing if a user can correctly log in
        /// </summary>
        [Test]
        public void LogIn()
        {

            //Register
            app.Tap("logInbuttonRegister");
            app.EnterText("registerEntryCompanyName", "Generic Inc.");
            app.DismissKeyboard();
            app.EnterText("registerEntryEmail", "generic@example.com");
            app.DismissKeyboard();
            app.EnterText("registerEntryUsername", "generic-admin");
            app.DismissKeyboard();
            app.EnterText("registerEntryPassword", "generic-password");
            app.DismissKeyboard();
            app.EnterText("registerEntryReEnterPassword", "generic-password");
            app.DismissKeyboard();
            app.Tap("registerButtonSubmit");

            //enter valid information
            app.EnterText("logInEntryUsername", "generic-admin");
            app.DismissKeyboard();
            app.EnterText("logInEntryPassword", "generic-password");
            app.DismissKeyboard();

            //submit
            app.Tap("logInbuttonLogIn");

            //check if element exists
            app.WaitForElement("mainPageLabelClock");
            var appResult = app.Query("mainPageLabelClock").FirstOrDefault();

            Assert.IsTrue(appResult != null);
        }

        /// <summary>
        /// Testing if a wrong password returns an error
        /// </summary>
        [Test]
        public void WrongPassword()
        {
            //Register
            app.Tap("logInbuttonRegister");
            app.EnterText("registerEntryCompanyName", "Generic Inc.");
            app.DismissKeyboard();
            app.EnterText("registerEntryEmail", "generic@example.com");
            app.DismissKeyboard();
            app.EnterText("registerEntryUsername", "generic-admin");
            app.DismissKeyboard();
            app.EnterText("registerEntryPassword", "generic-password");
            app.DismissKeyboard();
            app.EnterText("registerEntryReEnterPassword", "generic-password");
            app.DismissKeyboard();
            app.Tap("registerButtonSubmit");

            //enter valid username and invalid password
            app.EnterText("logInEntryUsername", "generic-admin");
            app.DismissKeyboard();
            app.EnterText("logInEntryPassword", "wrongPassword");
            app.DismissKeyboard();

            //submit
            app.Tap("logInbuttonLogIn");

            //check if element exists
            var appResult = app.Query("mainPageLabelClock").FirstOrDefault();

            Assert.IsFalse(appResult != null);
        }

    }
}
