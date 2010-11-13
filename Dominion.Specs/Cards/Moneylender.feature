Feature: Moneylender

Scenario: Moneylender with copper in hand
	Given A new game with 3 players
	And Player1 has a hand of Moneylender, Copper, Estate, Estate, Estate
	When Player1 plays a Moneylender
	Then Player1 should have 3 to spend
	And There should be a Copper on top of the trash pile	

Scenario: Moneylender without copper in hand
	Given A new game with 3 players
	And Player1 has a hand of Moneylender, Estate, Estate, Estate, Estate
	When Player1 plays a Moneylender
	Then Player1 should have 0 to spend
