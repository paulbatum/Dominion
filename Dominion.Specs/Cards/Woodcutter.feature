Feature: Woodcutter

Scenario: Play Woodcutter 
	Given A new game with 3 players	
	And Player1 has a Woodcutter in hand instead of a Copper
	When Player1 plays a Woodcutter
	Then Player1 should have 2 buys
	Then Player1 should have 2 to spend	