Feature: Duke


Scenario: Duke is worth zero points if there are no duchies in the deck
	Given A new game with 3 players	
	And Player1 has a hand of Copper, Copper, Copper, Copper, Copper
	And Player1 has a deck of Copper, Copper, Copper, Copper, Duke
	When The game is scored
	Then Player1 should have 0 victory points

Scenario: Duke is worth one point if there is one duchy in the deck
	Given A new game with 3 players	
	And Player1 has a hand of Copper, Copper, Copper, Copper, Copper
	And Player1 has a deck of Copper, Duchy, Copper, Copper, Duke
	When The game is scored
	Then Player1 should have 4 victory points

Scenario: Duke is worth five points if there are five duchies in the deck
	Given A new game with 3 players	
	And Player1 has a hand of Duchy, Duchy, Duchy, Duchy, Duchy
	And Player1 has a deck of Duke, Copper, Copper, Copper, Copper
	When The game is scored
	Then Player1 should have 20 victory points