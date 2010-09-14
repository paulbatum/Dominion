Feature: Turn Mechanics
	In order to have my turn
	As a dominion player
	I can perform certain actions

Background: 
	Given A new game with 3 players	

Scenario: Discard hand at end of turn	
	When Player1 ends their turn
	Then Player1 should have 5 cards in the discard pile

Scenario: Draw cards after discarding at end of turn
	When Player1 ends their turn
	Then Player1 should have 5 cards in hand

Scenario: Use single action
	Given Player1 has a Smithy in hand instead of a Copper
	When Player1 plays a Smithy
	Then Player1 should have 0 actions remaining
	And Smithy should be in play
	
