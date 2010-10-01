Feature: Expand

Scenario: Player must choose a card to Expand
	Given A new game with 3 players	
	And Player1 has a Expand in hand instead of a Copper
	When Player1 plays a Expand
	Then Player1 must select 1 card to Expand

Scenario: Player Expands Copper
	Given A new game with 3 players	
	And Player1 has a Expand in hand instead of a Copper
	When Player1 plays a Expand
	And Player1 selects a Copper to Expand
	Then Player1 must gain a card of cost 3 or less

Scenario: Player Expands Copper choosing silver
	Given A new game with 3 players	
	And Player1 has a Expand in hand instead of a Copper
	When Player1 plays a Expand
	And Player1 selects a Copper to Expand
	And Player1 gains a Silver
	Then There should be a Copper on top of the trash pile
	Then Player1 should have a Silver on top of the discard pile
