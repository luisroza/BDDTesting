Feature: User - Register New User
	As a visitor
	I want to register me as an user
	So that I can buy goods in the store

Scenario: Register a user with sucess
	Given a vistor is accessing the WebStore
	When user clicks on Register
	And fills correctly the form data
	| Data             |
	| E-mail           |
	| Password         |
	| Confirm Password |
	And clicks on register button
	Then user will be redirected to the home page
	And a greeting with the user's e-mail should be displayed on the top menu 

Scenario: User password without Uppercase
    Given a vistor is accessing the WebStore
	When user clicks on Register
	And fills the form data with an password with no Uppercase
	| Data             |
	| E-mail           |
	| Password         |
	| Confirm Password |
	And clicks on register button
	Then user will receive an error message informing that the password must have at least one Uppercase

Scenario: User password without special character
    Given a vistor is accessing the WebStore
	When user clicks on Register
	And fills the form data with an password with no special character
	| Data             |
	| E-mail           |
	| Password         |
	| Confirm Password |
	And clicks on register button
	Then user will receive an error message informing that the password must have at least one special character