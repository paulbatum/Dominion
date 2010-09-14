Feature: Game Log
	In order to know what is going on
	As a Dominion player
	I want to be told when players do things

Scenario: A player begins their turn
	Given A new game with 3 players		
	Then The game log should report that Player1's turn has begun

Scenario: A player plays an action
	Given A new game with 3 players		
	And Player1 has a Woodcutter in hand instead of a Copper
	When Player1 plays a Woodcutter
	Then The game log should report that Player1 played a Woodcutter

Scenario: A player buys a card
	Given A new game with 3 players			
	When Player1 moves to the buy step
	And Player1 buys a Copper
	Then The game log should report that Player1 bought a Copper

@Ignore
Scenario: The game ends
	Given A new game with 3 players
	But There is only 1 Province left	
	And Player1 has a hand of all Gold	
	When Player1 moves to the buy step
	And Player1 buys a Province	
	And Player1 ends their turn
	Then The game log should report the scores
	And Player1 should be the winner
