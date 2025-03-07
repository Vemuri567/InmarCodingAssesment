using SpecFlowFramework;
using SpecFlowFramework.StepDefinitions;
using System;
using TechTalk.SpecFlow;
using UITestCases.Pages;

namespace UITestCases.StepDefinition
{
    [Binding]
    public class PurchaseWatchSteps:StepDefinitions
    {
        public PurchaseWatchSteps(GlobalSettings settings): base(settings) { }

        [Given(@"user is on the Magneto Home Page")]
        public void GivenUserIsOnTheMagnetoHomePage()
        {
            var homePage = new HomePage(Settings);
            homePage.NavigateToApplication();
        }

        [Then(@"User should see the signin button on the home page")]
        public void ThenUserShouldSeeTheSigninButtonOnTheHomePage()
        {
            var homePage = new HomePage(Settings);
            bool signInButton = homePage.VerifySIgnInButton();
            signInButton.Should().BeTrue("Sign button should be displayed in the Home Page");

        }

        [When(@"User clicks on the signin button on the home page")]
        public void WhenUserClicksOnTheSigninButtonOnTheHomePage()
        {
            var homePage = new HomePage(Settings);
            homePage.ClickOnHomePageSIgnInButton();
        }

        [Then(@"User should be redirected to the login screen")]
        public void ThenUserShouldBeRedirectedToTheLoginScreen()
        {
            var loginPage = new LoginPage(Settings);
            bool emailTextBox = loginPage.VerifyEmailTextBox();
            emailTextBox.Should().BeTrue("email text box should be displayed in the customer login screen ");
        }

        [When(@"User enters the email ""([^""]*)"" and password ""([^""]*)""")]
        public void WhenUserEntersTheEmailAndPassword(string email, string password)
        {
            var loginPage = new LoginPage(Settings);
            loginPage.EnterEmail(email);
            loginPage.EnterPassword(password);
        }

        [When(@"User clicks on the sign-in button")]
        public void WhenUserClicksOnTheSign_InButton()
        {
            var loginPage = new LoginPage(Settings);
            loginPage.ClickOnSIgnInButton();
        }

        [Then(@"User should see the welcome text on the home page")]
        public void ThenUserShouldSeeTheWelcomeTextOnTheHomePage()
        {
            var homePage = new HomePage(Settings);
            homePage.VerifyWelcomeText().Should().BeTrue();
        }

        [When(@"User clicks on the ""([^""]*)"" item")]
        public void WhenUserClicksOnTheItem(string menuItem)
        {
            var homePage = new HomePage(Settings);
            homePage.ClickOnMenuItem(menuItem);
        }

        [When(@"User clicks on the submenu ""([^""]*)"" item")]
        public void WhenUserClicksOnTheSubmenuItem(string subMenuItem)
        {
            var homePage = new HomePage(Settings);
            homePage.ClickOnSubMenuItem(subMenuItem);
        }



        [Then(@"User should see available shopping options")]
        public void ThenUserShouldSeeAvailableShoppingOptions()
        {
            var productsPage= new ProductsPage(Settings);
            productsPage.ShoppingOptionsCount().Should().BeGreaterThan(0);
        }

        [When(@"User selects the ""([^""]*)"" category")]
        public void WhenUserSelectsTheCategory(string category)
        {
            var productsPage = new ProductsPage(Settings);
            productsPage.ClickOnShoppingOptionsByCategory(category);
            
        }

        [When(@"User filters the ""([^""]*)""")]
        public void WhenUserFiltersThe(string subCategory)
        {
            var productsPage = new ProductsPage(Settings);
            productsPage.ClickOnShoppingOptionsBySubCategory(subCategory);
        }

        [Then(@"User should see the product ""([^""]*)"" in the product list")]
        public void ThenUserShouldSeeTheProductInTheProductList(string productName)
        {
            var productsPage = new ProductsPage(Settings);
            productsPage.VerifyProductName(productName).Should().BeTrue($"{productName} should be displayed");
            Settings.Scenario["productName"] = productName;

        }

        [When(@"User clicks on the product ""([^""]*)""")]
        public void WhenUserClicksOnTheProduct(string productName)
        {
            var productsPage = new ProductsPage(Settings);
            productsPage.ClickOnProductName(productName);
        }

        [Then(@"User should be directed to the product details page")]
        public void ThenUserShouldBeDirectedToTheProductDetailsPage()
        {
            var productDetailsPage=new ProductDetailsPage(Settings);
            productDetailsPage.GetProductNameTitle().Should().NotBeNullOrEmpty();
        }

        [Then(@"User should see the product name ""([^""]*)""")]
        public void ThenUserShouldSeeTheProductName(string productName)
        {
            var productDetailsPage = new ProductDetailsPage(Settings);
            productDetailsPage.GetProductNameTitle().Should().Be(productName);
        }

        [Then(@"User should get the quantity and price for the product ""([^""]*)""")]
        public void ThenUserShouldGetTheQuantityAndPriceForTheProduct(string productName)
        {
            var productDetailsPage = new ProductDetailsPage(Settings);
            string quantityInCart = productDetailsPage.GetQunatityInCartPage();
            quantityInCart.Should().Be("1");
            string priceInCartPage = productDetailsPage.GetPriceInCartPage();
            Settings.Scenario["priceInCartPage"] = priceInCartPage;
            decimal priceInCart = decimal.Parse(new string(priceInCartPage.Where(c => char.IsDigit(c) || c == '.').ToArray()));
            Settings.Scenario["priceInCart"] = priceInCart;
        }

        [When(@"User adds the product to the cart")]
        public void WhenUserAddsTheProductToTheCart()
        {
            var productDetailsPage = new ProductDetailsPage(Settings);
            productDetailsPage.VerifyAddToCartButton().Should().BeTrue();
            productDetailsPage.ClickOnAddToCartButton();
        }

        [Then(@"The success message should confirm that the product ""([^""]*)"" is added to the cart")]
        public void ThenTheSuccessMessageShouldConfirmThatTheProductIsAddedToTheCart(string productName)
        {
            var productDetailsPage = new ProductDetailsPage(Settings);
            productDetailsPage.GetSuccessMessage().Should().Be($"You added {productName} to your shopping cart.");
        }

        [When(@"User navigates to the shopping cart page")]
        public void WhenUserNavigatesToTheShoppingCartPage()
        {
            var productDetailsPage = new ProductDetailsPage(Settings);
            productDetailsPage.ClickOnShoppingCartLink();
        }

        [Then(@"User should see the product ""([^""]*)"" in the shopping cart")]
        public void ThenUserShouldSeeTheProductInTheShoppingCart(string productName)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            shoppingCartPage.VerifyShoppingCartTable().Should().BeTrue();
            shoppingCartPage.VerifyProductNameInCheckOutPage(productName).Should().BeTrue();
        }

        [Then(@"validate the quantity of the product ""([^""]*)"" in the shopping cart")]
        public void ThenValidateTheQuantityOfTheProductInTheShoppingCart(string productName)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            string quantityInShoppingCartPage = shoppingCartPage.GetQuantityInShoppingCartPage(productName);
            //May be if already same product added in cart . So i am validating quantity as whether value greater than 0 or not
            quantityInShoppingCartPage.Should().NotBe("0");
            int noOfquantities = int.Parse(quantityInShoppingCartPage);
            Settings.Scenario["quantityInShoppingCartPage"] = quantityInShoppingCartPage;
            Settings.Scenario["noOfquantities"] = noOfquantities;

        }

        [Then(@"validate the product ""([^""]*)"" price in the shopping cart")]
        public void ThenValidateTheProductPriceInTheShoppingCart(string productName)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            string priceInShoppingCart = shoppingCartPage.GetPriceInShoppingCartPage(productName);
            Settings.Scenario["priceInCartPage"].Should().Be(priceInShoppingCart);
            decimal price = decimal.Parse(new string(priceInShoppingCart.Where(c => char.IsDigit(c) || c == '.').ToArray()));
            Settings.Scenario["price"] = price;
        }

        [Then(@"validate the product ""([^""]*)"" subtotal in the shopping cart")]
        public void ThenValidateTheProductSubtotalInTheShoppingCart(string productName)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            string subTotalInShoppingCartPage = shoppingCartPage.GetSubTotalInShoppingCartPage(productName);
            decimal subTotalInShoppingCart = decimal.Parse(new string(subTotalInShoppingCartPage.Where(c => char.IsDigit(c) || c == '.').ToArray()));
            decimal totalPrice = int.Parse(Settings.Scenario["quantityInShoppingCartPage"].ToString()) * decimal.Parse(Settings.Scenario["price"].ToString());
            totalPrice.Should().Be(subTotalInShoppingCart);
            Settings.Scenario["subTotalInShoppingCartPage"] = subTotalInShoppingCartPage;
            Settings.Scenario["subTotalInShoppingCart"] = subTotalInShoppingCart;
            Settings.Scenario["totalPrice"] = totalPrice;
        }

        [When(@"User updates the quantity of ""([^""]*)"" to ""([^""]*)""")]
        public void WhenUserUpdatesTheQuantityOfTo(string productName, string quantities)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            int increaseQuantity = int.Parse(quantities);
            int noOfquantities = int.Parse(Settings.Scenario["noOfquantities"].ToString());
            string quantity = (noOfquantities + increaseQuantity).ToString();
            shoppingCartPage.UpdatequnatityInCheckoutPage(productName, quantity);
        }

        [When(@"user clicks on the updateshoppingcart button")]
        public void WhenUserClicksOnTheUpdateshoppingcartButton()
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            shoppingCartPage.ClickOnUpdateShoppingCart();
        }

        [Then(@"The new quantity in the cart should be ""([^""]*)"" for the product ""([^""]*)""")]
        public void ThenTheNewQuantityInTheCartShouldBeForTheProduct(string updatedQuantity,string productName)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            int increaseQuantity = int.Parse(updatedQuantity);
            int noOfquantities = int.Parse(Settings.Scenario["noOfquantities"].ToString());
            string increasedquantityInShoppingCartPage = shoppingCartPage.GetQuantityInShoppingCartPage(productName);
            Settings.Scenario["increasedquantityInShoppingCartPage"] = increasedquantityInShoppingCartPage;
            int noOfQuantitiesAfterIncreasedquantityInShoppingCart = int.Parse(increasedquantityInShoppingCartPage);
            noOfQuantitiesAfterIncreasedquantityInShoppingCart.Should().Be((noOfquantities + increaseQuantity));
            Settings.Scenario["noOfQuantitiesAfterIncreasedquantityInShoppingCart"] = noOfQuantitiesAfterIncreasedquantityInShoppingCart;
            Settings.Scenario["increaseQuantity"] = increaseQuantity;
        }

        [Then(@"The subtotal price should reflect the updated quantity for the product ""([^""]*)""")]
        public void ThenTheSubtotalPriceShouldReflectTheUpdatedQuantityForTheProduct(string productName)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            string afterIncreasedSubTotalInShoppingCartPage = shoppingCartPage.GetSubTotalInShoppingCartPage(productName);
            decimal afterIncreasedSubTotalInShoppingCart = decimal.Parse(new string(afterIncreasedSubTotalInShoppingCartPage.Where(c => char.IsDigit(c) || c == '.').ToArray()));
            int noOfQuantitiesAfterIncreasedquantityInShoppingCart = int.Parse(Settings.Scenario["noOfQuantitiesAfterIncreasedquantityInShoppingCart"].ToString());
            decimal price= decimal.Parse(Settings.Scenario["price"].ToString());
            int increaseQuantity = int.Parse(Settings.Scenario["increaseQuantity"].ToString());
            decimal totalPriceAfterIncreasedQuantity = noOfQuantitiesAfterIncreasedquantityInShoppingCart * price ;
            decimal totalPrice=decimal.Parse(Settings.Scenario["totalPrice"].ToString());
            totalPriceAfterIncreasedQuantity.Should().Be(totalPrice + increaseQuantity * price);
            Settings.Scenario["afterIncreasedSubTotalInShoppingCartPage"] = afterIncreasedSubTotalInShoppingCartPage;
        }

        [Then(@"The order Total price should reflect the updated quantity for the product ""([^""]*)""")]
        public void ThenTheOrderTotalPriceShouldReflectTheUpdatedQuantityForTheProduct(string productName)
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            string orderTotal = shoppingCartPage.GetOrderTotal();
            decimal orderTotalValue = decimal.Parse(new string(orderTotal.Where(c => char.IsDigit(c) || c == '.').ToArray()));
            orderTotalValue.Should().NotBe(0);
            Settings.Scenario["orderTotal"] = orderTotal;
        }

        [When(@"User Clicks on proceeds to checkout")]
        public void WhenUserClicksOnProceedsToCheckout()
        {
            var shoppingCartPage = new ShoppingCartPage(Settings);
            shoppingCartPage.ClickOnProceedToCheckOut();
        }

        [Then(@"User should see the number of items in the cart in the order summary")]
        public void ThenUserShouldSeeTheNumberOfItemsInTheCartInTheOrderSummary()
        {
            var checkoutPage=new CheckoutPage(Settings);
            string increasedquantityInShoppingCartPage=Settings.Scenario["increasedquantityInShoppingCartPage"].ToString();
            increasedquantityInShoppingCartPage.Should().Be(checkoutPage.GetQuantitiesInCheckOutPage());
        }

        [When(@"User clicks on the number of items in the cart")]
        public void WhenUserClicksOnTheNumberOfItemsInTheCart()
        {
            var checkoutPage = new CheckoutPage(Settings);
            checkoutPage.ClickOnQuantitiesInCheckOutPage();
        }

        [Then(@"User should see the product ""([^""]*)"" in the order summary")]
        public void ThenUserShouldSeeTheProductInTheOrderSummary(string productName)
        {
            var checkoutPage = new CheckoutPage(Settings);
            checkoutPage.GetProductNameInCheckOutPage().Should().Be(productName);
        }

        [Then(@"validate the product ""([^""]*)"" price in the order summary")]
        public void ThenValidateTheProductPriceInTheOrderSummary(string productName)
        {
            var checkoutPage = new CheckoutPage(Settings);
            string afterIncreasedSubTotalInShoppingCartPage= Settings.Scenario["afterIncreasedSubTotalInShoppingCartPage"].ToString();
            checkoutPage.GetPriceInOrderSummary(productName).Should().Be(afterIncreasedSubTotalInShoppingCartPage);
        }

        [When(@"User chooses the shipping method ""([^""]*)""")]
        public void WhenUserChoosesTheShippingMethod(string shippingMethod)
        {
            var checkoutPage = new CheckoutPage(Settings);
            checkoutPage.ClickOnShippingMethod();
        }

        [When(@"User clicks on the next button")]
        public void WhenUserClicksOnTheNextButton()
        {
            var checkoutPage = new CheckoutPage(Settings);
            checkoutPage.ClickOnNextButton();
            checkoutPage.WaitForLoader();
        }

        [Then(@"Validate order summary details in the Review Page")]
        public void ThenValidateOrderSummaryDetailsInTheReviewPage()
        {
            var reviewPage = new ReviewAndPaymentsPage(Settings);
            reviewPage.VerifyPaymentMethodPage().Should().BeTrue();
            reviewPage.GetCartSubTotalInOrderSummary().Should().Be(Settings.Scenario["afterIncreasedSubTotalInShoppingCartPage"].ToString());
            reviewPage.GetShippingMethod().Should().Be("Best Way - Table Rate");
            reviewPage.GetShippingTotalPrice().Should().Be("$0.00");
            reviewPage.GetOrderTotal().Should().Be(Settings.Scenario["orderTotal"].ToString());
            var checkOutPage=new CheckoutPage(Settings);
            checkOutPage.GetProductNameInCheckOutPage().Should().Be(Settings.Scenario["productName"].ToString());
            string increasedquantityInShoppingCartPage = Settings.Scenario["increasedquantityInShoppingCartPage"].ToString();
            reviewPage.GetQuantityInOrderSUmmaryPage().Should().Be(increasedquantityInShoppingCartPage);
            string afterIncreasedSubTotalInShoppingCartPage = Settings.Scenario["afterIncreasedSubTotalInShoppingCartPage"].ToString();
            reviewPage.GetPriceInOrderSUmmaryPage().Should().Be(afterIncreasedSubTotalInShoppingCartPage);
 
        }

        [When(@"user clicks on place order")]
        public void WhenUserClicksOnPlaceOrder()
        {
            var reviewPage = new ReviewAndPaymentsPage(Settings);
            reviewPage.ClickOnPlaceOrder();
        }

        [Then(@"User should be able to place the order successfully")]
        public void ThenUserShouldBeAbleToPlaceTheOrderSuccessfully()
        {
            var orderConfirmationPage = new OrderConfirmationPage(Settings);
            orderConfirmationPage.GetOrderNo().Should().NotBeNullOrEmpty();
        }

    }
}
