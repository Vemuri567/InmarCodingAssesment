Feature: Product Purchase Automation

  As a shopper,
  I want to purchase a watch from the Magento website,
  So that I can complete the purchase successfully with the correct order details.

  Scenario Outline: User should be able to order Metal watches successfully
     Given  user is on the Magneto Home Page 
     Then User should see the signin button on the home page
     When User clicks on the signin button on the home page
     Then User should be redirected to the login screen
     When User enters the email "<email>" and password "<password>"
     And User clicks on the sign-in button
     Then User should see the welcome text on the home page
     When User clicks on the "<menu>" item
     And User clicks on the submenu "<submenu>" item
     Then User should see available shopping options
     When User selects the "<shopping>" category
     And User filters the "<subcategory>" 
     Then User should see the product "<productName>" in the product list
      When User clicks on the product "<productName>"
      Then User should be directed to the product details page
      And User should see the product name "<productName>"
      And User should get the quantity and price for the product "<productName>"
      When User adds the product to the cart
      Then The success message should confirm that the product "<productName>" is added to the cart
      When User navigates to the shopping cart page
      Then User should see the product "<productName>" in the shopping cart
      And  validate the quantity of the product "<productName>" in the shopping cart
      And validate the product "<productName>" price in the shopping cart 
      And validate the product "<productName>" subtotal in the shopping cart 
    When User updates the quantity of "<productName>" to "<updatedQuantity>"
    And user clicks on the updateshoppingcart button
    Then The new quantity in the cart should be "<updatedQuantity>" for the product "<productName>"
    And The subtotal price should reflect the updated quantity for the product "<productName>"
    And The order Total price should reflect the updated quantity for the product "<productName>"
     When User Clicks on proceeds to checkout
     Then User should see the number of items in the cart in the order summary
     When User clicks on the number of items in the cart
     Then User should see the product "<productName>" in the order summary
     And validate the product "<productName>" price in the order summary
      When User chooses the shipping method "Best Way - Table Rate"
      And User clicks on the next button
      Then Validate order summary details in the Review Page
      When user clicks on place order
      Then User should be able to place the order successfully

    Examples: 
    | email                     | password   | menu | submenu | shopping | subcategory | productName      | updatedQuantity |
    | naveenvemuri143@gmail.com | Naveen@987 | Gear | Watches | Material | Metal       | Didi Sport Watch | 2               |

 
