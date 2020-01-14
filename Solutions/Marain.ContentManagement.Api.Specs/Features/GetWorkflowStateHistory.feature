@perFeatureContainer
@useTransientTenant
@useContentManagementApi
@addTestContentData
@addTestContentStateData
Feature: Get workflow state history

Scenario: Basic history retrieval by state name, without specifying a limit or continuation token
	When I request workflow state history for slug '{Content0.Slug}', workflow Id 'workflow1id' and state name 'published'
	Then the response should have a status of '200'
	And the response should contain 20 content states
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link

Scenario: Basic history retrieval by state name, with embedded content summaries
	When I request workflow state history with embedded 'contentsummary' for slug '{Content0.Slug}', workflow Id 'workflow1id' and state name 'published'
	Then the response should have a status of '200'
	And the response should contain 20 content states
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link
	And each content state in the response should contain an embedded resource called 'contentsummary'

Scenario: History retrieval specifying a continuation token
	Given I have requested workflow state history for slug '{Content0.Slug}', workflow Id 'workflow1id' and state name 'published'
	When I request workflow state history for slug '{Content0.Slug}', workflow Id 'workflow1id' and state name 'published' with the contination token from the previous response
	Then the response should have a status of '200'
	And the response should contain another 5 content states
	And the response should contain a 'self' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link

Scenario: History retrieval specifying a limit
	When I request workflow state history for slug '{Content0.Slug}', workflow Id 'workflow1id' and state name 'published' with a limit of 5 items
	Then the response should have a status of '200'
	And the response should contain 5 content states
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link

Scenario: History retrieval with fewer items than the limit doesn't include a continuation token
	When I request workflow state history for slug '{Content0.Slug}', workflow Id 'workflow1id' and state name 'published' with a limit of 50 items
	Then the response should have a status of '200'
	And the response should contain 25 content states
	And the response should contain a 'self' link
	And the response should not contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link
