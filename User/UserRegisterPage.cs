using WebStore.BDD.Tests.Config;

namespace WebStore.BDD.Tests.User
{
    public class UserRegisterPage : UserPageBase
    {
        public UserRegisterPage(SeleniumHelper helper) : base(helper) { }

        public void ClickRegisterLink()
        {
            Helper.ClickLinkText("Register");
        }

        public void FillRegisterForm(User user)
        {
            Helper.FillTextBoxById("Input_Email", user.Email);
            Helper.FillTextBoxById("Input_Password", user.Password);
            Helper.FillTextBoxById("Input_ConfirmPassword", user.Password);
        }

        public bool IsRegisterFormFilledProperly(User user)
        {
            if (Helper.GetTextBoxValueById("Input_Email") != user.Email) return false;
            if (Helper.GetTextBoxValueById("Input_Password") != user.Password) return false;
            if (Helper.GetTextBoxValueById("Input_ConfirmPassword") != user.Password) return false;

            return true;
        }

        public void ClickRegisterButton()
        {
            var button = Helper.GetElementByXPath("/html/body/div/main/div/div/form/button");
            button.Click();
        }
    }
}
