Feature: Mining Village

Scenario: Player must choose whether to trash Mining Village
	Given A new game with 3 players
	And Player1 has a MiningVillage in hand instead of a Copper
	When Player1 plays a MiningVillage
	Then Player1 must choose whether to trash MiningVillage (Yes or No)

Scenario: Player does not trash Mining Village
	Given A new game with 3 players
	And Player1 has a MiningVillage in hand instead of a Copper
	When Player1 plays a MiningVillage
	And Player1 chooses to not to trash MiningVillage (No)	
	Then Player1 should have 0 to spend	
	And Player1 should have 5 cards in hand
	And Player1 should have 2 actions remaining

Scenario: Player trashes Mining Village
	Given A new game with 3 players
	And Player1 has a MiningVillage in hand instead of a Copper
	When Player1 plays a MiningVillage
	And Player1 chooses to trash MiningVillage (Yes)	
	Then Player1 should have 2 to spend	
	And Player1 should have 5 cards in hand
	And Player1 should have 2 actions remaining
	And There should be a MiningVillage on top of the trash pile	

Scenario: Using Throne Room on a Mining Village should draw one card and then prompt
	Given A new game with 3 players
	And Player1 has a hand of ThroneRoom, MiningVillage, Copper, Copper, Estate
	When Player1 plays a ThroneRoom
	When Player1 selects a MiningVillage to ThroneRoom
	Then Player1 must choose whether to trash MiningVillage (Yes or No)	
	And Player1 should have 4 cards in hand
	And Player1 should have 2 actions remaining	

Scenario: Using Throne Room on a Mining Village and then trashing should not prompt to trash a second time
	Given A new game with 3 players
	And Player1 has a hand of ThroneRoom, MiningVillage, Copper, Copper, Estate
	When Player1 plays a ThroneRoom
	When Player1 selects a MiningVillage to ThroneRoom
	And Player1 chooses to trash MiningVillage (Yes)	
	Then All actions should be resolved
	And Player1 should have 5 cards in hand
	And Player1 should have 4 actions remaining
	And There should be a MiningVillage on top of the trash pile	
	And Player1 should have 2 to spend	
		
Scenario: Can trash a Mining Village revealed via a Golem
	Given A new game with 3 players
	And Player1 has a Golem in hand instead of a Copper
	And Player1 has a deck of MiningVillage, Copper, Copper, Copper, Copper
	When Player1 plays a Golem
	And Player1 chooses to trash MiningVillage (Yes)
	Then Player1 should have 2 to spend	
	And Player1 should have 5 cards in hand
	And Player1 should have 2 actions remaining
	And There should be a MiningVillage on top of the trash pile	