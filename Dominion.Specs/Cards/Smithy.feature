Feature: Smithy

Scenario: Play Smithy
	Given A new game with 3 players	
	And Player1 has a Smithy in hand instead of a Copper
	When Player1 plays a Smithy
	Then Player1 should have 7 cards in hand