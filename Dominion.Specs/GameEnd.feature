Feature: Game End
	In order to avoid playing Dominion forever
	As a Dominion player
	I want the game to end at some point

Scenario: Game ends when last province is bought
	Given A new game with 3 players
	But There is only 1 Province left	
	And Player1 has a hand of all Gold	
	When Player1 moves to the buy step
	And Player1 buys a Province	
	And Player1 ends their turn
	Then The game should have ended
