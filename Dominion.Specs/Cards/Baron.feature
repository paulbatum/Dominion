Feature: Baron	

Scenario: Player plays baron with no estate in hand
	Given A new game with 3 players	
	And Player1 has a hand of Baron, Copper, Copper, Copper, Copper
	When Player1 plays a Baron
	Then Player1 should have an Estate on top of the discard pile
	And Player1 should have 2 buys

Scenario: Player plays baron with an estate in hand and must choose
	Given A new game with 3 players	
	And Player1 has a hand of Baron, Estate, Copper, Copper, Copper
	When Player1 plays a Baron
	Then Player1 must choose whether to use Chancellor's effect (Yes or No)	

Scenario: Player plays baron with an estate in hand and chooses to discard it
	Given A new game with 3 players	
	And Player1 has a hand of Baron, Estate, Copper, Copper, Copper
	When Player1 plays a Baron
	When Player1 chooses to discard the estate (Yes)
	Then Player1 should have 4 to spend
	Then Player1 should have an Estate on top of the discard pile
	And Player1 should have a hand of Copper, Copper, Copper

Scenario: Player plays baron with an estate in hand and chooses not to discard
	Given A new game with 3 players	
	And Player1 has a hand of Baron, Estate, Copper, Copper, Copper
	When Player1 plays a Baron
	When Player1 chooses to keep the estate (No)
	Then Player1 should have 0 to spend
	And Player1 should have an Estate on top of the discard pile