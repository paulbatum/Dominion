Feature: Chancellor

Background:
	Given A new game with 3 players	
	And Player1 has a Chancellor in hand instead of a Copper	

Scenario: Chancellor is +2 money		
	When Player1 plays a Chancellor
	Then Player1 should have 2 to spend	

Scenario: Player may choose whether to use Chancellor's effect
	When Player1 plays a Chancellor
	Then Player1 must choose whether to use Chancellor's effect (Yes or No)

Scenario: Player uses Chancellor's effect
	When Player1 plays a Chancellor
	When Player1 chooses to move their deck to the discard pile (Yes)
	Then Player1 should have a deck of 0 cards
	And Player1 should have 5 cards in the discard pile

Scenario: Player does not use Chancellor's effect
	When Player1 plays a Chancellor
	When Player1 chooses to leave their deck as is (No)
	Then Player1 should have a deck of 5 cards
	And Player1 should have 0 cards in the discard pile	
	
