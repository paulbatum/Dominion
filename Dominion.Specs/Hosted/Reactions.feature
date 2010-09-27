Feature: Reactions
	In order to play the game to it's fullest potential
	As a dominion player
	I want to be able to pull neat tricks with reactions

Scenario: Player uses Secret Chamber, draws a Moat, uses Moat, uses Secret Chamber again putting both reactions back on top of the deck
	Given A new hosted game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a hand of SecretChamber, Copper, Copper, Copper, Estate
	And Player2 has a deck of Moat, Estate, Estate, Copper, Copper
	When Player1 tells the host to play Militia
	And Player2 tells the host to reveal SecretChamber
	And Player2 tells the host to put Copper on top
	And Player2 tells the host to put Copper on top
	And Player2 tells the host to reveal Moat
	And Player2 tells the host to reveal SecretChamber
	And Player2 tells the host to put SecretChamber on top
	And Player2 tells the host to put Moat on top	
	Then Player2 should have a hand of Copper, Estate, Estate, Copper, Copper
	And Player2 should have a deck of: Moat, SecretChamber, Estate, Copper, Copper
