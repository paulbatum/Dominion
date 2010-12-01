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
		|1				|12				|
		|2				|12				|
		|3				|12				|
		|4				|12				|
		|5				|15				|
		|6				|18				|

Scenario Outline: Available curses for number of players
	Given A new game with <player count> players
	Then There should be <curse count> Curse available to buy

	Examples:
		|player count	|curse count	|
		|1				|10				|
		|2				|10				|
		|3				|20				|
		|4				|30				|
		|5				|40				|
		|6				|50				|


Scenario: No potions are available if there are no cards that cost them
	Given A new game with 3 players
	Then There should be no Potion pile

Scenario: Potions are available if there are cards that cost them
	Given A new game with 3 players and bank of Familiar, Militia, Smithy, Village, Cellar, Gardens, Market, Festival, Witch, CouncilRoom
	Then There should be 20 Potion available to buy