Feature: Familiar

Scenario: Buy Familiar
	Given A new game with 3 players	
	And Familiar is available to buy
	And Player1 has a hand of Potion, Copper, Copper, Copper, Copper
	When Player1 moves to the buy step
	And Player1 buys a Familiar		
	Then Player1 should have a Familiar on top of the discard pile	
	And Player1 should have 1 to spend

Scenario: Play Familiar
	Given A new game with 3 players	
	And Player1 has a Familiar in hand instead of a Copper
	When Player1 plays a Familiar
	Then Player1 should have 5 cards in hand
	Then Player1 should have 1 action remaining
	Then Player2 should have a Curse on top of the discard pile
	Then Player3 should have a Curse on top of the discard pile
