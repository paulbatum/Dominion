Feature: Mountebank

Scenario: Player plays Mountebank when opponents have no curses in hand
	Given A new game with 3 players	
	And Player1 has a Mountebank in hand instead of a Copper
	When Player1 plays a Mountebank
	Then Player1 should have 2 to spend
	Then Player2 should have a discard pile of Copper, Curse
	Then Player3 should have a discard pile of Copper, Curse

Scenario: Player plays Mountebank, opponent must choose whether to discard a curse 
	Given A new game with 3 players	
	And Player1 has a Mountebank in hand instead of a Copper
	And Player2 has a Curse in hand instead of a Copper
	When Player1 plays a Mountebank	
	Then Player2 must choose whether to discard a Curse (Yes or No)
	Then Player3 must wait

Scenario: Play Mountebank when an opponent is holding a curse
	Given A new game with 3 players	
	And Player1 has a Mountebank in hand instead of a Copper
	And Player2 has a Curse in hand instead of a Copper
	When Player1 plays a Mountebank	
	When Player2 chooses to discard a Curse (Yes)
	Then Player2 should have a discard pile of Curse
	Then Player3 should have a discard pile of Copper, Curse