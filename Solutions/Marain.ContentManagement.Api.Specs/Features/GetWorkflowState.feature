@setupContainer
@perFeatureContainer
@useTransientTenant
@useContentManagementApi
Feature: Get workflow state

Scenario: Requesting an item by slug and workflow Id returns the item
	Given a content item has been created
	| Name     | Id    | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid1 | myslug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug    | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid1     | myslug1 | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the workflow state for slug '{Expected.Slug}' and workflow Id '{Expected-State.WorkflowId}'
	Then the response should have a status of '200'
	And the response body should contain content state matching 'Expected-State'
	And the response should contain a 'self' link
	And the response should contain a 'content' link
	And the response should contain a 'contentsummary' link

Scenario: Requesting an item by slug and workflow Id and with embedded Content returns the item and corresponding content
	Given a content item has been created
	| Name     | Id    | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid4 | myslug4 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug    | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid4     | myslug4 | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the workflow state with embedded 'content' for slug '{Expected.Slug}' and workflow Id '{Expected-State.WorkflowId}'
	Then the response should have a status of '200'
	And the response body should contain content state matching 'Expected-State'
	And the response should contain a 'self' link
	And the response should contain a 'content' link
	And the response should contain a 'contentsummary' link
	And the response should contain an embedded resource called 'content'
	And the embedded resource called 'content' should match the content called 'Expected'

Scenario: Requesting an item by slug and workflow Id and with embedded content summary returns the item and corresponding content summary
	Given a content item has been created
	| Name     | Id    | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid5 | myslug5 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug    | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid5     | myslug5 | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the workflow state with embedded 'contentsummary' for slug '{Expected.Slug}' and workflow Id '{Expected-State.WorkflowId}'
	Then the response should have a status of '200'
	And the response body should contain content state matching 'Expected-State'
	And the response should contain a 'self' link
	And the response should contain a 'content' link
	And the response should contain a 'contentsummary' link
	And the response should contain an embedded resource called 'contentsummary'
	And the embedded resource called 'contentsummary' should be a summary of the content called 'Expected'

Scenario: Requesting an item when the content item does not exist returns a 404 Not Found
	Given there is no content available
	When I request the workflow state for slug 'myslug/' and workflow Id 'myworkflowid'
	Then the response should have a status of '404'

Scenario: Requesting an item when the content item exists but there is no corresponding content state returns a 404 Not Found
	Given a content item has been created
	| Name     | Id    | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid2 | myslug2 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	When I request the workflow state for slug '{Expected.Slug}' and workflow Id '{newguid}'
	Then the response should have a status of '404'

Scenario: Requesting an item with a valid slug but invalid workflow Id returns a 404 Not Found
	Given a content item has been created
	| Name     | Id    | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid3 | myslug3 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug    | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid3     | myslug3 | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the workflow state for slug '{Expected.Slug}' and workflow Id '{newguid}'
	Then the response should have a status of '404'

Scenario: Requesting an item with an invalid slug and a valid workflow Id returns a 404 Not Found
	Given a content item has been created
	| Name     | Id    | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Expected | myid6 | myslug6 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | en-GB   | This is the fragment of text |
	And a workflow state has been set for the content item
	| Name           | Id        | ContentId | Slug    | WorkflowId | StateName | ChangedBy.Name | ChangedBy.Id |
	| Expected-State | {newguid} | myid6     | myslug6 | {newguid}  | retired   | Frodo Baggins  | {newguid}    |
	When I request the workflow state for slug 'myotherslug' and workflow Id '{Expected-State.WorkflowId}'
	Then the response should have a status of '404'
