Feature: Mine

Background:	
	Given A new game with 3 players	
	And Player1 has a hand of Mine, Copper, Silver, Estate, Estate
	And Player1 has a Copper in the discard pile
	And There is a Province in the trash pile
	When Player1 plays a Mine

Scenario: Player must only choose a treasure card to mine
	Then Player1 must select 1 treasure (only) card to mine

Scenario: Player must choose a treasure card to gain
	And Player1 selects a Silver to mine
	And Player1 attempts to gain a Gold
	Then Player1 should have a Gold on top of the discard pile
	
Scenario: Player cannot buy too much
	And Player1 selects a Copper to mine
	And Player1 attempts to gain a Gold
	Then Player1 should have a Copper on top of the discard pile
	
Scenario: Mined card is trashed
	And Player1 selects a Copper to mine
	Then There should be a Copper on top of the trash pile	