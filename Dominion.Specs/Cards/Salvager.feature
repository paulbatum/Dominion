Feature: Salvager

Background:
	Given A new game with 3 players	
	And Player1 has a Warehouse in hand instead of a Copper
	When Player1 plays a Salvager

Scenario: Salvager trashes a card
	Then Player1 must select 1 card to trash

Scenario: Salvager trashing estate gives plus 2 spend
	And Player1 selects a Estate to trash
	Then Player1 should have 2 to spend

Scenario: Salvager gives plus one buy
	Then Player1 should have 2 buys
