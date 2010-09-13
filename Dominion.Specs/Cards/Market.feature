Feature: Market

Scenario: Play Market
	Given A new game with 3 players
	And I am going first
	And I have a Market in hand instead of a Copper
	When I play Market
	Then I should have 5 cards in hand
	Then I should have 1 remaining action
	Then I should have 2 buys
	Then I should have 1 to spend
