Feature: Ironworks	


Scenario: Player play Ironworks and must choose a card to gain
	Given A new game with 3 players	
	And Player1 has a Ironworks in hand instead of a Copper
	When Player1 plays a Ironworks
	Then Player1 must gain a card of cost 4 or less

Scenario: Player play Ironworks and gains a Silver
	Given A new game with 3 players	
	And Player1 has a Ironworks in hand instead of a Copper
	When Player1 plays a Ironworks
	When Player1 gains a Silver
	Then Player1 should have a Silver on top of the discard pile
	And Player1 should have 1 to spend

Scenario: Player play Ironworks and gains an Estate
	Given A new game with 3 players	
	And Player1 has a Ironworks in hand instead of a Copper
	When Player1 plays a Ironworks
	When Player1 gains an Estate
	Then Player1 should have a Estate on top of the discard pile
	And Player1 should have 5 cards in hand

Scenario: Player play Ironworks and gains an Ironworks
	Given A new game with 3 players	
	And Ironworks is available to buy
	And Player1 has a Ironworks in hand instead of a Copper
	When Player1 plays a Ironworks
	When Player1 gains an Ironworks
	Then Player1 should have a Ironworks on top of the discard pile
	And Player1 should have 1 action remaining

Scenario: Player play Ironworks and gains a GreatHall
	Given A new game with 3 players	
	And GreatHall is available to buy	
	And Player1 has a Ironworks in hand instead of a Copper
	When Player1 plays a Ironworks
	When Player1 gains a GreatHall
	Then Player1 should have a GreatHall on top of the discard pile
	And Player1 should have 1 action remaining
	And Player1 should have 5 cards in hand