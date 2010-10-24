Feature: Golem

Scenario: Play Golem but reveal no actions
	Given A new game with 3 players
	And Player1 has a Golem in hand instead of a Copper
	When Player1 plays a Golem
	Then All actions should be resolved

Scenario: Play Golem but reveal only one action
	Given A new game with 3 players
	And Player1 has a Golem in hand instead of a Copper
	And Player1 has a deck of Woodcutter, Estate, Copper, Copper, Copper
	When Player1 plays a Golem
	Then All actions should be resolved	
	And Player1 should have 2 to spend
	And Player1 should have in play: Golem, Woodcutter
	And Player1 should have a discard pile of Estate, Copper, Copper, Copper

Scenario: Play Golem and reveal two of the same action
	Given A new game with 3 players
	And Player1 has a Golem in hand instead of a Copper
	And Player1 has a deck of Village, Estate, Copper, Village, Copper, Copper
	When Player1 plays a Golem	
	Then Player1 should have 4 remaining actions
	Then Player1 should have 6 cards in hand
	And Player1 should have in play: Golem, Village, Village
	Then Player1 should have a discard pile of Estate, Copper

Scenario: Player reveals Golem when resolving Golem and the revealed Golem is ignored
	Given A new game with 3 players
	And Player1 has a Golem in hand instead of a Copper
	And Player1 has a deck of Village, Golem, Village, Copper, Copper
	When Player1 plays a Golem
	Then Player1 should have 4 remaining actions
	Then Player1 should have 6 cards in hand
	Then Player1 should have a Golem on top of the discard pile

Scenario: Player plays Golem, reveals two different actions and must choose which to play first
	Given A new game with 3 players
	And Player1 has a Golem in hand instead of a Copper
	And Player1 has a deck of Village, Estate, Copper, Militia, Copper, Copper
	When Player1 plays a Golem	
	Then Player1 must select a revealed card from: Village, Militia	
	And Player1 should have a discard pile of Estate, Copper	
	And Player1 should have in play: Golem

Scenario: Player plays Golem, reveals two different actions and the chosen action is resolved first
	Given A new game with 3 players
	And Player1 has a Golem in hand instead of a Copper
	And Player1 has a deck of Village, Estate, Copper, Nobles, Copper, Copper
	When Player1 plays a Golem
	When Player1 selects Nobles from the revealed cards
	Then Player1 must choose from DrawCards, GainActions	
	And Player1 should have in play: Golem, Nobles
	And Player1 should have 0 remaining actions
	And Player1 should have 4 cards in hand
	