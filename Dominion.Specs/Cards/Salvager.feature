Feature: Salvager

Scenario: Salvager trashes a card
	Given A new game with 3 players	
	And Player1 has a Salvager in hand instead of a Copper
	When Player1 plays a Salvager
	Then Player1 must select 1 card to trash

Scenario: Salvager trashing estate gives plus 2 spend
	Given A new game with 3 players	
	And Player1 has a hand of Salvager, Estate, Copper, Copper, Copper
	When Player1 plays a Salvager
	And Player1 selects a Estate to trash
	Then Player1 should have 2 to spend
	And Player1 should have 2 buys
	And There should be a Estate on top of the trash pile	

Scenario: Salvager with no other cards in hand
	Given A new game with 3 players	
	And Player1 has a hand of Salvager
	When Player1 plays a Salvager
	Then All actions should be resolved
	And Player1 should have 2 buys

Scenario: Salvaging a potion card does not let the player buy a potion card
	Given A new game with 3 players	
	And Player1 has a hand of Salvager, Golem, Estate, Estate, Estate
	When Player1 plays a Salvager
	And Player1 selects a Golem to trash
	Then Player1 should have 4 to spend