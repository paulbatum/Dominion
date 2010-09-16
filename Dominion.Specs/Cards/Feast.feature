Feature: Feast

Background:
	Given A new game with 3 players	
	And Player1 has a Feast in hand instead of a Copper
	When Player1 plays a Feast

Scenario: Player must choose a card to gain
	Then Player1 must gain a card of cost 5 or less

Scenario: Player gains card
	When Player1 gains a Silver
	Then Player1 should have a Silver on top of the discard pile
	Then There should be a Feast on top of the trash pile
