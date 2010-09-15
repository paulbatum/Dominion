Feature: Gardens

Scenario: GardensInDeck
	Given A new game with 3 players	
	And Player1 has a Gardens in hand instead of a Copper
	Then Player1 should have 4 victory points
