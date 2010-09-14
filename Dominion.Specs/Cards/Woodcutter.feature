Feature: Woodcutter

Background:
	Given A new game with 3 players	
	And Player1 has a Woodcutter in hand instead of a Copper

Scenario: Woodcutter grants extra buy	
	When Player1 plays a Woodcutter
	Then Player1 should have 2 buys

Scenario: Woodcutter is +2 money	
	When Player1 plays a Woodcutter
	Then Player1 should have 2 to spend	