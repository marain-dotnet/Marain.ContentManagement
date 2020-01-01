@perScenarioContainer
@useTransientTenant
@useContentManagementApi
Feature: CreateContent

Scenario: Create a new content item with a new slug and Id
	Given I have a new content item
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	When I request that the content 'Expected' is created
	Then the response should have a status of '201'
	And the response body should contain the content item 'Expected'
	And the Location header should be set
	And the response should contain a 'self' link
	And the location header should match the response 'self' link
	And the ETag header should be set

Scenario: Create a new content item at an existing slug with a new Id

Scenario: Create a new content item with a new slug and an existing Id

Scenario: Creating a content item with an existing slug and Id fails
	Given I have a new content item
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	And I have requested that the content 'Expected' is created
	When I issue a second request that the content 'Expected' is created
	Then the response should have a status of '409'

Scenario: New item validation