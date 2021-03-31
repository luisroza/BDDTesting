using WebStore.BDD.Tests.Config;

namespace WebStore.BDD.Tests.User
{
    public abstract class UserPageBase : PageObjectModel
    {
        protected UserPageBase(SeleniumHelper helper) : base(helper) { }

        public void GoToStorePage()
        {
            Helper.GoToUrl(Helper.Configuration.DomainUrl);
        }

        public bool ValidateGreetingLoggedUser(User user)
        {
            return Helper.GetTextElementById("greetingUser").Contains(user.Email);
        }

        public bool ValidateFormErrorMessage(string message)
        {
            return Helper.GetTextElementByClassCss("text-danger").Contains(message);
        }
    }
}
