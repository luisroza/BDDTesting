using System;
using TechTalk.SpecFlow;
using WebStore.BDD.Tests.Config;
using Xunit;

namespace WebStore.BDD.Tests.User
{
    [Binding]
    [CollectionDefinition(nameof(WebTestsFixtureAutomationCollection))]
    public class User_LoginSteps
    {
        private readonly UserLoginPage _userLoginPage;
        private readonly WebTestsFixtureAutomation _testsFixture;

        public User_LoginSteps(WebTestsFixtureAutomation testsFixture)
        {
            _testsFixture = testsFixture;
            _userLoginPage = new UserLoginPage(testsFixture.BrowserHelper);
        }

        [When(@"user clicks on Login")]
        public void WhenUserClicksOnLogin()
        {
            // Act
            _userLoginPage.ClickLoginLink();

            //Assert
            Assert.Contains(_testsFixture.Configuration.LoginUrl, _userLoginPage.GetUrl());
        }
        
        [When(@"fills the login data form")]
        public void WhenFillsTheLoginDataForm(Table table)
        {
            // Arrange
            var user = new User
            {
                Email = "test@test.com",
                Password = "Test@0001"
            };
            _testsFixture.User = user;

            // Act
            _userLoginPage.FillLoginForm(user);

            // Assert
            Assert.True(_userLoginPage.IsLogingFormFilledProperly(user));
        }
        
        [When(@"clicks on login button")]
        public void WhenClicksOnLoginButton()
        {
            // Act
            _userLoginPage.ClickLoginButton();

            // Assert
            Assert.Contains(_testsFixture.Configuration.DisplayUrl, _userLoginPage.GetUrl());
        }
    }
}
