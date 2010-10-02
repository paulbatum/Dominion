Feature: Caravan

Scenario: Caravan Initial Effect
	Given A new game with 2 players	
	And Player1 has a Caravan in hand instead of a Copper
	When Player1 plays a Caravan
	Then Player1 should have 1 remaining action
	And Player1 should have 5 cards in hand

Scenario: Caravan Second Turn Effect
	Given A new game with 2 players	
	And Player1 has a Caravan in hand instead of a Copper
	When Player1 plays a Caravan
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 is the active player
	And Player1 should have 1 remaining action
	And Player1 should have 6 cards in hand

Scenario: Caravan is not Discarded Immediately
	Given A new game with 2 players	
	And Player1 has a Caravan in hand instead of a Copper
	When Player1 plays a Caravan
	And Player1 ends their turn
	Then Player1 should have 0 cards in the discard pile
	And Player1 should have a deck of 4 cards
	
Scenario: Caravan is Discarded After Second Turn
	Given A new game with 2 players	
	And Player1 has a Caravan in hand instead of a Copper
	When Player1 plays a Caravan
	And Player1 ends their turn
	And Player2 ends their turn
	And Player1 ends their turn
	Then Player1 should have 0 cards in the discard pile
	And Player1 should have a deck of 5 cards
	
Scenario: Caravan Has No Effect After Two Turns
	Given A new game with 2 players	
	And Player1 has a Caravan in hand instead of a Copper
	When Player1 plays a Caravan
	And Player1 ends their turn
	And Player2 ends their turn
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 is the active player
	And Player1 should have 1 remaining action
	And Player1 should have 0 to spend
