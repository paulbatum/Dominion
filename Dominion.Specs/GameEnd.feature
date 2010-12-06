Feature: Game End
	In order to avoid playing Dominion forever
	As a Dominion player
	I want the game to end at some point

Scenario: Game ends when last province is bought
	Given A new game with 3 players
	But There is only 1 Province left	
	And Player1 has 5 Gold in hand	
	When Player1 moves to the buy step
	And Player1 buys a Province	
	And Player1 ends their turn
	Then The game should have ended

Scenario Outline: Game ends when piles are exhausted
	Given A new game with <player count> players
	And There are <empty pile count> empty piles		
	When Player1 ends their turn
	Then The game should have ended

	Examples:
		|player count	|empty pile count	|
		|1				|3					|
		|2				|3					|
		|3				|3					|
		|4				|3					|
		|5				|4					|
		|6				|4					|

Scenario: Game still ends when more piles are exhausted than necessary
	Given A new game with 4 players
	And There are 4 empty piles		
	When Player1 ends their turn
	Then The game should have ended

Scenario: Game isn't over until the last turn ends
	Given A new game with 3 players
	But There is only 1 Province left	
	And Player1 has 5 Gold in hand	
	When Player1 moves to the buy step
	And Player1 buys a Province		
	Then The game should not have ended

