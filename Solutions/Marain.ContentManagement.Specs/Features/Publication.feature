@setupContainer
@setupTenantedCosmosContainer
Feature: ContentPublication
	In order to manage content
	As a developer
	I want to be publish and archive content

Scenario: Publish content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | publication/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | publication/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherpublication/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherpublication/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I get the published content for Slug '{ExpectedFirst.Slug}' and call it 'Actual'
	Then the content called 'ExpectedFirst' should match the content called 'Actual'

Scenario: Publish then archive content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I get the published content for Slug '{ExpectedFirst.Slug}' and call it 'Actual'
	Then it should throw a ContentNotFoundException

Scenario: Get a publication history
	Given I have created content with a content fragment
		| Name           | Id        | Slug              | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | histpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | histpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | histpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedThird.Slug}' and id '{ExpectedThird.Id}'
	When I get the publication history for Slug '{ExpectedFirst.Slug}' with limit '20' and continuationToken '{null}' and call it 'Actual'
	Then the content summaries with state called 'Actual' should match
		| ContentName    | StateName |
		| ExpectedThird  | published |
		| ExpectedSecond | published |
		| ExpectedFirst  | published |
		| ExpectedSecond | published |
		| ExpectedFirst  | published |

Scenario: Get a publication history in batches
	Given I have created content with a content fragment
		| Name           | Id        | Slug              | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | batchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | batchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | batchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedThird.Slug}' and id '{ExpectedThird.Id}'
	When I get the publication history for Slug '{ExpectedFirst.Slug}' with limit '2' and continuationToken '{null}' and call it 'Actual1'
	When I get the publication history for Slug '{ExpectedFirst.Slug}' with limit '5' and continuationToken '{Actual1.ContinuationToken}' and call it 'Actual2'
	Then the content summaries with state called 'Actual1' should match
		| ContentName    | StateName |
		| ExpectedThird  | published |
		| ExpectedSecond | published |
	Then the content summaries with state called 'Actual2' should match
		| ContentName    | StateName |
		| ExpectedFirst  | published |
		| ExpectedSecond | published |
		| ExpectedFirst  | published |