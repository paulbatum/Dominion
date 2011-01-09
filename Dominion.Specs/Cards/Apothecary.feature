Feature: Apothecary

Scenario: Play Apothecary with all potions and coppers on top
	Given A new game with 3 players
	And Player1 has an Apothecary in hand instead of a Copper
	And Player1 has a deck of Estate, Copper, Potion, Copper, Copper
	When Player1 plays an Apothecary
	Then All actions should be resolved 
	And Player1 should have 9 cards in hand
	And Player1 should have 1 action remaining

Scenario: Play Apothecary with no potions or coppers on top
	Given A new game with 3 players
	And Player1 has an Apothecary in hand instead of a Copper
	And Player1 has a deck of Copper, Estate, Curse, Silver, Gold
	When Player1 plays an Apothecary
	Then Player1 must select a revealed card from: Estate, Curse, Silver, Gold
	When Player1 selects Estate from the revealed cards
	Then Player1 must select a revealed card from: Curse, Silver, Gold	
	When Player1 selects Silver from the revealed cards
	Then Player1 must select a revealed card from: Curse, Gold	
	When Player1 selects Curse from the revealed cards
	Then Player1 should have a deck of: Gold, Curse, Silver, Estate
	And All actions should be resolved		
		
Scenario: Apothecary is played with no other cards in hand, no discards and no deck
	Given A new game with 3 players	
	And Player1 has a hand of Apothecary
	And Player1 has an empty deck
	When Player1 plays a Apothecary
	Then Player1 should have 0 cards in hand
	And All actions should be resolved		
	
