Feature: Cutpurse

Scenario: Cutpurse is played and players discard copper
	Given A new game with 3 players	
	And Player1 has a Cutpurse in hand instead of a Copper
	When Player1 plays a Cutpurse
	Then Player1 should have 2 to spend
	And Player2 should have 4 cards in hand
	And Player2 should have a Copper on top of the discard pile
	And Player3 should have 4 cards in hand
	And Player3 should have a Copper on top of the discard pile

Scenario: Cutpurse is played but opponent has no copper
	Given A new game with 2 players	
	And Player1 has a Cutpurse in hand instead of a Copper
	And Player2 has a hand of Estate, Estate, Silver, Silver, Silver
	When Player1 plays a Cutpurse
	Then All actions should be resolved
	And Player2 should have 5 cards in hand
