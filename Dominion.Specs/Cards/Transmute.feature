Feature: Transmute

Scenario: Player transmutes an estate into gold
	Given A new game with 2 players		
	And Player1 has a hand of Transmute, Estate, Copper, Copper, Copper
	When Player1 plays a Transmute
	And Player1 selects an Estate to transmute
	Then Player1 should have 3 cards in hand
	And Player1 should have a Gold on top of the discard pile	
	And There should be an Estate on top of the trash pile	

Scenario: Player transmutes a copper into a transmute
	Given A new game with 2 players	
	And Player1 has a hand of Transmute, Estate, Copper, Copper, Copper
	And Transmute is available to buy
	When Player1 plays a Transmute
	And Player1 selects a Copper to transmute
	Then Player1 should have 3 cards in hand
	And Player1 should have a Transmute on top of the discard pile	
	And There should be a Copper on top of the trash pile	

Scenario: Player transmutes a transmute into a duchy
	Given A new game with 2 players	
	And Player1 has a hand of Transmute, Transmute, Copper, Copper, Copper
	When Player1 plays a Transmute
	And Player1 selects a Transmute to transmute
	Then Player1 should have 3 cards in hand
	And Player1 should have a Duchy on top of the discard pile	
	And There should be a Transmute on top of the trash pile		

Scenario: Player transmutes a great hall into a duchy and a gold
	Given A new game with 2 players	
	And Player1 has a hand of Transmute, GreatHall, Copper, Copper, Copper
	When Player1 plays a Transmute
	And Player1 selects a GreatHall to transmute
	Then Player1 should have 3 cards in hand
	And Player1 should have a discard pile of Duchy, Gold
	And There should be a GreatHall on top of the trash pile	

Scenario: Player transmutes a curse into nothing
	Given A new game with 2 players	
	And Player1 has a hand of Transmute, Curse, Copper, Copper, Copper
	When Player1 plays a Transmute
	And Player1 selects a Curse to transmute
	Then Player1 should have 3 cards in hand
	And Player1 should have 0 cards in the discard pile
	And There should be a Curse on top of the trash pile	

Scenario: Player plays transmute with no other cards in hand
	Given A new game with 2 players	
	And Player1 has a hand of Transmute
	When Player1 plays a Transmute
	Then All actions should be resolved