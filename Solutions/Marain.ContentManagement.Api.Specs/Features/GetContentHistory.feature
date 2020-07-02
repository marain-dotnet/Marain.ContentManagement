@setupContainer
@perFeatureContainer
@useTransientTenant
@useContentManagementApi
@addTestContentData
Feature: Get content history

Scenario: Basic history retrieval without specifying a limit or continuation token
	When I request content history for slug '{Content0.Slug}'
	Then the response should have a status of '200'
	And the response should contain 20 content summaries
	And the ETag header should be set
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content summary in the response should contain a 'self' link
	And each content summary in the response should contain a 'content' link
	And each content summary in the response should contain a 'history' link

Scenario: History retrieval specifying a continuation token to retrieve the last page of results
	Given I have requested content history for slug '{Content0.Slug}'
	When I request content history for slug '{Content0.Slug}' with the contination token from the previous response
	Then the response should have a status of '200'
	And the response should contain another 10 content summaries
	And the ETag header should be set
	And the response should contain a 'self' link
	And each content summary in the response should contain a 'self' link
	And each content summary in the response should contain a 'content' link
	And each content summary in the response should contain a 'history' link

Scenario: History retrieval specifying a limit
	When I request content history for slug '{Content0.Slug}' with a limit of 5 items
	Then the response should have a status of '200'
	And the response should contain 5 content summaries
	And the ETag header should be set
	And the response should contain a 'self' link
	And the response should contain a 'next' link
	And each content summary in the response should contain a 'self' link
	And each content summary in the response should contain a 'content' link
	And each content summary in the response should contain a 'history' link

Scenario: History retrieval with fewer items than the limit doesn't include a continuation token
	When I request content history for slug '{Content0.Slug}' with a limit of 50 items
	Then the response should have a status of '200'
	And the response should contain 30 content summaries
	And the ETag header should be set
	And the response should contain a 'self' link
	And the response should not contain a 'next' link
	And each content summary in the response should contain a 'self' link
	And each content summary in the response should contain a 'content' link
	And each content summary in the response should contain a 'history' link

