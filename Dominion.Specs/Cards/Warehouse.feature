Feature: Warehouse

Scenario: WarehouseWorksProperly
	Given A new game with 3 players	
	And Player1 has a Warehouse in hand instead of a Copper
	When Player1 plays a Warehouse
	Then Player1 should have 4 cards in hand
	Then Player1 should have 1 remaining action
	Then Player1 should have 3 cards in discard pile
