Feature: Tactitian

Scenario: Play Tactician with a deck of 10 cards
	Given A new game with 2 players	
	And Player1 has a Tactician in hand instead of a Copper
	When Player1 plays a Tactician
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 should have 9 cards in hand
	And Player1 should have 2 remaining actions
	And Player1 should have 2 buys

Scenario: Play Tactician with no cards in hand
	Given A new game with 2 players	
	And Player1 has a hand of Tactician
	When Player1 plays a Tactician
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 should have 5 cards in hand
	And Player1 should have 1 remaining action
	And Player1 should have 1 buy