Feature: Vineyard

Scenario: Vineyard is worth zero points if there are 2 (or less) actions in the deck
	Given A new game with 3 players	
	And Player1 has a hand of Copper, Copper, Copper, Copper, Copper
	And Player1 has a deck of Copper, Copper, Smithy, Smithy, Vineyard
	When The game is scored
	Then Player1 should have 0 victory points

Scenario: Vineyard is worth one point if there are 3 actions in the deck
	Given A new game with 3 players	
	And Player1 has a hand of Copper, Copper, Copper, Copper, Copper
	And Player1 has a deck of Copper, Smithy, Smithy, Smithy, Vineyard
	When The game is scored
	Then Player1 should have 1 victory point

Scenario: Vineyard is worth two points if there are 6 actions in the deck
	Given A new game with 3 players	
	And Player1 has a hand of Copper, Copper, Copper, Smithy, Smithy
	And Player1 has a deck of Smithy, Smithy, Smithy, Smithy, Vineyard
	When The game is scored
	Then Player1 should have 2 victory points