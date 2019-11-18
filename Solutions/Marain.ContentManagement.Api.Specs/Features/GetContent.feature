Feature: GetContent
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Requesting item that does not exist returns a 404
	Given there is no content available
	When I request the content with slug 'thisismyslug' and Id 'thisismyid'
	Then the response should have a status of '404'

Scenario: Requesting an item by slug and Id returns the item
	Given a content item has been created
	| Id   | ETag | Slug   | Title             | Description                |
	| myid |      | myslug | Test content item | Lorem ipsum dolor sit amet |
	When I request the content with slug 'myslug' and Id 'myid'
	Then the response should have a status of '200'
	And the response body should contain the content item
	