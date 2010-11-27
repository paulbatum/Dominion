Feature: Activity Information
	In order to make decent AI's
	As a programmer
	I want more information about the current activity

Scenario: Information on Militia discard
	Given A new hosted game with 2 players		
	And Player1 has a Militia in hand instead of a Copper	
	When The game begins
	And Player1 tells the host to play Militia
	Then Player2's current activity has a type of SelectFixedNumberOfCards 
	Then Player2's current activity has a hint of DiscardCards
	Then Player2's current activity has a source of Militia
