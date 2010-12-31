Feature: Torturer

Scenario: Player plays Torturer and opponents must decide whether to discard cards or gain a curse
	Given A new game with 3 players	
	And Player1 has a Torturer in hand instead of a Copper
	When Player1 plays a Torturer
	Then Player1 should have 7 cards in hand
	And Player2 must choose whether to discard cards (Yes or No)
	And Player3 must choose whether to discard cards (Yes or No)

Scenario: Player plays Torturer and opponent decides to discard cards
	Given A new game with 2 players	
	And Player1 has a Torturer in hand instead of a Copper
	When Player1 plays a Torturer	
	And Player2 chooses to discard cards (Yes)
	Then Player2 must select 2 cards to discard
