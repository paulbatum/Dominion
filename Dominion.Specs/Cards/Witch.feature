Feature: Witch

Scenario: Play Witch
	Given A new game with 3 players	
	And Player1 has a Witch in hand instead of a Copper
	When Player1 plays a Witch
	Then Player1 should have 6 cards in hand
	Then Player2 should have a Curse on top of the discard pile
	Then Player3 should have a Curse on top of the discard pile

@Ignore
Scenario: Play Witch with only 1 Curse left
	Given A new game with 3 players
	But There is only 1 Curse left
	And Player1 has a Witch in hand instead of a Copper
	When Player1 plays a Witch
	Then Player1 should have 6 cards in hand
	Then Player2 should have a Curse on top of the discard pile
	Then Player3 should have 0 cards in the discard pile