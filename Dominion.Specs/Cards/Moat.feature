Feature: Moat
	
Scenario: Moat draws 2 cards
	Given A new game with 3 players	
	And Player1 has a Moat in hand instead of a Copper
	When Player1 plays a Moat
	Then Player1 should have 6 cards in hand

Scenario: Moat is a reaction
	Given A new game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a Moat in hand instead of a Copper
	When Player1 plays a Militia	
	Then Player2 may reveal a reaction

Scenario: Moat protects from attacks
	Given A new game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a Moat in hand instead of a Copper
	When Player1 plays a Militia
	When Player2 reveals Moat
	Then Player2 must wait
	And Player3 must select 2 cards to discard