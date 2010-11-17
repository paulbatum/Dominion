Feature: Warehouse

Scenario: Player must decide what cards to discard
	Given A new game with 3 players	
	And Player1 has a Warehouse in hand instead of a Copper
	When Player1 plays a Warehouse
	Then Player1 should have 7 cards in hand
	And Player1 should have 1 remaining action
	And Player1 must select 3 cards to discard

Scenario: Player discards 3 cards
	Given A new game with 3 players	
	And Player1 has a Warehouse in hand instead of a Copper
	When Player1 plays a Warehouse
	And Player1 selects 3 Copper to discard
	Then Player1 should have 4 cards in hand
	And Player1 should have 1 remaining action
	And Player1 should have 3 cards in the discard pile

Scenario: Play warehouse with a total of two cards in hand, deck and discard pile
	Given A new game with 3 players	
	And Player1 has a hand of Warehouse, Copper
	And Player1 has an empty deck
	When Player1 plays a Warehouse
	Then Player1 should have 0 cards in hand
	And Player1 should have 1 remaining action
	And Player1 should have 1 card in the discard pile