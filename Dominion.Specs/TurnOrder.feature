Feature: Turn Order
	In order to play the game
	As a dominion player
	I alternate with the other players, taking turns

Scenario: Player1 goes first
	Given A new game with 3 players
	Then Player1 is the active player

Scenario: Player2 goes second
	Given A new game with 3 players
	When Player1 ends their turn
	Then Player2 is the active player

Scenario: Player3 goes third
	Given A new game with 3 players
	When Player1 ends their turn
	When Player2 ends their turn
	Then Player3 is the active player

Scenario: Player1 goes after Player3
	Given A new game with 3 players
	When Player1 ends their turn
	When Player2 ends their turn
	When Player3 ends their turn
	Then Player1 is the active player