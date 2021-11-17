Feature: IctSearchTest


Scenario: Search Then Sort By Date
	Given I navigate to 'ICT'
	When I select region 'United States' and language 'English'
	When I search for 'IFRS 17'
	Then I validate results page
	Then I validate sort by Date
	When I filter Content Type by 'Article'
	Then I validate article link starts with 'https://www.willistowerswatson.com/en-US/'