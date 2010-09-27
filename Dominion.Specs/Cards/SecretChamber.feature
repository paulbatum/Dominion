Feature: Secret Chamber
	
Scenario: Player must select a number of cards to discard
	Given A new game with 3 players	
	And Player1 has a SecretChamber in hand instead of a Copper
	When Player1 plays a SecretChamber
	Then Player1 may select up to 4 cards from their hand

Scenario: Player discards one card to Secret Chamber
	Given A new game with 3 players	
	And Player1 has a SecretChamber in hand instead of a Copper
	When Player1 plays a SecretChamber
	And  Player1 selects a Copper to discard
	Then Player1 should have 1 to spend	
	And Player1 should have a Copper on top of the discard pile

Scenario: Player discards nothing to Secret Chamber
	Given A new game with 3 players	
	And Player1 has a SecretChamber in hand instead of a Copper
	When Player1 plays a SecretChamber
	And  Player1 selects nothing to discard
	Then Player1 should have 0 to spend	
	And All actions should be resolved

Scenario: Secret Chamber is a reaction
	Given A new game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a SecretChamber in hand instead of a Copper
	When Player1 plays a Militia	
	Then Player2 may reveal a reaction

Scenario: Player uses Secret Chamber's reaction effect and is prompted to select cards
	Given A new game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a SecretChamber in hand instead of a Copper
	When Player1 plays a Militia
	When Player2 reveals SecretChamber
	Then Player2 must select 1 card to put on top of the deck
	When Player2 selects a Copper to go on top 
	Then Player2 must select 1 card to put on top of the deck (again)

Scenario: Player uses Secret Chamber's reaction effect and cards are put on top
	Given A new game with 3 players	
	And Player1 has a Militia in hand instead of a Copper
	And Player2 has a hand of SecretChamber, Copper, Copper, Copper, Estate
	And Player2 has a deck of Estate, Estate, Copper, Copper, Copper
	When Player1 plays a Militia
	When Player2 reveals SecretChamber
	When Player2 selects a SecretChamber to go on top
	When Player2 selects a Copper to go on top 
	Then Player2 should have a hand of Copper, Copper, Estate, Estate, Estate
	And Player2 should have a deck of: Copper, SecretChamber, Copper, Copper, Copper
