Feature: CouncilRoom

Scenario: Play CouncilRoom
	Given A new game with 3 players
	And I am going first
	And I have a CouncilRoom in hand instead of a Copper
	When I play CouncilRoom
	Then I should have 2 buys
	Then Player player1 should have 8 cards in hand
	Then Player player2 should have 6 cards in hand
	Then Player player3 should have 6 cards in hand