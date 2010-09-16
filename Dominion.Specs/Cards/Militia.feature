Feature: Militia
	
Scenario: Players are expected to discard cards when militia is played
	Given A new game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	When Player1 plays a Militia
	Then Player1 should have 2 to spend
	And Player2 must select 2 cards to discard
	And Player3 must select 2 cards to discard	

Scenario: Players have discarded cards once Militia has resolved
	Given A new game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	When Player1 plays a Militia
	And Player2 selects 2 Copper to discard
	And Player3 selects 2 Copper to discard
	Then All actions should be resolved
	And Player2 should have 3 cards in hand
	And Player3 should have 3 cards in hand