using WebStore.BDD.Tests.Config;

namespace WebStore.BDD.Tests.User
{
    public class UserLoginPage : UserPageBase
    {
        public UserLoginPage(SeleniumHelper helper) : base(helper) { }

        public void ClickLoginLink()
        {
            Helper.ClickLinkText("Login");
        }

        public void FillLoginForm(User user)
        {
            Helper.FillTextBoxById("Input_Email", user.Email);
            Helper.FillTextBoxById("Input_Password", user.Password);
        }

        public bool IsLogingFormFilledProperly(User user)
        {
            if (Helper.GetTextBoxValueById("Input_Email") != user.Email) return false;
            if (Helper.GetTextBoxValueById("Input_Password") != user.Password) return false;

            return true;
        }

        public void ClickLoginButton()
        {
            var button = Helper.GetElementByXPath("//*[@id='account']/div[5]/button");
            button.Click();
        }

        public bool LoginSucceeded(User user)
        {
            GoToStorePage();
            ClickLoginLink();
            FillLoginForm(user);

            if (!IsLogingFormFilledProperly(user)) return false;
            ClickLoginButton();
            if (!ValidateGreetingLoggedUser(user)) return false;

            return true;
        }
    }
}
