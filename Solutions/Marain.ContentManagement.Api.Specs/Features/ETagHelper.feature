Feature: ETagHelper
	In order to generate etags
	As a developer
	I want to use a consistent method of building and verifying tags

Scenario: Generating an etag from a discriminator and single etag always returns the same result
	Given I build an etag from discriminator 'Content' and etag '"thisismyetag"'
	And I build an etag from discriminator 'Content' and etag '"thisismyetag"'
	Then the generated etags are the same

Scenario: Generating etags from the same etag with different discriminators returns different results
	Given I build an etag from discriminator 'Content' and etag '"thisismyetag"'
	And I build an etag from discriminator 'ContentSummary' and etag '"thisismyetag"'
	Then the generated etags are distinct

Scenario: Generating an etag from a discriminator and list of etags always returns the same result
	Given I build an etag from discriminator 'Content' and a list of etags
	| ETag  |
	| etag1 |
	| etag2 |
	| etag3 |
	| etag4 |
	| etag5 |
	And I build an etag from discriminator 'Content' and a list of etags
	| ETag  |
	| etag1 |
	| etag2 |
	| etag3 |
	| etag4 |
	| etag5 |
	Then the generated etags are the same

Scenario: Generating an etag from a discriminator and lists containing the same items in different orders returns different results
	Given I build an etag from discriminator 'Content' and a list of etags
	| ETag  |
	| etag1 |
	| etag2 |
	| etag3 |
	| etag4 |
	| etag5 |
	And I build an etag from discriminator 'Content' and a list of etags
	| ETag  |
	| etag2 |
	| etag3 |
	| etag4 |
	| etag5 |
	| etag1 |
	Then the generated etags are distinct

Scenario: Generating an etag from a discriminator and lists containing different items returns different results
	Given I build an etag from discriminator 'Content' and a list of etags
	| ETag  |
	| etag1 |
	| etag2 |
	| etag3 |
	| etag4 |
	| etag5 |
	And I build an etag from discriminator 'Content' and a list of etags
	| ETag  |
	| etag10 |
	| etag11 |
	Then the generated etags are distinct

