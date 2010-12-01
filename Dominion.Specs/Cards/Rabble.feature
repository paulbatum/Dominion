Feature: Rabble

Scenario: Rabble is +3 cards
	Given A new game with 2 players
	And Player1 has a Rabble in hand instead of a Copper
	When Player1 plays a Rabble
	Then Player1 should have 7 cards in hand

Scenario: Player plays Rabble and the revealed cards are all treasure and actions
	Given A new game with 2 players
	And Player1 has a Rabble in hand instead of a Copper
	And Player2 has a deck of Copper, Militia, Copper, Estate, Estate
	When Player1 plays a Rabble
	Then Player2 should have 3 cards in the discard pile
	And All actions should be resolved

Scenario: Player plays Rabble and the revealed cards include two non-treasure non-action cards
	Given A new game with 2 players
	And Player1 has a Rabble in hand instead of a Copper
	And Player2 has a deck of Estate, Curse, Copper, Copper, Estate
	When Player1 plays a Rabble	
	Then Player2 must select a revealed card from: Estate, Curse	
	When Player2 selects Estate from the revealed cards
	Then Player2 must select a revealed card from: Curse	
	When Player2 selects Curse from the revealed cards
	Then Player2 should have a deck of: Curse, Estate, Copper, Estate
	And All actions should be resolved

@Hosted
Scenario: Information on Rabble effect
	Given A new hosted game with 2 players		
	And Player1 has a Rabble in hand instead of a Copper	
	And Player2 has a deck of Estate, Curse, Copper, Copper, Estate
	When The game begins
	And Player1 tells the host to play Rabble
	Then Player2's current activity should have a type of SelectFromRevealed 
	Then Player2's current activity should have a hint of RedrawCards
	Then Player2's current activity should have a source of Rabble