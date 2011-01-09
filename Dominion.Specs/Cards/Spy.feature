Feature: Spy

Scenario: Player plays Spy, draws a card, gains an action and then must choose whether cards are discarded
	Given A new game with 2 players
	And Player1 has a Spy in hand instead of a Copper
	And Player2 has a deck of Estate, Copper, Estate, Copper, Copper
	When Player1 plays a Spy	
	Then Estate should be revealed to Player1
	And Player1 must choose whether to make Player2 discard it (Yes or No)
	And Player1 should have 5 cards in hand
	And Player1 should have 1 remaining action

Scenario: Player plays Spy but other player nullifies with moat - player chooses whether his own card is discarded
	Given A new game with 2 players
	And Player1 has a Spy in hand instead of a Copper
	And Player2 has a Moat in hand instead of a Copper
	And Player1 has a deck of Copper, Silver, Estate, Copper, Copper
	When Player1 plays a Spy	
	And Player2 reveals Moat
	And Player2 is done with reactions
	Then Silver should be revealed to Player1
	And Player1 must choose whether to discard it (Yes or No)

Scenario: Player plays Spy, makes opponent discard the revealed card and then keeps his own card
	Given A new game with 2 players
	And Player1 has a Spy in hand instead of a Copper
	And Player1 has a deck of Copper, Silver, Estate, Copper, Copper
	And Player2 has a deck of Estate, Copper, Estate, Copper, Copper
	When Player1 plays a Spy
	And Player1 chooses to make Player2 discard the Estate (Yes)
	Then Player2 should have an Estate on top of the discard pile	
	And Silver should be revealed to Player1
	When Player1 chooses to keep it (No)
	Then Player1 should have a deck of: Silver, Estate, Copper, Copper

Scenario: Player plays Spy, neither he nor his opponent have any deck (or discards)
	Given A new game with 2 players
	And Player1 has a Spy in hand instead of a Copper
	And Player1 has an empty deck
	And Player2 has an empty deck
	When Player1 plays a Spy
	Then All actions should be resolved
