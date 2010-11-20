Feature: Adventurer

Scenario: Play Adventurer but reveal no treasure
	Given A new game with 3 players
	And Player1 has a hand of Adventurer, Copper, Copper, Copper, Copper
	And Player1 has a deck of Estate, Estate, Estate, Estate, Estate
	When Player1 plays a Adventurer
	Then Player1 should have a hand of Copper, Copper, Copper, Copper
	And Player1 should have 0 cards in deck
	And Player1 should have a discard pile of Estate, Estate, Estate, Estate, Estate

Scenario: Play Adventurer but reveal only one treasure
	Given A new game with 3 players
	And Player1 has a hand of Adventurer, Copper, Copper, Copper, Copper
	And Player1 has a deck of Estate, Estate, Estate, Copper, Estate
	When Player1 plays a Adventurer
	Then Player1 should have a hand of Copper, Copper, Copper, Copper, Copper
	And Player1 should have 0 cards in deck
	And Player1 should have a discard pile of Estate, Estate, Estate, Estate

Scenario: Play Adventurer and reveal two treasure
	Given A new game with 3 players
	And Player1 has a hand of Adventurer, Copper, Copper, Copper, Copper
	And Player1 has a deck of Estate, Copper, Estate, Copper, Silver
	When Player1 plays a Adventurer
	Then Player1 should have a hand of Copper, Copper, Copper, Copper, Copper, Copper
	And Player1 should have a deck of Silver
	And Player1 should have a discard pile of Estate, Estate
