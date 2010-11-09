Feature: ShantyTown

Scenario: Play Shanty Town with no other actions in hand
	Given A new game with 2 players	
	And Player1 has a ShantyTown in hand instead of a Copper
	When Player1 plays a ShantyTown
	Then Player1 should have 2 remaining action
	Then Player1 should have 6 cards in hand

Scenario: Play Shanty Town with another action in hand
	Given A new game with 2 players	
	And Player1 has a ShantyTown in hand instead of a Copper
	And Player1 has a ShantyTown in hand instead of a Copper
	When Player1 plays a ShantyTown
	Then Player1 should have 2 remaining action
	Then Player1 should have 4 cards in hand
