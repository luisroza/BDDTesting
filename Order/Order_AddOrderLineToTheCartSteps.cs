using System;
using TechTalk.SpecFlow;
using WebStore.BDD.Tests.Config;
using WebStore.BDD.Tests.User;
using Xunit;

namespace WebStore.BDD.Tests.Order
{
    [Binding]
    [CollectionDefinition(nameof(WebTestsFixtureAutomationCollection))]
    public class Order_AddOrderLineToTheCartSteps
    {
        private readonly WebTestsFixtureAutomation _testsFixture;
        private readonly OrderPage _orderPage;
        private readonly UserLoginPage _userLoginPage;
        private string _productUrl;

        public Order_AddOrderLineToTheCartSteps(WebTestsFixtureAutomation testsFixture)
        {
            _testsFixture = testsFixture;
            _orderPage = new OrderPage(testsFixture.BrowserHelper);
            _userLoginPage = new UserLoginPage(testsFixture.BrowserHelper);
        }

        [Given(@"user is logged in")]
        public void GivenUserIsLoggedIn()
        {
            // Arrange
            var user = new User.User
            {
                Email = "test@test.com",
                Password = "Test@0001"
            };
            _testsFixture.User = user;

            // Act
            var login = _userLoginPage.LoginSucceeded(user);

            // Assert
            Assert.True(login);
        }

        [Given(@"a product on display")]
        public void GivenAProductOnDisplay()
        {
            // Arrange
            _orderPage.AccessProductDisplay();

            // Act
            _orderPage.GetProductDetails();
            _productUrl = _orderPage.GetUrl();

            // Assert
            Assert.True(_orderPage.IsProductValid());

        }
        
        [Given(@"there is available quantity in stock")]
        public void GivenThereIsAvailableQuantityInStock()
        {
            // Assert
            Assert.True(_orderPage.GetStockQuantity() > 0);
        }

        [Given(@"the product is already added into the cart")]
        public void GivenTheProductIsAlreadyAddedIntoTheCart()
        {
            // Act
            _orderPage.ClickBuyNow();
        }

        [Given(@"there is no product added into the cart")]
        public void GivenThereIsNoProductAddedIntoTheCart()
        {
            // Act
            _orderPage.NavigateToCart();
            _orderPage.EmpytCart();

            // Assert
            Assert.Equal(0, _orderPage.GetTotalAmountCart());

            _orderPage.NavigateToUrl(_productUrl);
        }


        [When(@"the user add an unit to the cart")]
        public void WhenTheUserAddAnUnitToTheCart()
        {
            // Act
            _orderPage.NavigateToCart();
            _orderPage.EmpytCart();
            _orderPage.AccessProductDisplay();
            _orderPage.GetProductDetails();
            _orderPage.ClickBuyNow();

            // Assert
            Assert.True(_orderPage.IsUserInTheCart());

            _orderPage.BackNavigation();
        }

        [When(@"the user add more items of an order line than allowed")]
        public void WhenTheUserAddMoreItemsOfAnOrderLineThanAllowed()
        {
            // Arrange
            _orderPage.ClickAddOrderLineQuantity(Convert.ToInt32(_testsFixture.Configuration.MAX_QUANTITY_ALLOWED) + 1);

            // Act
            _orderPage.ClickBuyNow();

            // Assert
        }

        [When(@"the user add more units to the cart")]
        public void WhenTheUserAddMoreUnitsToTheCart()
        {
            // Arrange
            _orderPage.EmpytCart();
            _orderPage.ClickAddOrderLineQuantity(Convert.ToInt32(_testsFixture.Configuration.MAX_QUANTITY_ALLOWED));

            // Act
            _orderPage.ClickBuyNow();

            // Assert
        }

        [Then(@"the user will be redirected to the order summary")]
        public void ThenTheUserWillBeRedirectedToTheOrderSummary()
        {
            // Assert
            Assert.True(_orderPage.IsUserInTheCart());
        }

        [Then(@"the order's total amount will add the unit price of the added order line")]
        public void ThenTheOrderSTotalAmountWillAddTheUnitPriceOfTheAddedOrderLine()
        {
            // Arrange
            var unitPrice = _orderPage.GetProductUniPriceCart();
            var cartValue = _orderPage.GetTotalAmountCart();

            // Assert
            Assert.Equal(unitPrice, cartValue);
        }

        [Then(@"a error message will be displayd informing quantity surpasses the allowed quantity")]
        public void ThenAErrorMessageWillBeDisplaydInformingQuantitySurpassesTheAllowedQuantity()
        {
            // Arrange
            var message = _orderPage.GetProductErrorMessage();

            // Assert
            Assert.Contains($"Maximum item quantity allowed is {_testsFixture.Configuration.MAX_QUANTITY_ALLOWED}", message);
        }

        [Then(@"the order line quantity must be increased by the quantity inputed")]
        public void ThenTheOrderLineQuantityMustBeIncreasedByTheQuantityInputed()
        {
            // Assert
            Assert.True(_orderPage.GetFirstProductCartQuantity() > 1);
        }

        [Then(@"the order's total amount must multiple the order line quantity by unit price")]
        public void ThenTheOrderSTotalAmountMustMultipleTheOrderLineQuantityByUnitPrice()
        {
            // Arrange
            var unitPrice = _orderPage.GetProductUniPriceCart();
            var cartValue = _orderPage.GetTotalAmountCart();
            var unitQuantity = _orderPage.GetFirstProductCartQuantity();

            // Assert
            Assert.Equal(unitPrice * unitQuantity, cartValue);
        }
    }
}
