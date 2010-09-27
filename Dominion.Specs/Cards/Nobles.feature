Feature: Nobles

Background:	
	Given A new game with 3 players	
	And Player1 has a hand of Nobles, Copper, Silver, Estate, Estate
	When Player1 plays a Nobles

Scenario: Player must choose between cards or actions
	Then Player1 must choose from DrawCards, GainActions

Scenario: Player gets 3 Cards for DrawCards
	And Player1 chooses DrawCards
	Then Player1 should have 7 cards in hand
	And Player1 should have 0 actions remaining
	
Scenario: Player gets 2 Actions for GainActions
	And Player1 chooses GainActions
	Then Player1 should have 2 actions remaining
	And Player1 should have 4 cards in hand
	
Scenario: Is Worth 2 Victory Points
	Given A new game with 3 players	
	And Player1 has a Nobles in hand instead of a Copper
	When The game is scored
	Then Player1 should have 5 victory points