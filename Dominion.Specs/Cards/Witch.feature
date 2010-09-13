Feature: Witch

Scenario: Play Witch
	Given A new game with 3 players
	And I am going first
	And I have a Witch in hand instead of a Copper
	When I play Witch
	Then Player player1 should have 6 cards in hand
	Then Player player2 should have a Curse card on top of the discard pile
	Then Player player3 should have a Curse card on top of the discard pile