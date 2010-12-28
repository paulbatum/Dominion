Feature: Workshop

Scenario: Player must choose a card to gain
	Given A new game with 3 players	
	And Player1 has a Workshop in hand instead of a Copper
	When Player1 plays a Workshop
	Then Player1 must gain a card of cost 4 or less

Scenario: Player gains card
	Given A new game with 3 players	
	And Player1 has a Workshop in hand instead of a Copper
	When Player1 plays a Workshop
	When Player1 gains a Silver
	Then Player1 should have a Silver on top of the discard pile

@Hosted
Scenario: Valid choices for workshop gain are marked accordingly in view
	Given A new hosted game with 3 players			
	And Player1 has a Workshop in hand instead of a Copper
	When The game begins
	And Player1 tells the host to play Workshop	
	Then Player1's view includes a Silver in the bank that can be gained