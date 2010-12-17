Feature: Swindler
	
Scenario: Player plays Swindler and must choose what card the opponent gains
	Given A new game with 2 players and bank of Familiar, Militia, Smithy, Village, Cellar, Swindler, Market, Festival, Witch, Moat
	And Player1 has a Swindler in hand instead of a Copper
	And Player2 has a deck of Estate, Copper, Estate, Copper, Copper
	When Player1 plays a Swindler
	Then Player1 must select a card of cost 2 for Player2 to gain

Scenario: Player plays Swindler and select a card for the opponent to gain
	Given A new game with 2 players and bank of Familiar, Militia, Smithy, Village, Cellar, Swindler, Market, Festival, Witch, Moat
	And Player1 has a Swindler in hand instead of a Copper
	And Player2 has a deck of Moat, Copper, Estate, Copper, Copper
	When Player1 plays a Swindler
	And Player1 selects a Estate for Player2 to gain
	Then There should be a Moat on top of the trash pile
	And Player2 should have a Estate on top of the discard pile
	And Player2 should have a deck of: Copper, Estate, Copper, Copper

Scenario: Player plays Swindler but there is only one possible card to gain
	Given A new game with 2 players and bank of Familiar, Militia, Smithy, Village, Cellar, Swindler, Market, Festival, Witch, Moat
	And Player1 has a Swindler in hand instead of a Copper
	And Player2 has a deck of Familiar, Copper, Estate, Copper, Copper
	When Player1 plays a Swindler
	Then There should be a Familiar on top of the trash pile
	And Player2 should have a Familiar on top of the discard pile
	And Player2 should have a deck of: Copper, Estate, Copper, Copper
		
Scenario: Player plays Swindler but opponent has no deck so it is just +2 money
	Given A new game with 2 players and bank of Familiar, Militia, Smithy, Village, Cellar, Swindler, Market, Festival, Witch, Moat	
	And Player1 has a Swindler in hand instead of a Copper
	And Player2 has an empty deck
	When Player1 plays a Swindler
	Then Player1 should have 2 to spend
	And All actions should be resolved

Scenario: Player plays Swindler but there are no cards of appropriate cost to gain
	Given A new game with 2 players and bank of Familiar, Militia, Smithy, Village, Cellar, Swindler, Market, Festival, Witch, Moat	
	And Player1 has a Swindler in hand instead of a Copper
	And Player2 has a deck of Golem, Copper, Estate, Copper, Copper
	When Player1 plays a Swindler
	Then There should be a Golem on top of the trash pile
	And Player2 should have 0 cards in the discard pile
	And Player2 should have a deck of: Copper, Estate, Copper, Copper

Scenario: Player plays Swindler with multiple opponents
	Given A new game with 3 players and bank of Familiar, Militia, Smithy, Village, Cellar, Swindler, Market, Festival, Witch, Moat	
	And Player1 has a Swindler in hand instead of a Copper
	And Player2 has a deck of Copper, Copper, Estate, Copper, Copper
	And Player3 has a deck of Copper, Copper, Estate, Copper, Copper
	When Player1 plays a Swindler
	And Player1 selects a Curse for Player2 to gain	
	Then Player1 must select a card of cost 0 for Player3 to gain

@Hosted
Scenario: Information on Swindle
	Given A new hosted game with 2 players		
	And Player1 has a Swindler in hand instead of a Copper	
	When The game begins
	And Player1 tells the host to play Swindler	
	Then Player1's current activity should have a type of SelectPile 
	Then Player1's current activity should have a hint of OpponentGainCards
	Then Player1's current activity should have a source of Swindler