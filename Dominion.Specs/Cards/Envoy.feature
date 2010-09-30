Feature: Envoy

Scenario: Player plays Envoy, opponent must choose which card they don't draw
	Given A new game with 3 players
	And Player1 has a Envoy in hand instead of a Copper
	And Player1 has a deck of Silver, Copper, Estate, Copper, Copper
	When Player1 plays a Envoy
	Then Player2 must select a revealed card from: Silver, Copper, Estate, Copper, Copper

Scenario: Player plays Envoy, draws 4 cards
	Given A new game with 3 players
	And Player1 has a hand of Envoy, Copper, Estate, Copper, Copper	
	And Player1 has a deck of Silver, Copper, Estate, Copper, Copper
	When Player1 plays a Envoy
	When Player2 selects Silver from the revealed cards
	Then Player1 should have a hand of Copper, Estate, Copper, Copper, Copper, Estate, Copper, Copper	