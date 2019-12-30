Feature: Get workflow history

Background:
	Given content items have been created
	| Name      | Id        | Slug  | Tags                  | CategoryPaths                               | Author.Name   | Author.Id | Title             | Description                  | Culture | Fragment                     |
	| Content0  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content1  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content2  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content3  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content4  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content5  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content6  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content7  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content8  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
	| Content9  | {newguid} | slug1 | First tag; Second tag | /standard/content;/books/hobbit;/books/lotr | Bilbo Baggins | {newguid} | This is the title | A description of the content | fr-FR   | This is the fragment of text |
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
	And workflow states have been set for the content items
	| Name            | Id        | ContentId      | Slug  | WorkflowId  | StateName | ChangedBy.Name | ChangedBy.Id |
	| Content0-State  | {newguid} | {Content0.Id}  | slug1 | workflow1Id | draft     | Frodo Baggins  | {newguid}    |
	| Content1-State  | {newguid} | {Content1.Id}  | slug1 | workflow1Id | draft     | Frodo Baggins  | {newguid}    |
	| Content2-State  | {newguid} | {Content2.Id}  | slug1 | workflow1Id | draft     | Frodo Baggins  | {newguid}    |
	| Content3-State  | {newguid} | {Content3.Id}  | slug1 | workflow1Id | draft     | Frodo Baggins  | {newguid}    |
	| Content4-State  | {newguid} | {Content4.Id}  | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content5-State  | {newguid} | {Content5.Id}  | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content6-State  | {newguid} | {Content6.Id}  | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content7-State  | {newguid} | {Content7.Id}  | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content8-State  | {newguid} | {Content8.Id}  | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content9-State  | {newguid} | {Content9.Id}  | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content10-State | {newguid} | {Content10.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content11-State | {newguid} | {Content11.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content12-State | {newguid} | {Content12.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content13-State | {newguid} | {Content13.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content14-State | {newguid} | {Content14.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content15-State | {newguid} | {Content15.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content16-State | {newguid} | {Content16.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content17-State | {newguid} | {Content17.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content18-State | {newguid} | {Content18.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content19-State | {newguid} | {Content19.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content20-State | {newguid} | {Content20.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content21-State | {newguid} | {Content21.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content22-State | {newguid} | {Content22.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content23-State | {newguid} | {Content23.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content24-State | {newguid} | {Content24.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content25-State | {newguid} | {Content25.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content26-State | {newguid} | {Content26.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content27-State | {newguid} | {Content27.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content28-State | {newguid} | {Content28.Id} | slug1 | workflow1Id | published | Frodo Baggins  | {newguid}    |
	| Content29-State | {newguid} | {Content29.Id} | slug1 | workflow1Id | archived  | Frodo Baggins  | {newguid}    |

Scenario: Basic history with state retrieval by state name, without specifying a limit or continuation token
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1Id'
	Then the response should have a status of '200'
	And the response should contain 20 embedded content summaries with state
	And the response should contain a 'self' link
	And the response should contain a 'next' link

Scenario: History retrieval specifying a continuation token
	Given I have requested content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1Id'
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1Id' with the contination token from the previous response
	Then the response should have a status of '200'
	And the response should contain another 5 embedded content summaries with state
	And the response should contain a 'self' link

Scenario: History retrieval specifying a limit
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1Id' with a limit of 5 items
	Then the response should have a status of '200'
	And the response should contain 10 embedded content summaries with state
	And the response should contain a 'self' link
	And the response should contain a 'next' link

Scenario: History retrieval with fewer items than the limit doesn't include a continuation token
	When I request content history with state for slug '{Content0.Slug}' and workflow Id 'workflow1Id' with a limit of 50 items
	Then the response should have a status of '200'
	And the response should contain 30 embedded content summaries with state
	And the response should contain a 'self' link
	And the response should not contain a 'next' link
