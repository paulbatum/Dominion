Feature: Chancellor

Background:
	Given A new game with 3 players	
	And Player1 has a Chancellor in hand instead of a Copper
	When Player1 plays a Chancellor

Scenario: Chancellor is +2 money		
	Then Player1 should have 2 to spend	

Scenario: Player must choose whether to use Chancellor's effect
	Then Player1 must choose yes or no

Scenario: Player uses Chancellor's effect
	When Player1 chooses yes
	Then Player1 should have a deck of 0 cards
	And Player1 should have 5 cards in the discard pile

Scenario: Player does not use Chancellor's effect
	When Player1 chooses no
	Then Player1 should have a deck of 5 cards
	And Player1 should have 0 cards in the discard pile	
	
