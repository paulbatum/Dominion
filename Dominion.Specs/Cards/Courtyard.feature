Feature: Courtyard

Scenario: Player plays Courtyard, draws 3 and puts one card from hand back on top
	Given A new game with 3 players	
	And Player1 has a hand of Courtyard, Militia, Copper, Copper, Estate
	When Player1 plays a Courtyard
	Then Player1 should have 7 cards in hand
	And Player1 must select 1 card to put on top of the deck
	When Player1 selects a Militia to put on top
	Then Player1 should have 6 cards in hand
	And Player1 should have a Militia on top of the deck

	
Scenario: Courtyard is played with no other cards in hand, no discards and no deck
	Given A new game with 3 players	
	And Player1 has a hand of Courtyard
	And Player1 has an empty deck
	When Player1 plays a Courtyard
	Then Player1 should have 0 cards in hand
	And All actions should be resolved
