Feature: GetContent

Scenario: Requesting an item by slug and Id returns the item
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	When I request the content with slug '{Expected.Slug}' and Id '{Expected.Id}'
	Then the response should have a status of '200'
	And the response body should contain the content item 'Expected'
	And the ETag header should be set
	And the Cache header should be set to 'max-age=31536000'
	
Scenario: Requesting an item by slug and Id with an etag that matches the item returns a 304 Not Modified
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	And I have requested the content with slug '{Expected.Slug}' and Id '{Expected.Id}'
	When I request the content with slug '{Expected.Slug}' and Id '{Expected.Id}' using the etag returned by the previous request
	Then the response should have a status of '304'
	And there should be no response body

Scenario: Requesting an item by slug and Id with an etag that does not matches the item returns the item
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	When I request the content with slug '{Expected.Slug}' and Id '{Expected.Id}' using a random etag
	Then the response should have a status of '200'
	And the response body should contain the content item 'Expected'
	And the ETag header should be set
	And the Cache header should be set to 'max-age=31536000'

Scenario: Requesting item that does not exist returns a 404 Not Found
	Given there is no content available
	When I request the content with slug 'thisismyslug' and Id 'thisismyid'
	Then the response should have a status of '404'

Scenario: Requesting item with a valid slug but invalid Id returns a 404 Not Found
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	When I request the content with slug '{Expected.Slug}' and Id 'myotherid'
	Then the response should have a status of '404'

Scenario: Requesting item with a valid Id but valid slug returns a 404 Not Found
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	When I request the content with slug 'myotherslug' and Id '{Expected.Id}'
	Then the response should have a status of '404'