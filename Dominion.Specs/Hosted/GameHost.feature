Feature: Game Host
	In order to play with other people
	As a Dominion player
	I want to interact with the game 
	And know when the game state changes

Scenario: Game Begins
	Given A new hosted game with 3 players		
	When The game begins
	Then All players should recieve 1 game state update

Scenario: Player interacts with game
	Given A new hosted game with 3 players		
	When The game begins
	And Player1 tells the host to buy Copper
	Then All players should recieve 2 game state updates
