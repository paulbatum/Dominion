Feature: Apprentice

Scenario: Apprentice trashes a card and is +1 action
	Given A new game with 3 players	
	And Player1 has a Apprentice in hand instead of a Copper
	When Player1 plays a Apprentice
	Then Player1 must select 1 card to trash
	And Player1 should have 1 remaining action

Scenario: Apprentice trashing estate draws 2 cards
	Given A new game with 3 players	
	And Player1 has a hand of Apprentice, Estate, Copper, Copper, Copper
	When Player1 plays a Apprentice
	And Player1 selects a Estate to trash
	Then Player1 should have 5 cards in hand
	And There should be a Estate on top of the trash pile	

Scenario: Apprentice with no other cards in hand
	Given A new game with 3 players	
	And Player1 has a hand of Apprentice
	When Player1 plays a Apprentice
	Then All actions should be resolved

Scenario: Apprentice trashing Familiar draws 5 cards
	Given A new game with 3 players	
	And Player1 has a hand of Apprentice, Familiar, Estate, Estate, Estate
	When Player1 plays a Apprentice
	And Player1 selects a Familiar to trash
	Then Player1 should have 8 cards in hand