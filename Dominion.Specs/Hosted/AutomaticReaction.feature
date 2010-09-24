Feature: Automatic Reaction
	In order to speed up gameplay
	As a dominion player
	I want my reaction to automatically be played when attacked

Scenario: Automatically react with moat
	Given A new hosted game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a Moat in hand instead of a Copper
	When Player1 tells the host to play Militia	
	Then Player2 must wait
	And Player3 must select 2 cards to discard

Scenario: Automatically react when holding multiples of same reaction 
	Given A new hosted game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a hand of Moat, Moat, Copper, Copper, Copper
	When Player1 tells the host to play Militia	
	Then Player2 must wait
	And Player3 must select 2 cards to discard

Scenario: Do not automatically react when holding different reactions
	Given A new hosted game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a hand of SecretChamber, Moat, Copper, Copper, Copper
	When Player1 tells the host to play Militia	
	Then Player2 may reveal a reaction	