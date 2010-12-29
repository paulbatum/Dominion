Feature: Tribute

Scenario: Play tribute, revealing a treasure card and an action card
	Given A new game with 3 players
	And Player1 has a Tribute in hand instead of a Copper
	And Player2 has a deck of Copper, Smithy, Estate, Estate, Copper
	When Player1 plays a Tribute
	Then Player1 should have 2 remaining actions
	And Player1 should have 2 to spend	

Scenario: Play tribute, revealing a curse card and a victory card
	Given A new game with 3 players
	And Player1 has a Tribute in hand instead of a Copper
	And Player2 has a deck of Curse, Estate, Estate, Estate, Copper
	When Player1 plays a Tribute
	Then Player1 should have 6 cards in hand

Scenario: Play tribute, revealing two of the same card
	Given A new game with 3 players
	And Player1 has a Tribute in hand instead of a Copper
	And Player2 has a deck of Copper, Copper, Estate, Estate, Copper
	When Player1 plays a Tribute
	Then Player1 should have 2 to spend	

Scenario: Play tribute, revealing two different treasure cards
	Given A new game with 3 players
	And Player1 has a Tribute in hand instead of a Copper
	And Player2 has a deck of Copper, Silver, Estate, Estate, Copper
	When Player1 plays a Tribute
	Then Player1 should have 4 to spend	

Scenario: Play tribute, revealing a copper and a great hall
	Given A new game with 3 players
	And Player1 has a Tribute in hand instead of a Copper
	And Player2 has a deck of Copper, GreatHall, Estate, Estate, Copper
	When Player1 plays a Tribute
	Then Player1 should have 2 to spend	
	Then Player1 should have 6 cards in hand
	Then Player1 should have 2 remaining actions

Scenario: Play tribute, opponent has no deck or discards
	Given A new game with 3 players
	And Player1 has a Tribute in hand instead of a Copper
	And Player2 has an empty deck
	When Player1 plays a Tribute
	Then All actions should be resolved
	And Player1 should have 4 cards in hand