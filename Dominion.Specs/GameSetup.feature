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

Scenario Outline: Available provinces for number of players
	Given A new game with <player count> players
	Then There should be <province count> Province available to buy

	Examples:
		|player count	|province count	|
		|3				|12				|
		|4				|12				|
		|5				|15				|
		|6				|18				|

Scenario Outline: Available curses for number of players
	Given A new game with <player count> players
	Then There should be <curse count> Curse available to buy

	Examples:
		|player count	|curse count	|
		|3				|20				|
		|4				|30				|
		|5				|40				|
		|6				|50				|

	
