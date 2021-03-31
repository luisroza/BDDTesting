Feature: Order Add OrderLine to the Cart
	As a User
	I would like to add an order line to my cart
	So that I can buy it

Scenario: Add Order LIne with success to a new Order
	Given a product on display
	And there is available quantity in stock
	And user is logged in
	And there is no product added into the cart
	When the user add an unit to the cart
	Then the user will be redirected to the order summary
	And the order's total amount will add the unit price of the added order line

Scenario: Add Order Line max quantity allowed
	Given a product on display
	And there is available quantity in stock
	And user is logged in
	And the product is already added into the cart
	When the user add more items of an order line than allowed
	Then a error message will be displayd informing quantity surpasses the allowed quantity

Scenario: Add Order Line already placed in the cart
	Given a product on display
	And there is available quantity in stock
	And user is logged in
	And the product is already added into the cart
	When the user add more units to the cart
	Then the user will be redirected to the order summary
	And the order line quantity must be increased by the quantity inputed
	And the order's total amount must multiple the order line quantity by unit price