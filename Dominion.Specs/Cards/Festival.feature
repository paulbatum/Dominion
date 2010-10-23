Feature: Festival

Scenario: Play Festival
	Given A new game with 3 players	
	And Player1 has a Festival in hand instead of a Copper
	When Player1 plays a Festival
	Then Player1 should have 2 to spend
	Then Player1 should have 2 buys
	Then Player1 should have 2 remaining action
