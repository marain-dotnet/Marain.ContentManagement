﻿Feature: Get content history

Background:
	Given content items have been created
	| Name     | Id        | Slug   | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Content0 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content1 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content2 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content3 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content4 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content5 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content6 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content7 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content8 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content9 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content10 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content11 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content12 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content13 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content14 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content15 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content16 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content17 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content18 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content19 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content20 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content21 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content22 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content23 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content24 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content25 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content26 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content27 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content28 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content29 | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |

Scenario: Basic history retrieval without specifying a limit or continuation token
	When I request content history for slug '{Content0.Slug}'
	Then the response should have a status of '200'
	And the response should contain 20 embedded content summaries
	And the ETag header should be set
	And the response should contain a 'self' link
	And the response should contain a 'next' link
