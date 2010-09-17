Feature: Remodel

Scenario: Player must choose a card to remodel
	Given A new game with 3 players	
	And Player1 has a Remodel in hand instead of a Copper
	When Player1 plays a Remodel
	Then Player1 must select 1 card to remodel

Scenario: Player remodels copper
	Given A new game with 3 players	
	And Player1 has a Remodel in hand instead of a Copper
	When Player1 plays a Remodel
	And Player1 selects a Copper to remodel
	Then Player1 must gain a card of cost 2 or less

Scenario: Player remodels copper choosing estate
	Given A new game with 3 players	
	And Player1 has a Remodel in hand instead of a Copper
	When Player1 plays a Remodel
	And Player1 selects a Copper to remodel
	And Player1 gains a Estate
	Then There should be a Copper on top of the trash pile
	Then Player1 should have a Estate on top of the discard pile
