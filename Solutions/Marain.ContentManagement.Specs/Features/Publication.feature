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

Scenario: Move published content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | movepublication/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I move the content from Slug '{ExpectedFirst.Slug}' to 'movepublication/anotherslug/' and call it 'Moved'
	And I get the content for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchived'
	And I get the content for Slug 'movepublication/anotherslug/' and call it 'Actual'
	Then the content called 'Moved' should match the content with state called 'Actual'
	And the content called 'ExpectedFirst' should be copied to the content with state called 'Actual'
	And the content called 'ActualArchived' should be in the state 'archived'
	And the content called 'Actual' should be in the state 'published'

Scenario: Move archived content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | movearchived/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	When I move the content from Slug '{ExpectedFirst.Slug}' to 'movearchived/anotherslug/' and call it 'Moved'
	And I get the content for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchived'
	And I get the content for Slug 'movearchived/anotherslug/' and call it 'Actual'
	Then the content called 'Moved' should match the content with state called 'Actual'
	And the content called 'ExpectedFirst' should be copied to the content with state called 'Actual' 
	And the content called 'ExpectedFirst' should match the content with state called 'ActualArchived'
	And the content called 'ActualArchived' should be in the state 'archived'
	And the content called 'Actual' should be in the state 'archived'

Scenario: Copy published content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | copypublication/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I copy the content from Slug '{ExpectedFirst.Slug}' to 'copypublication/anotherslug/' and call it 'Moved'
	And I get the content for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchived'
	And I get the content for Slug 'copypublication/anotherslug/' and call it 'Actual'
	Then the content called 'Moved' should match the content with state called 'Actual'
	And the content called 'ExpectedFirst' should be copied to the content with state called 'Actual'
	And the content called 'ActualArchived' should be in the state 'published'
	And the content called 'Actual' should be in the state 'draft'

Scenario: Copy archived content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | copyarchived/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	When I copy the content from Slug '{ExpectedFirst.Slug}' to 'copyarchived/anotherslug/' and call it 'Moved'
	And I get the content for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchived'
	And I get the content for Slug 'copyarchived/anotherslug/' and call it 'Actual'
	Then the content called 'Moved' should match the content with state called 'Actual'
	And the content called 'ExpectedFirst' should be copied to the content with state called 'Actual' 
	And the content called 'ExpectedFirst' should match the content with state called 'ActualArchived'
	And the content called 'ActualArchived' should be in the state 'archived'
	And the content called 'Actual' should be in the state 'draft'


Scenario: Publish then archive content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	When I get the published content for Slug '{ExpectedFirst.Slug}' and call it 'Actual'
	Then it should throw a ContentNotFoundException

Scenario: Publish then draft content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I draft the content with Slug '{ExpectedFirst.Slug}'
	When I get the published content for Slug '{ExpectedFirst.Slug}' and call it 'Actual'
	Then it should throw a ContentNotFoundException

Scenario: Archive then archive content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                     | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
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