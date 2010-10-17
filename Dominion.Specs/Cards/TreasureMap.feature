Feature: Treasure Map

Scenario: Playing treasure map with another treasure map in hand gets the gold
	Given A new game with 3 players	
	And Player1 has a hand of TreasureMap, TreasureMap, Copper, Copper, Estate
	And Player1 has a deck of Estate, Copper, Copper, Copper, Estate
	When Player1 plays a TreasureMap
	Then The trash pile should be TreasureMap, TreasureMap	
	And Player1 should have a deck of: Gold, Gold, Gold, Gold, Estate, Copper, Copper, Copper, Estate	

Scenario: Playing treasure map without another one in hand just trashes it
	Given A new game with 3 players	
	And Player1 has a hand of TreasureMap, Copper, Copper, Copper, Estate
	And Player1 has a deck of Estate, Copper, Copper, Copper, Estate
	When Player1 plays a TreasureMap
	Then The trash pile should be TreasureMap
	And Player1 should have a deck of: Estate, Copper, Copper, Copper, Estate	
