Feature: Forge

Scenario: Player plays forge and must select cards to trash
	Given A new game with 3 players
	And Player1 has a hand of Forge, Estate, Estate, Estate, Copper
	When Player1 plays a Forge
	Then Player1 may select up to 4 cards from their hand

Scenario: Player must choose a card to gain
	Given A new game with 3 players
	And Player1 has a hand of Forge, Estate, Estate, Estate, Copper
	When Player1 plays a Forge
	And Player1 selects cards [Estate, Estate, Estate, Copper] to trash
	Then Player1 must gain a card of exact cost 6
	And The trash pile should be Estate, Estate, Estate, Copper

Scenario: Player gains a card
	Given A new game with 3 players
	And Player1 has a hand of Forge, Estate, Estate, Estate, Copper
	When Player1 plays a Forge
	And Player1 selects cards [Estate, Estate, Estate, Copper] to trash
	And Player1 gains a Gold
	Then Player1 should have a Gold on top of the discard pile	

Scenario: Player has no cards in hand to trash
	Given A new game with 3 players
	And Player1 has a hand of Forge
	When Player1 plays a Forge
	Then Player1 must gain a card of exact cost 0

Scenario: There is no card of appropriate cost to gain
	Given A new game with 3 players
	And Player1 has a hand of Forge, Silver, Silver, Estate, Estate
	When Player1 plays a Forge
	And Player1 selects cards [Silver, Silver, Estate, Estate] to trash
	Then All actions should be resolved
	And The trash pile should be Silver, Silver, Estate, Estate
