Feature: Coppersmith

Background:	
	Given A new game with 3 players	
	And Player1 has a hand of Coppersmith, Copper, Copper, Copper, ThroneRoom

Scenario: Copper is improved in value
	When Player1 plays a Coppersmith
	And Player1 moves to the buy step
	Then Player1 should have 6 to spend

Scenario: Coppersmith is cumulative
	When Player1 plays a ThroneRoom
	And Player1 selects a Coppersmith to ThroneRoom
	And Player1 moves to the buy step
	Then Player1 should have 9 to spend

Scenario: Coppersmith only modifies Copper value
	Given A new game with 3 players	
	And Player1 has a hand of Coppersmith, Copper, Silver, Gold, ThroneRoom
	When Player1 plays a Coppersmith
	And Player1 moves to the buy step
	Then Player1 should have 7 to spend
