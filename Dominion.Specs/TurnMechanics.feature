Feature: Turn Mechanics
	In order to have my turn
	As a dominion player
	I can perform certain actions

Background: 
	Given A new game with 3 players
	And I am going first

Scenario: Discard hand at end of turn	
	When I end my turn
	Then I should have 5 cards in the discard pile

Scenario: Draw cards after discarding at end of turn
	When I end my turn
	Then I should have 5 cards in hand

Scenario: Use single action
	Given I have a Smithy in hand instead of a Copper
	When I play Smithy
	Then I should have 0 actions remaining
	And Smithy should be in play
	
