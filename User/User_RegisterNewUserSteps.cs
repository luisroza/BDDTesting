using TechTalk.SpecFlow;
using WebStore.BDD.Tests.Config;
using Xunit;

namespace WebStore.BDD.Tests.User
{
    [Binding]
    [CollectionDefinition(nameof(WebTestsFixtureAutomationCollection))]
    public class User_RegisterNewUserSteps
    {
        private readonly UserRegisterPage _userRegisterPage;
        private readonly WebTestsFixtureAutomation _testsFixture;

        public User_RegisterNewUserSteps(WebTestsFixtureAutomation testsFixture)
        {
            _testsFixture = testsFixture;
            _userRegisterPage = new UserRegisterPage(testsFixture.BrowserHelper);
        }

        [When(@"user clicks on Register")]
        public void WhenUserClicksOnRegister()
        {
            // Act
            _userRegisterPage.ClickRegisterButton();

            // Assert
            Assert.Contains(_testsFixture.Configuration.RegisterUrl, _userRegisterPage.GetUrl());
        }
        
        [When(@"fills correctly the form data")]
        public void WhenFillsCorrectlyTheFormData(Table table)
        {
            // Arrange
            _testsFixture.GenerateUserData();
            var user = _testsFixture.User;

            // Act
            _userRegisterPage.FillRegisterForm(user);

            // Assert
            Assert.True(_userRegisterPage.IsRegisterFormFilledProperly(user));
        }
        
        [When(@"clicks on register button")]
        public void WhenClicksOnRegisterButton()
        {
            // Assert
            _userRegisterPage.ClickRegisterButton();
        }
        
        [When(@"fills the form data with an password with no Uppercase")]
        public void WhenFillsTheFormDataWithAnPasswordWithNoUppercase(Table table)
        {
            // Arrange
            _testsFixture.GenerateUserData();
            var user = _testsFixture.User;
            user.Password = "test@0001";

            // Act
            _userRegisterPage.FillRegisterForm(user);

            // Assert
            Assert.True(_userRegisterPage.IsRegisterFormFilledProperly(user));
        }
        
        [When(@"fills the form data with an password with no special character")]
        public void WhenFillsTheFormDataWithAnPasswordWithNoSpecialCharacter(Table table)
        {
            // Arrange
            _testsFixture.GenerateUserData();
            var user = _testsFixture.User;
            user.Password = "Tests0001";

            // Act
            _userRegisterPage.FillRegisterForm(user);

            // Assert
            Assert.True(_userRegisterPage.IsRegisterFormFilledProperly(user));
        }
        
        [Then(@"user will receive an error message informing that the password must have at least one Uppercase")]
        public void ThenUserWillReceiveAnErrorMessageInformingThatThePasswordMustHaveAtLeastOneUppercase()
        {
            // Aseert
            Assert.True(_userRegisterPage.ValidateFormErrorMessage("Passwords must have at least one uppercase ('A'-'Z')"));
        }
        
        [Then(@"user will receive an error message informing that the password must have at least one special character")]
        public void ThenUserWillReceiveAnErrorMessageInformingThatThePasswordMustHaveAtLeastOneSpecialCharacter()
        {
            // Aseert
            Assert.True(_userRegisterPage.ValidateFormErrorMessage("Passwords must have at least one non alphanumeric character"));
        }
    }
}
