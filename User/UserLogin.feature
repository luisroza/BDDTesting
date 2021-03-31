Feature: User - Login
	As a user
	I want to logging on the store
	So that I will be able to access further functionalities

Scenario: Login with success
	Given a vistor is accessing the WebStore
	When user clicks on Login
	And fills the login data form 
	| Data     |
	| E-mail   |
	| Password |
	And clicks on login button
	Then user will be redirected to the home page
	And a greeting message with the user's e-mail should be displayed on top menu