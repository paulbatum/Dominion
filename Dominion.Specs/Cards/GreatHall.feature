Feature: Great Hall

Background: 
	Given A new game with 3 players	

Scenario: GreatHallInDeck
	And Player1 has a GreatHall in hand instead of a Copper
	Then Player1 should have 4 victory points

Scenario: PlayGreatHall
	And Player1 has a GreatHall in hand instead of a Copper
	When Player1 plays a GreatHall
	Then Player1 should have 5 cards in hand
	Then Player1 should have 1 remaining action