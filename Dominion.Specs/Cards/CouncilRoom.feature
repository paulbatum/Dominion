Feature: Council Room

Scenario: Play CouncilRoom
	Given A new game with 3 players	
	And Player1 has a CouncilRoom in hand instead of a Copper
	When Player1 plays a CouncilRoom
	Then Player1 should have 2 buys
	And Player1 should have 8 cards in hand
	And Player2 should have 6 cards in hand
	And Player3 should have 6 cards in hand