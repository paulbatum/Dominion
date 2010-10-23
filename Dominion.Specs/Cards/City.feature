Feature: City

Scenario: Play City with no empty piles
	Given A new game with 3 players	
	And Player1 has a City in hand instead of a Copper
	When Player1 plays a City	
	Then Player1 should have 2 remaining actions
	Then Player1 should have 5 cards in hand

Scenario: Play City with 1 empty pile
	Given A new game with 3 players	
	Given There are 1 empty piles
	And Player1 has a City in hand instead of a Copper
	When Player1 plays a City	
	Then Player1 should have 2 remaining actions
	Then Player1 should have 6 cards in hand

Scenario: Play City with 2 empty piles
	Given A new game with 3 players	
	Given There are 2 empty piles
	And Player1 has a City in hand instead of a Copper
	When Player1 plays a City	
	Then Player1 should have 2 remaining actions
	Then Player1 should have 6 cards in hand
	Then Player1 should have 1 to spend
	Then Player1 should have 2 buys
