using TechTalk.SpecFlow;
using WebStore.BDD.Tests.Config;
using Xunit;

namespace WebStore.BDD.Tests.User
{
    [Binding]
    [CollectionDefinition(nameof(WebTestsFixtureAutomationCollection))]
    public class CommomSteps
    {
        private readonly UserRegisterPage _userRegisterPage;
        private readonly WebTestsFixtureAutomation _testsFixture;

        public CommomSteps(WebTestsFixtureAutomation testsFixture)
        {
            _testsFixture = testsFixture;
            _userRegisterPage = new UserRegisterPage(testsFixture.BrowserHelper);
        }

        [Given(@"a vistor accessing the WebStore")]
        public void GivenAVistorAccessingTheWebStore()
        {
            // Act
            _userRegisterPage.GoToStorePage();

            // Assert
            Assert.Contains(_testsFixture.Configuration.DomainUrl, _userRegisterPage.GetUrl());
        }

        [Then(@"user will be redirected to the home page")]
        public void ThenUserWillBeRedirectedToTheHomePage()
        {
            // Act
            _userRegisterPage.GoToStorePage();

            // Assert
            Assert.Contains(_testsFixture.Configuration.DisplayUrl, _userRegisterPage.GetUrl());
        }

        [Then(@"a greeting with the user's e-mail should be displayed on the top menu")]
        public void ThenAGreetingWithTheUserSE_MailShouldBeDisplayedOnTheTopMenu()
        {
            // Assert
            Assert.True(_userRegisterPage.ValidateGreetingLoggedUser(_testsFixture.User));
        }
    }
}
