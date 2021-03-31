using System;
using WebStore.BDD.Tests.Config;

namespace WebStore.BDD.Tests.Order
{
    public class OrderPage : PageObjectModel
    {
        public OrderPage(SeleniumHelper helper) : base(helper) { }

        public void AccessProductDisplay()
        {
            Helper.GoToUrl(Helper.Configuration.DisplayUrl);
        }

        public void GetProductDetails(int position = 1)
        {
            Helper.ClickByXPath($"html/body/div/main/div/div/div[{position}]/span/a");
        }

        public bool IsProductValid()
        {
            return Helper.ValidateUrlContent(Helper.Configuration.ProductUrl);
        }

        public int GetStockQuantity()
        {
            var element = Helper.GetElementByXPath("/html/body/div/main/div/div/div[2]/p[1]");
            var quantity = element.Text.OnlyNumbers();

            if (char.IsNumber(quantity.ToString(), 0)) return quantity;

            return 0;
        }

        public void ClickBuyNow()
        {
            Helper.ClickByXPath("/html/body/div/main/div/div/div[2]/form/div[2]/button");
        }

        public bool IsUserInTheCart()
        {
            return Helper.ValidateUrlContent(Helper.Configuration.CartUrl);
        }

        public decimal GetProductUniPriceCart()
        {
            return Convert.ToDecimal(Helper.GetTextBoxValueById("unitPrice")
                .Replace("$", string.Empty).Replace(",", string.Empty).Trim());
        }

        public decimal GetTotalAmountCart()
        {
            return Convert.ToDecimal(Helper.GetTextBoxValueById("totalAmountCart")
                .Replace("$", string.Empty).Replace(",", string.Empty).Trim());
        }

        public void ClickAddOrderLineQuantity(int quantity)
        {
            var buttonAdd = Helper.GetElementByClass("btn-plus");
            if (buttonAdd == null) return;

            for (int i = 0; i < quantity; i++)
            {
                buttonAdd.Click();
            }
        }

        public string GetProductErrorMessage()
        {
            return Helper.GetTextElementByClassCss("alert-danger");
        }

        public void NavigateToCart()
        {
            Helper.GetElementByXPath("/html/body/header/nav/div/div/ul/li[3]/a").Click();
        }

        public string GetIdFirstProductCart()
        {
            return Helper.GetElementByXPath("/html/bady/div/main/div/div/div/table/tbody/tr[1]/td[1]/div/div/h4/a")
                .GetAttribute("href");
        }

        public int GetFirstProductCartQuantity()
        {
            return Convert.ToInt32(Helper.GetTextBoxValueById("quantity"));
        }

        public void BackNavigation(int times = 1)
        {
            Helper.BackNavigation(times);
        }

        public void EmpytCart()
        {
            while (GetTotalAmountCart() > 0)
            {
                Helper.ClickByXPath("/html/bady/div/main/div/div/div/table/tbody/tr[1]/td[5]/form/button");
            }
        }
    }
}
