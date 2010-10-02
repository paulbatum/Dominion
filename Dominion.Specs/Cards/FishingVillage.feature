Feature: FishingVillage

Scenario: Fishing Village Initial Effect
	Given A new game with 2 players	
	And Player1 has a FishingVillage in hand instead of a Copper
	When Player1 plays a FishingVillage
	Then Player1 should have 2 remaining action
	And Player1 should have 1 to spend

Scenario: Fishing Village Second Effect
	Given A new game with 2 players	
	And Player1 has a FishingVillage in hand instead of a Copper
	When Player1 plays a FishingVillage
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 is the active player
	And Player1 should have 2 remaining action
	And Player1 should have 1 to spend

Scenario: Fishing Village is not Discarded Immediately
	Given A new game with 2 players	
	And Player1 has a FishingVillage in hand instead of a Copper
	When Player1 plays a FishingVillage
	And Player1 ends their turn
	Then Player1 should have 4 cards in the discard pile
	
Scenario: Fishing Village is Discarded After Second Turn
	Given A new game with 2 players	
	And Player1 has a FishingVillage in hand instead of a Copper
	When Player1 plays a FishingVillage
	And Player1 ends their turn
	And Player2 ends their turn
	And Player1 ends their turn
	Then Player1 should have 0 cards in the discard pile
	And Player1 should have a deck of 5 cards
	
Scenario: Fishing Village Has No Effect After Two Turns
	Given A new game with 2 players	
	And Player1 has a FishingVillage in hand instead of a Copper
	When Player1 plays a FishingVillage
	And Player1 ends their turn
	And Player2 ends their turn
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 is the active player
	And Player1 should have 1 remaining action
	And Player1 should have 0 to spend
