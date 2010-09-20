Feature: Scoring
	In order to know how well I did in the game
	As a Dominion player
	I want the scores to be calculated

Scenario: Players start with 3 points
	Given A new game with 3 players		
	When The game is scored
	Then Player1 should have 3 victory points
	And Player2 should have 3 victory points
	And Player3 should have 3 victory points

Scenario: Score cards in all regions
	Given A new game with 3 players		
	And Player1 has a Curse in hand instead of a Copper
	And Player1 has a GreatHall in play
	And Player1 has a Province in the discard pile
	When The game is scored
	Then Player1 should have 9 victory points

Scenario: Show sorted deck in play area once game is scored
	Given A new game with 3 players		
	And Player1 has a Curse in hand instead of a Copper
	And Player1 has a GreatHall in play
	And Player1 has a Province in the discard pile
	When The game is scored
	Then Player1's play area should start with this sequence of cards: Province, Estate, Estate, Estate, GreatHall, Curse