Feature: Chapel

Background:
	Given A new game with 3 players
	And Player1 has a hand of Chapel, Estate, Estate, Copper, Copper
	When Player1 plays a Chapel

Scenario: Player must select a number of cards to trash
	Then Player1 may select up to 4 cards from their hand

Scenario: Player trashes one card
	And Player1 selects a Copper to trash
	Then There should be a Copper on top of the trash pile

Scenario: Player trashes nothing
	And Player1 selects nothing to trash
	Then Player1 should have 4 cards in hand
