Feature: Laboratory

Background:
	Given A new game with 3 players	
	And Player1 has a Laboratory in hand instead of a Copper
	When Player1 plays a Laboratory

Scenario: Gain 2 Cards
	Then Player1 should have 6 cards in hand

Scenario: Gain 1 Action
	Then Player1 should have 1 remaining action
