Feature: Turn Mechanics
	In order to have my turn
	As a dominion player
	I can perform certain actions

Background: 
	Given A new game with 3 players
	And It is my turn

Scenario: Discard hand at end of turn	
	When I end my turn
	Then I should have 5 cards in the discard pile

Scenario: Draw cards after discarding at end of turn
	When I end my turn
	Then I should have 5 cards in hand
	
