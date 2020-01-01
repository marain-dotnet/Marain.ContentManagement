@useTransientTenant
@useContentManagementApi
Feature: GetContentWithState

Scenario: Requesting an item by slug and workflow Id returns the item
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug   | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid      | myslug | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the content with its state for slug '{Expected.Slug}' and workflow Id '{Expected-State.WorkflowId}'
	Then the response should have a status of '200'
	And the response body should contain content and state matching content 'Expected' and state 'Expected-State'

Scenario: Requesting an item when the content item does not exist returns a 404 Not Found
	Given there is no content available
	When I request the content with its state for slug 'myslug/' and workflow Id 'myworkflowid'
	Then the response should have a status of '404'

Scenario: Requesting an item when the content item exists but there is no corresponding content state returns a 404 Not Found
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	When I request the content with its state for slug '{Expected.Slug}' and workflow Id '{newguid}'
	Then the response should have a status of '404'

Scenario: Requesting an item with a valid slug but invalid workflow Id returns a 404 Not Found
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug   | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid      | myslug | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the content with its state for slug '{Expected.Slug}' and workflow Id '{newguid}'
	Then the response should have a status of '404'

Scenario: Requesting an item with an invalid slug and a valid workflow Id returns a 404 Not Found
	Given a content item has been created
	| Name     | Id     | Slug              | Tags                       | CategoryPaths                               | Author.Name     | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid   | myslug            | First tag; Second tag      | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins   | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug   | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid      | myslug | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the content with its state for slug 'myotherslug' and workflow Id '{Expected-State.WorkflowId}'
	Then the response should have a status of '404'
