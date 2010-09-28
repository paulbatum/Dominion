Feature: Mountebank

Scenario: Play Mountebank 
	Given A new game with 3 players	
	And Player1 has a Mountebank in hand instead of a Copper
	When Player1 plays a Mountebank
	Then Player1 should have 2 to spend
	Then Player2 should have a discard pile of Copper, Curse
	Then Player3 should have a discard pile of Copper, Curse

Scenario: Play Mountebank when a player is holding a curse
	Given A new game with 3 players	
	And Player1 has a Mountebank in hand instead of a Copper
	And Player2 has a Curse in hand instead of a Copper
	When Player1 plays a Mountebank	
	Then Player2 should have a discard pile of Curse
	Then Player3 should have a discard pile of Copper, Curse