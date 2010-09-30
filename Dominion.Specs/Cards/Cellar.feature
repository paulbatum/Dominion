Feature: Cellar

Background:
	Given A new game with 3 players
	And Player1 has a hand of Cellar, Estate, Estate, Copper, Copper
	When Player1 plays a Cellar

Scenario: Player must select a number of cards to discard
	Then Player1 may select up to 4 cards from their hand

Scenario: Player discards one card to Cellar
	And Player1 selects a Copper to discard
	Then Player1 should have a Copper on top of the discard pile
	And Player1 should have 1 actions remaining

Scenario: Player discards two cards to Cellar
	And Player1 selects cards Copper, Estate to discard
	Then Player1 should have a Estate on top of the discard pile
	And Player1 should have 1 actions remaining

Scenario: Player discards nothing to Cellar
	And Player1 selects nothing to discard
	Then Player1 should have 4 cards in hand
	And Player1 should have 1 actions remaining
