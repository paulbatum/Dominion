Feature: GhostShip
	
Scenario: Players are expected to put cards on their deck when Ghost Ship is played
	Given A new game with 2 players	
	And Player1 has a GhostShip in hand instead of a Copper
	When Player1 plays a GhostShip
	Then Player1 should have 6 cards in hand
	And Player2 must select 1 card to put on top of the deck
	When Player2 selects a Copper to go on top 
	Then Player2 must select 1 card to put on top of the deck (again)	

Scenario: Players put cards on top of their deck
	Given A new game with 2 players	
	And Player1 has a GhostShip in hand instead of a Copper
	And Player2 has a hand of Copper, Copper, Copper, Copper, Copper
	And Player2 has a deck of Estate, Estate, Estate, Estate, Estate
	When Player1 plays a GhostShip
	And Player2 selects a Copper to go on top 
	And Player2 selects a Copper to go on top (again)
	Then All actions should be resolved
	And Player2 should have a deck of: Copper, Copper, Estate, Estate, Estate, Estate, Estate
	And Player2 should have 3 cards in hand

Scenario: Ghost ship played twice via Throne Room
	Given A new game with 2 players	
	And Player1 has a GhostShip in hand instead of a Copper
	And Player1 has a ThroneRoom in hand instead of a Copper
	When Player1 plays a ThroneRoom
	And Player1 selects a GhostShip to throne room
	And Player2 selects a Copper to go on top 
	And Player2 selects a Copper to go on top (again)
	Then All actions should be resolved
	And Player1 should have 7 cards in hand

@Hosted
Scenario: Information on Ghost Ship effect
	Given A new hosted game with 2 players		
	And Player1 has a GhostShip in hand instead of a Copper	
	When The game begins
	And Player1 tells the host to play GhostShip
	Then Player2's current activity should have a type of SelectFixedNumberOfCards 
	Then Player2's current activity should have a hint of RedrawCards
	Then Player2's current activity should have a source of GhostShip