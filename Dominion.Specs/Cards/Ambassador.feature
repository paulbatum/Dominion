Feature: Ambassador

Scenario: Player plays Ambassador and must select a card to reveal
	Given A new game with 3 players
	And Player1 has an Ambassador in hand instead of a Copper
	When Player1 plays an Ambassador
	Then Player1 must select a card to reveal

Scenario: Player plays Ambassador and selects a card
	Given A new game with 3 players
	And Player1 has a hand of Ambassador, Copper, Copper, Estate, Curse
	When Player1 plays an Ambassador
	And Player1 selects a Curse to reveal
	Then Player1 may select up to 2 Curse cards from their hand

Scenario: Player plays Ambassador and selects one curse
	Given A new game with 3 players
	And Player1 has a hand of Ambassador, Copper, Copper, Estate, Curse
	When Player1 plays an Ambassador
	And Player1 selects a Curse to reveal
	And Player1 selects a Curse to return to the supply
	Then Player1 should have 3 cards in hand
	And Player2 should have a Curse on top of the discard pile
	And Player3 should have a Curse on top of the discard pile
	And There should be 19 Curse available to buy

Scenario: Player plays Ambassador and selects two curses
	Given A new game with 3 players
	And Player1 has a hand of Ambassador, Copper, Copper, Curse, Curse
	When Player1 plays an Ambassador
	And Player1 selects a Curse to reveal
	And Player1 selects cards [Curse, Curse] to return to the supply
	Then Player1 should have 2 cards in hand
	And Player2 should have a Curse on top of the discard pile
	And Player3 should have a Curse on top of the discard pile
	And There should be 20 Curse available to buy

Scenario: Player plays Ambassador but doesn't want to return anything - the other players still gain a copy.
	Given A new game with 3 players
	And Player1 has a hand of Ambassador, Gold, Gold, Gold, Gold
	When Player1 plays an Ambassador
	And Player1 selects a Gold to reveal
	And Player1 selects nothing to return to the supply
	Then Player1 should have 4 cards in hand
	And Player2 should have a Gold on top of the discard pile
	And Player3 should have a Gold on top of the discard pile	

Scenario: Player plays Ambassador with nothing in hand
	Given A new game with 3 players
	And Player1 has a hand of Ambassador
	When Player1 plays an Ambassador
	Then All actions should be resolved


