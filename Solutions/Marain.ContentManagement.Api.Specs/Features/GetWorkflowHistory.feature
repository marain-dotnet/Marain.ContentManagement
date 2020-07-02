@setupContainer
@perFeatureContainer
@useTransientTenant
@useContentManagementApi
@addTestContentData
@addTestContentStateData
Feature: Get workflow history

Scenario: Basic history retrieval without specifying a limit or continuation token
	When I request workflow state history for slug '{Content0.Slug}' and workflow Id 'workflow1id'
	Then the response should have a status of '200'
	And the response should contain 20 content states
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link

Scenario: Basic history retrieval with embedded content summaries
	When I request workflow state history with embedded 'contentsummary' for slug '{Content0.Slug}' and workflow Id 'workflow1id'
	Then the response should have a status of '200'
	And the response should contain 20 content states
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link
	And each content state in the response should contain an embedded resource called 'contentsummary'

Scenario: History retrieval specifying a continuation token
	Given I have requested workflow state history for slug '{Content0.Slug}' and workflow Id 'workflow1id'
	When I request workflow state history for slug '{Content0.Slug}' and workflow Id 'workflow1id' with the contination token from the previous response
	Then the response should have a status of '200'
	And the response should contain another 10 content states
	And the response should contain a 'self' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link

Scenario: History retrieval specifying a limit
	When I request workflow state history for slug '{Content0.Slug}' and workflow Id 'workflow1id' with a limit of 5 items
	Then the response should have a status of '200'
	And the response should contain 5 content states
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link

Scenario: History retrieval with fewer items than the limit doesn't include a continuation token
	When I request workflow state history for slug '{Content0.Slug}' and workflow Id 'workflow1id' with a limit of 50 items
	Then the response should have a status of '200'
	And the response should contain 30 content states
	And the response should contain a 'self' link
	And the response should not contain a 'next' link
	And each content state in the response should contain a 'self' link
	And each content state in the response should contain a 'content' link
	And each content state in the response should contain a 'contentsummary' link
