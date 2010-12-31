Feature: Wharf

Scenario: Player plays Wharf and draws 2 cards and gains a buy
	Given A new game with 2 players	
	And Player1 has a Wharf in hand instead of a Copper
	When Player1 plays a Wharf
	Then Player1 should have 6 cards in hand
	And Player1 should have 2 buys

Scenario: Player plays Wharf and draws 2 cards and gains a buy on the following turn
Given A new game with 2 players	
	And Player1 has a Wharf in hand instead of a Copper
	When Player1 plays a Wharf
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 should have 7 cards in hand
	And Player1 should have 2 buys	