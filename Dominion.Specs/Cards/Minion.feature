Feature: Minion
	
Scenario: Player plays Minion and must choose whether to discard his hand
	Given A new game with 3 players	
	And Player1 has a Minion in hand instead of a Copper
	When Player1 plays a Minion
	Then Player1 must choose whether to discard his hand (Yes or No)	
	And Player1 should have 1 action remaining

Scenario: Player plays Minion and chooses to gain 2 spend
	Given A new game with 3 players	
	And Player1 has a Minion in hand instead of a Copper
	When Player1 plays a Minion
	And Player1 chooses to gain 2 spend (No)
	Then Player1 should have 2 to spend
	And All actions should be resolved

Scenario: Player plays Minion and chooses to discard his hand, all other players must discard their hands
	Given A new game with 3 players	
	And Player1 has a Minion in hand instead of a Copper
	When Player1 plays a Minion
	And Player1 chooses to discard his hand (Yes)
	Then Player1 should have 4 cards in hand	
	And Player2 should have 4 cards in hand	
	And Player3 should have 4 cards in hand
	And Player1 should have 4 cards in the discard pile
	And Player2 should have 5 cards in the discard pile
	And Player3 should have 5 cards in the discard pile
	And All actions should be resolved

Scenario: Player plays Minion and chooses to discard his hand but the other player only has 4 cards in hand
	Given A new game with 2 players	
	And Player1 has a Minion in hand instead of a Copper
	And Player2 has a hand of Silver, Silver, Silver, Silver
	When Player1 plays a Minion
	And Player1 chooses to discard his hand (Yes)
	Then Player1 should have 4 cards in hand	
	And Player1 should have 4 cards in the discard pile
	And Player2 should have a hand of Silver, Silver, Silver, Silver

Scenario: Player plays Minion and opponents must choose whether to Moat
	Given A new game with 3 players	
	And Player1 has a Minion in hand instead of a Copper
	And Player2 has a Moat in hand instead of a Copper
	When Player1 plays a Minion
	Then Player2 may reveal a reaction

Scenario: Player plays Minion, opponent moats and then player must choose whether to discard
	Given A new game with 3 players	
	And Player1 has a Minion in hand instead of a Copper
	And Player2 has a Moat in hand instead of a Copper
	When Player1 plays a Minion
	And Player2 reveals Moat
	And Player2 is done with reactions
	Then Player1 must choose whether to discard his hand (Yes or No)

Scenario: Player plays Minion, opponent moats and then player chooses to discard anyway
	Given A new game with 3 players	
	And Player1 has a Minion in hand instead of a Copper
	And Player2 has a Moat in hand instead of a Copper
	When Player1 plays a Minion
	And Player2 reveals Moat
	And Player2 is done with reactions
	And Player1 chooses to discard his hand (Yes)
	Then Player1 should have 4 cards in hand	
	And Player2 should have 5 cards in hand	
	And Player1 should have 4 cards in the discard pile
	And Player2 should have 0 cards in the discard pile	
	And All actions should be resolved
