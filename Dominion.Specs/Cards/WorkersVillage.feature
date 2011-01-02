Feature: Workers Village

Scenario: Workers Village is +1 card +2 actions +1 buy
	Given A new game with 3 players	
	And Player1 has a WorkersVillage in hand instead of a Copper
	When Player1 plays a WorkersVillage
	Then Player1 should have 5 cards in hand
	Then Player1 should have 2 remaining actions
	Then Player1 should have 2 buys