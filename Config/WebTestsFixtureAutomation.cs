using Bogus;
using Xunit;

namespace WebStore.BDD.Tests.Config
{
    [CollectionDefinition(nameof(WebTestsFixtureAutomationCollection))]
    public class WebTestsFixtureAutomationCollection : ICollectionFixture<WebTestsFixtureAutomation> {}
    public class WebTestsFixtureAutomation
    {
        public readonly ConfigurationHelper Configuration;
        public SeleniumHelper BrowserHelper;
        public User.User User;

        public WebTestsFixtureAutomation()
        {
            User = new User.User();
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, false);
        }

        public void GenerateUserData()
        {
            var faker = new Faker("en_US");
            User.Email = faker.Internet.Email().ToLower();
            User.Password = faker.Internet.Password(8, false, "", "@9Xy_");
        }
    }
}
