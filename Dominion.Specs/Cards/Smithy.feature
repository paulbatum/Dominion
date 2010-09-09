Feature: Smithy

Scenario: Play Smithy
	Given A new game with 3 players
	And I am going first
	And I have a Smithy in hand instead of a Copper
	When I play Smithy
	Then I should have 7 cards in hand