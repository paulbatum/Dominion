Feature: SeaHag

Scenario: Play sea hag
	Given A new game with 3 players	
	And Player1 has a SeaHag in hand instead of a Copper
	When Player1 plays a SeaHag
	Then Player1 should have 4 cards in hand
	Then Player2 should have a Curse on top of the deck
	Then Player2 should have a card on top of the discard pile
	Then Player3 should have a Curse on top of the deck
	Then Player3 should have a card on top of the discard pile

Scenario: Sea hag with no cards in deck or discard pile
	Given A new game with 2 players	
	And Player1 has a SeaHag in hand instead of a Copper
	And Player2 has no cards in deck
	When Player1 plays a SeaHag
	Then Player2 should have a Curse on top of the deck
	Then Player2 should have no cards in the discard pile
