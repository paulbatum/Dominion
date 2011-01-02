Feature: Kings Court

Scenario: Player must choose which card to use Kings Court effect on
	Given A new game with 3 players 
	And Player1 has a KingsCourt in hand instead of a Copper		
	And Player1 has a Market in hand instead of a Copper	
	When Player1 plays a KingsCourt
	Then Player1 must select 1 action card

Scenario: Player uses Kings Court's effect on a Market
	Given A new game with 3 players 
	And Player1 has a KingsCourt in hand instead of a Copper		
	And  Player1 has a Market in hand instead of a Copper	
	When Player1 plays a KingsCourt
	And Player1 selects a Market to KingsCourt
	Then Player1 should have 6 cards in hand
	And Player1 should have 3 remaining actions
	And Player1 should have 4 buys
	And Player1 should have 3 to spend