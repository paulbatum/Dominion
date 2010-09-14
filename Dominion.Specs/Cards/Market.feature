Feature: Market

Scenario: Play Market
	Given A new game with 3 players	
	And Player1 has a Market in hand instead of a Copper
	When Player1 plays a Market
	Then Player1 should have 5 cards in hand
	Then Player1 should have 1 remaining action
	Then Player1 should have 2 buys
	Then Player1 should have 1 to spend
