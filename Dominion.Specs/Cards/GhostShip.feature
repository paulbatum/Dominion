Feature: GhostShip
	
Scenario: Players have put cards on top once ghost ship has resolved
	Given A new game with 3 players	
	And Player1 has a GhostShip in hand instead of a Copper
	When Player1 plays a GhostShip
	Then All actions should be resolved
	And Player2 should have 3 cards in hand
	And Player2 should have 7 cards in deck
	And Player3 should have 3 cards in hand
	And Player3 should have 7 cards in deck

Scenario: Ghost ship give plus 2 cards
	Given A new game with 3 players	
	And Player1 has a GhostShip in hand instead of a Copper
	When Player1 plays a GhostShip
	Then Player1 should have 6 cards in hand

Scenario: Ghost ship played twice via Throne Room
	Given A new game with 3 players	
	And Player1 has a GhostShip in hand instead of a Copper
	And Player1 has a ThroneRoom in hand instead of a Copper
	When Player1 plays a ThroneRoom
	When Player1 selects a GhostShip to throne room
	Then All actions should be resolved
	And Player2 should have 3 cards in hand
	And Player2 should have 7 cards in deck
	And Player3 should have 3 cards in hand
	And Player3 should have 7 cards in deck
