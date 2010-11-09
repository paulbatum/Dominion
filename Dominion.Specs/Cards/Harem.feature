Feature: Harem

Scenario: Harem is worth 2 victory points
	Given A new game with 3 players	
	And Player1 has a Harem in hand instead of a Copper
	When The game is scored
	Then Player1 should have 5 victory points

Scenario: Harem is worth 2 spend
	Given A new game with 3 players	
	And Player1 has a hand of Copper, Copper, Estate, Estate, Harem
	When Player1 moves to the buy step	
	Then Player1 should have 4 to spend
