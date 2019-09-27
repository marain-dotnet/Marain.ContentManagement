@setupContainer
@setupTenantedCosmosContainer
Feature: CreateContent
	In order to manage content
	As a developer
	I want to be create and retrieve content

Scenario: Create and get content
	Given I have created content with a content fragment
		| Name     | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
		| Expected | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	When I get the content with Id '{Expected.Id}' and Slug '{Expected.Slug}' and call it 'Actual'
	Then the content called 'Expected' should match the content called 'Actual'

Scenario: Create multiple content records for the same slug
	Given I have created content with a content fragment
		| Name           | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	When I get the content with Id '{ExpectedFirst.Id}' and Slug '{ExpectedFirst.Slug}' and call it 'ActualFirst'
	And I get the content with Id '{ExpectedSecond.Id}' and Slug '{ExpectedSecond.Slug}' and call it 'ActualSecond'
	And I get the content with Id '{ExpectedThird.Id}' and Slug '{ExpectedThird.Slug}' and call it 'ActualThird'
	And I get the content with Id '{ExpectedFourth.Id}' and Slug '{ExpectedFourth.Slug}' and call it 'ActualFourth'
	Then the content called 'ExpectedFirst' should match the content called 'ActualFirst'
	And the content called 'ExpectedSecond' should match the content called 'ActualSecond'
	And the content called 'ExpectedThird' should match the content called 'ActualThird'
	And the content called 'ExpectedFourth' should match the content called 'ActualFourth'

Scenario: Get content summaries for a given slug
	Given I have created content with a content fragment
		| Name           | Id        | Slug       | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | some/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | some/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | some/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | some/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	When I get the content summaries for Slug 'some/slug/' with limit '20' and continuationToken '{null}' and call it 'Actual'
	Then the content summaries called 'Actual' should match
		| ContentName    |
		| ExpectedFourth |
		| ExpectedThird  |
		| ExpectedSecond |
		| ExpectedFirst  |

Scenario: Get batched content summaries for a given slug
	Given I have created content with a content fragment
		| Name           | Id        | Slug        | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title               | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | batch/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title 1 | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | batch/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title 2 | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | batch/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title 3 | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | batch/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title 4 | A description of the content | fr-FR   | This is the fragment of text 4 |
	When I get the content summaries for Slug 'batch/slug/' with limit '2' and continuationToken '{null}' and call it 'ActualBatch1'
	And I get the content summaries for Slug 'batch/slug/' with limit '2' and continuationToken '{ActualBatch1.ContinuationToken}' and call it 'ActualBatch2'
	Then the content summaries called 'ActualBatch1' should match
		| ContentName    |
		| ExpectedFourth |
		| ExpectedThird  |
	And the content summaries called 'ActualBatch2' should match
		| ContentName    |
		| ExpectedSecond |
		| ExpectedFirst  |

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