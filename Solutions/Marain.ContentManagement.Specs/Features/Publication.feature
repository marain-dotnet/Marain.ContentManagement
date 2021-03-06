﻿@setupContainer
@setupTenantedCosmosContainer
Feature: ContentPublication
	In order to manage content
	As a developer
	I want to be publish and archive content

Scenario: Get state for non-existent content
	Then getting the content for the publication workflow for Slug '{newguid}/' should throw a ContentNotFoundException
	Then getting the publication state for Slug '{newguid}/' should throw a ContentNotFoundException

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
		| Name          | Id        | Slug                  | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst | {newguid} | movepublication/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I move the content from Slug '{ExpectedFirst.Slug}' to 'movepublication/anotherslug/' and call it 'Moved'
	And I get the content publication state for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchivedState'
	And I get the content for the content state called 'ActualArchivedState' and call it 'ActualArchivedContent'
	And I get the content publication state for Slug 'movepublication/anotherslug/' and call it 'ActualState'
	And I get the content for the content state called 'ActualState' and call it 'ActualContent'
	Then the content called 'Moved' should match the content called 'ActualContent'
	And the content called 'ExpectedFirst' should be copied to the content called 'ActualContent'
	And the content state called 'ActualArchivedState' should be in the state 'archived'
	And the content state called 'ActualState' should be in the state 'published'

Scenario: Move archived content
	Given I have created content with a content fragment
		| Name          | Id        | Slug               | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst | {newguid} | movearchived/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	When I move the content from Slug '{ExpectedFirst.Slug}' to 'movearchived/anotherslug/' and call it 'Moved'
	And I get the content publication state for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchivedState'
	And I get the content for the content state called 'ActualArchivedState' and call it 'ActualArchivedContent'
	And I get the content publication state for Slug 'movearchived/anotherslug/' and call it 'ActualState'
	And I get the content for the content state called 'ActualState' and call it 'ActualContent'
	Then the content called 'Moved' should match the content called 'ActualContent'
	And the content called 'ExpectedFirst' should be copied to the content called 'ActualContent'
	And the content called 'ExpectedFirst' should match the content called 'ActualArchivedContent'
	And the content state called 'ActualArchivedState' should be in the state 'archived'
	And the content state called 'ActualState' should be in the state 'archived'

Scenario: Copy published content
	Given I have created content with a content fragment
		| Name          | Id        | Slug                  | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst | {newguid} | copypublication/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I copy the content from Slug '{ExpectedFirst.Slug}' to 'copypublication/anotherslug/' and call it 'Moved'
	And I get the content publication state for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchivedState'
	And I get the content for the content state called 'ActualArchivedState' and call it 'ActualArchivedContent'
	And I get the content publication state for Slug 'copypublication/anotherslug/' and call it 'ActualState'
	And I get the content for the content state called 'ActualState' and call it 'ActualContent'
	Then the content called 'Moved' should match the content called 'ActualContent'
	And the content called 'ExpectedFirst' should be copied to the content called 'ActualContent'
	And the content state called 'ActualArchivedState' should be in the state 'published'
	And the content state called 'ActualState' should be in the state 'draft'

Scenario: Copy archived content
	Given I have created content with a content fragment
		| Name          | Id        | Slug               | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst | {newguid} | copyarchived/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	When I copy the content from Slug '{ExpectedFirst.Slug}' to 'copyarchived/anotherslug/' and call it 'Moved'
	And I get the content publication state for Slug '{ExpectedFirst.Slug}' and call it 'ActualArchivedState'
	And I get the content for the content state called 'ActualArchivedState' and call it 'ActualArchivedContent'
	And I get the content publication state for Slug 'copyarchived/anotherslug/' and call it 'ActualState'
	And I get the content for the content state called 'ActualState' and call it 'ActualContent'
	Then the content called 'Moved' should match the content called 'ActualContent'
	And the content called 'ExpectedFirst' should be copied to the content called 'ActualContent'
	And the content called 'ExpectedFirst' should match the content called 'ActualArchivedContent'
	And the content state called 'ActualArchivedState' should be in the state 'archived'
	And the content state called 'ActualState' should be in the state 'draft'

Scenario: Publish then archive content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                 | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I archive the content with Slug '{ExpectedFirst.Slug}'
	Then getting the published content for Slug '{ExpectedFirst.Slug}' throws a ContentNotFoundException

Scenario: Publish then draft content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                 | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I draft the content with Slug '{ExpectedFirst.Slug}'
	When I get the content publication state for Slug '{ExpectedFirst.Slug}' and call it 'Actual'
	Then the content state called 'Actual' should be in the state 'draft'
	And getting the published content for Slug '{ExpectedFirst.Slug}' throws a ContentNotFoundException


Scenario: Archive then archive content
	Given I have created content with a content fragment
		| Name           | Id        | Slug                 | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | archpub/slug/        | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | anotherarchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	When I archive the content with Slug '{ExpectedFirst.Slug}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	Then getting the published content for Slug '{ExpectedFirst.Slug}' throws a ContentNotFoundException

Scenario: Get a published history
	Given I have created content with a content fragment
		| Name           | Id        | Slug          | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | histpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | histpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | histpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedThird.Slug}' and id '{ExpectedThird.Id}'
	When I get the published state history and corresponding content summaries for Slug '{ExpectedFirst.Slug}' with limit '20' and continuationToken '{null}' and call them 'Actual'
	Then the publication state histories and corresponding content summaries called 'Actual' should match
		| ContentName    | StateName |
		| ExpectedThird  | published |
		| ExpectedSecond | published |
		| ExpectedFirst  | published |
		| ExpectedSecond | published |
		| ExpectedFirst  | published |

Scenario: Get a publication history
	Given I have created content with a content fragment
		| Name           | Id        | Slug          | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | histpublication/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | histpublication/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | histpublication/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I draft the content with Slug '{ExpectedFirst.Slug}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I draft the content with Slug '{ExpectedFirst.Slug}'
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I archive the content with Slug '{ExpectedFirst.Slug}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedThird.Slug}' and id '{ExpectedThird.Id}'
	When I get the publication history and corresponding content summaries for Slug '{ExpectedFirst.Slug}' with limit '20' and call it 'Actual'
	Then the publication state histories and corresponding content summaries called 'Actual' should match
		| ContentName    | StateName |
		| ExpectedThird  | published |
		| ExpectedSecond | published |
		| ExpectedFirst  | archived  |
		| ExpectedFirst  | published |
		| ExpectedSecond | draft     |
		| ExpectedSecond | published |
		| ExpectedFirst  | draft     |
		| ExpectedFirst  | published |

Scenario: Get a publication history in batches
	Given I have created content with a content fragment
		| Name           | Id        | Slug           | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | batchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | batchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | batchpub/slug/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedFirst.Slug}' and id '{ExpectedFirst.Id}'
	And I publish the content with Slug '{ExpectedSecond.Slug}' and id '{ExpectedSecond.Id}'
	And I publish the content with Slug '{ExpectedThird.Slug}' and id '{ExpectedThird.Id}'
	When I get the publication history and corresponding content summaries for Slug '{ExpectedFirst.Slug}' with limit '2' and call it 'Actual1'
	When I get the publication history and corresponding content summaries for Slug '{ExpectedFirst.Slug}' with limit '5' and continuationToken from previous results called 'Actual1' and call it 'Actual2'
	Then the publication state histories and corresponding content summaries called 'Actual1' should match
		| ContentName    | StateName |
		| ExpectedThird  | published |
		| ExpectedSecond | published |
	Then the publication state histories and corresponding content summaries called 'Actual2' should match
		| ContentName    | StateName |
		| ExpectedFirst  | published |
		| ExpectedSecond | published |
		| ExpectedFirst  | published |