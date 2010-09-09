Feature: Game Host
	In order to play with other people
	As a Dominion player
	I want to interact with the game 
	And know when the game state changes

Scenario: Player interacts with game
	Given A new hosted game with 3 players		
	And The game has begun
	When Player1 tells the host to buy Copper
	Then All players should recieve a game state update
