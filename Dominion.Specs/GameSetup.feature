Feature: Game Setup
	In order to play dominion
	As a player
	I want the game to be set up according to the rules

Scenario: Initial deck
	Given A new game with 3 players
	Then The initial deck for each player should comprise of 7 Copper and 3 Estate

Scenario: Draw opening hand
	Given A new game with 3 players	
	Then Each player should have 5 cards in hand 

Scenario: Available provinces in 3 player game
	Given A new game with 3 players	
	Then There should be 12 Province available to buy