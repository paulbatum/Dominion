Feature: Bureaucrat

Scenario: Player plays a bureaucrat, puts silver on top and then players must select a victory card on top
	Given A new game with 3 players	
	And Player1 has a Bureaucrat in hand instead of a Copper
	And Player2 has a hand of Copper, Copper, Copper, Duchy, Estate
	And Player3 has a hand of Copper, Copper, Copper, Duchy, Estate
	When Player1 plays a Bureaucrat
	Then Player1 should have a Silver on top of the deck	
	And Player2 must select 1 victory card
	And Player3 must select 1 victory card

Scenario: Player plays a bureaucrat and opponent puts selected victory card on top
	Given A new game with 2 players	
	And Player1 has a Bureaucrat in hand instead of a Copper
	And Player2 has a hand of Copper, Copper, Copper, Duchy, Estate
	When Player1 plays a Bureaucrat
	And Player2 selects a Duchy to put on top
	Then Player2 should have a Duchy on top of the deck	
	And Player2 should have 4 cards in hand

Scenario: Opponents automatically put victory card on top if there is no choice
	Given A new game with 2 players	
	And Player1 has a Bureaucrat in hand instead of a Copper
	And Player2 has a hand of Copper, Copper, Copper, Copper, Duchy
	When Player1 plays a Bureaucrat
	Then Player2 should have a Duchy on top of the deck	
	And Player2 should have 4 cards in hand

Scenario: Opponents automatically reveal if there are no victory cards
	Given A new game with 2 players	
	And Player1 has a Bureaucrat in hand instead of a Copper
	And Player2 has a hand of Copper, Copper, Copper, Copper, Copper
	When Player1 plays a Bureaucrat
	Then The game log should report that Player2 revealed Copper, Copper, Copper, Copper, Copper
	And All actions should be resolved