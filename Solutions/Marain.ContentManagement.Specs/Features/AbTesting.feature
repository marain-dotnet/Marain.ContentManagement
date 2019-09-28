@setupContainer
@setupTenantedCosmosContainer
Feature: AbTesting
	In order to manage AB test scenarios
	As a developer
	I want to be able to aggregate content instances into an AB test group

Scenario: Create draft content and aggregate it into an AB test group
	Given I have created content with a content fragment
		| Name           | Id        | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| ExpectedFirst  | {newguid} | abtest/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
		| ExpectedSecond | {newguid} | abtest/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 2 |
		| ExpectedThird  | {newguid} | abtest/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 3 |
		| ExpectedFourth | {newguid} | abtest/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 4 |
	And I have created an AbTest set called 'ExpectedAbTest' with the content
		| ContentName    | Key    |
		| ExpectedFirst  | Group1 |
		| ExpectedSecond | Group2 |
		| ExpectedThird  | Group3 |
		| ExpectedFourth | Group4 |
	And I have created content with an AbTest set
		| Name                  | Id        | Slug    | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | AbTestSetName  |
		| ExpectedAbTestContent | {newguid} | abtest/ | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | ExpectedAbTest |
	When I get the content with Id '{ExpectedAbTestContent.Id}' and Slug '{ExpectedAbTestContent.Slug}' and call it 'Actual'
	And I get the ABTest content called 'Group1' from the content called 'Actual' and call it 'ActualFirst'
	And I get the ABTest content called 'Group2' from the content called 'Actual' and call it 'ActualSecond'
	And I get the ABTest content called 'Group3' from the content called 'Actual' and call it 'ActualThird'
	And I get the ABTest content called 'Group4' from the content called 'Actual' and call it 'ActualFourth'
	Then the content called 'ExpectedFirst' should match the content called 'ActualFirst'
	Then the content called 'ExpectedSecond' should match the content called 'ActualSecond'
	Then the content called 'ExpectedThird' should match the content called 'ActualThird'
	Then the content called 'ExpectedFourth' should match the content called 'ActualFourth'
	And getting the ABTest content called 'Group5' from the content called 'Actual' should throw a ContentNotFoundException.