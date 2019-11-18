Feature: GetContent
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Requesting item that does not exist returns a 404 Not Found
	Given there is no content available
	When I request the content with slug 'thisismyslug' and Id 'thisismyid'
	Then the response should have a status of '404'

Scenario: Requesting an item by slug and Id returns the item
	Given a content item has been created
	| Id   | Slug   | Title             | Description                | Author Id | Author UserName |
	| myid | myslug | Test content item | Lorem ipsum dolor sit amet | tu1       | Test User 1     |
	When I request the content with slug 'myslug' and Id 'myid'
	Then the response should have a status of '200'
	And the response body should contain the content item
	And the ETag header should be set to the content item's etag
	
Scenario: Requesting an item by slug and Id with an etag that matches the item returns a 304 Not Modified
	Given a content item has been created
	| Id   | Slug   | Title             | Description                | Author Id | Author UserName |
	| myid | myslug | Test content item | Lorem ipsum dolor sit amet | tu1       | Test User 1     |
	And I have requested the content with slug 'myslug' and Id 'myid'
	When I request the content with slug 'myslug' and Id 'myid' using the etag returned by the previous request
	Then the response should have a status of '304'
	And there should be no response body

Scenario: Requesting an item by slug and Id with an etag that does not matches the item returns the item
	Given a content item has been created
	| Id   | Slug   | Title             | Description                | Author Id | Author UserName |
	| myid | myslug | Test content item | Lorem ipsum dolor sit amet | tu1       | Test User 1     |
	When I request the content with slug 'myslug' and Id 'myid' using a random etag
	Then the response should have a status of '200'
	And the response body should contain the content item
	And the ETag header should be set to the content item's etag


