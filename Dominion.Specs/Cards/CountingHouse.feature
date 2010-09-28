Feature: Counting House


Scenario: Counting House does nothing when there are no coppers in the discard pile
	Given A new game with 1 players	
	And Player1 has a CountingHouse in hand instead of a Copper
	When Player1 plays a CountingHouse
	Then Player1 should have 4 cards in hand

Scenario: Counting House retrieves coppers
	Given A new game with 3 players	
	And Player1 has a hand of CountingHouse, Estate, Estate, Copper, Copper
	And Player1 has a Copper in the discard pile
	And Player1 has a Copper in the discard pile
	When Player1 plays a CountingHouse
	Then Player1 should have a hand of Estate, Estate, Copper, Copper, Copper, Copper
	And Player1 should have 0 cards in the discard pile


