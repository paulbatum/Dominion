Feature: Festival

Background:
	Given A new game with 3 players	
	And Player1 has a Festival in hand instead of a Copper
	When Player1 plays a Festival

Scenario: Gain 2 Spend
	Then Player1 should have 2 to spend

Scenario: Gain 1 Buy
	Then Player1 should have 2 buys

Scenario: Gain 2 Actions
	Then Player1 should have 2 remaining action
