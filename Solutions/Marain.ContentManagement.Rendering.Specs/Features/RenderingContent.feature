﻿@setupContainer
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

Scenario: Render markdown to HTML
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Markdown                         |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the *fragment* of text 1 |
	When I render the content called 'FirstContent' to 'FirstRendered'
	Then the output called 'FirstRendered' should match '<p>This is the <em>fragment</em> of text 1</p>\n'

Scenario: Render a liquid template to HTML
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Liquid template                                                    |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | <ul>{% for tag in content.tags %}<li>{{tag}}</li>{% endfor %}</ul> |
	When I render the content called 'FirstContent' to 'FirstRendered'
	Then the output called 'FirstRendered' should match '<ul><li>First tag</li><li>Second tag</li></ul>'

Scenario: Render a liquid template with markdown to HTML
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Liquid with markdown template                       |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | {% for tag in content.tags %}1. {{tag}}{% endfor %} |
	When I render the content called 'FirstContent' to 'FirstRendered'
	Then the output called 'FirstRendered' should match '<ol>\n<li>First tag1. Second tag</li>\n</ol>\n'

Scenario: Render published workflow content with the published state
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I have created content
		| Name            | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | ContentSlug |
		| WorkflowContent | {newguid} | /wf  | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | /           |
	And I publish the content called 'FirstContent'
	When I render the content called 'WorkflowContent' to 'FirstRendered' with the context {"PublicationStateToRender": "publishedOnly"}
	Then the output called 'FirstRendered' should match 'This is the fragment of text 1'

Scenario: Render draft workflow content with the published state
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I have created content
		| Name            | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | ContentSlug |
		| WorkflowContent | {newguid} | /wf  | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | /           |
	And I draft the content called 'FirstContent'
	When I render the content called 'WorkflowContent' to 'FirstRendered' with the context {"PublicationStateToRender": "publishedOnly"}
	Then the output called 'FirstRendered' should match ''

Scenario: Render draft workflow content with the published or draft state
	Given I have created content
		| Name         | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                       |
		| FirstContent | {newguid} | /    | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text 1 |
	And I have created content
		| Name            | Id        | Slug | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | ContentSlug |
		| WorkflowContent | {newguid} | /wf  | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | /           |
	And I draft the content called 'FirstContent'
	When I render the content called 'WorkflowContent' to 'FirstRendered' with the context {"PublicationStateToRender": "publishedOrDraft"}
	Then the output called 'FirstRendered' should match 'This is the fragment of text 1'