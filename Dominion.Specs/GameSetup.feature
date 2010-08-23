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

Scenario: Available provinces in 4 player game
	Given A new game with 4 players	
	Then There should be 12 Province available to buy

Scenario: Available provinces in 5 player game
	Given A new game with 5 players	
	Then There should be 15 Province available to buy

Scenario: Available provinces in 6 player game
	Given A new game with 6 players	
	Then There should be 18 Province available to buy

Scenario: Available curses in 3 player game
	Given A new game with 3 players	
	Then There should be 20 Curse available to buy

Scenario: Available curses in 4 player game
	Given A new game with 4 players	
	Then There should be 30 Curse available to buy

Scenario: Available curses in 5 player game
	Given A new game with 5 players	
	Then There should be 40 Curse available to buy

Scenario: Available curses in 6 player game
	Given A new game with 6 players	
	Then There should be 50 Curse available to buy