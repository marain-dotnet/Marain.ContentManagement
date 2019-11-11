@setupContainer
Feature: RenderingContent
	In order to render content
	As a developer
	I want to be able to render content using a configurable rendering pipeline

Scenario: Render a content fragment to HTML
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	When I render the content called 'FirstContent' to 'FirstRendered'
	Then the output called 'FirstRendered' should match 'This is the fragment of text 1'

Scenario: Render a markdown to HTML
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Markdown                       |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the *fragment* of text 1 |
	When I render the content called 'FirstContent' to 'FirstRendered'
	Then the output called 'FirstRendered' should match '<p>This is the <em>fragment</em> of text 1</p>\n'
