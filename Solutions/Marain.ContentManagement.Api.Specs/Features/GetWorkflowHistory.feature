@perFeatureContainer
@useTransientTenant
@useContentManagementApi
@addTestContentData
@addTestContentStateData
Feature: Get workflow history

Scenario: Basic history with state retrieval by state name, without specifying a limit or continuation token
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1id'
	Then the response should have a status of '200'
	And the response should contain 20 embedded content summaries with state
	And the response should contain a 'self' link
	And the response should contain a 'next' link

Scenario: History retrieval specifying a continuation token
	Given I have requested content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1id'
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1id' with the contination token from the previous response
	Then the response should have a status of '200'
	And the response should contain another 10 embedded content summaries with state
	And the response should contain a 'self' link

Scenario: History retrieval specifying a limit
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1id' with a limit of 5 items
	Then the response should have a status of '200'
	And the response should contain 5 embedded content summaries with state
	And the response should contain a 'self' link
	And the response should contain a 'next' link

Scenario: History retrieval with fewer items than the limit doesn't include a continuation token
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1id' with a limit of 50 items
	Then the response should have a status of '200'
	And the response should contain 30 embedded content summaries with state
	And the response should contain a 'self' link
	And the response should not contain a 'next' link
