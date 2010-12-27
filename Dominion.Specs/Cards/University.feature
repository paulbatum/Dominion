Feature: University

Scenario: Player plays University and must select an action card to gain 
	Given A new game with 3 players
	And Player1 has a University in hand instead of a Copper
	When Player1 plays a University
	Then Player1 may gain an action card of cost 5 or less
	And Player1 should have 2 remaining actions

Scenario: Player plays University and selects an action
	Given A new game with 3 players
	And Player1 has a University in hand instead of a Copper
	When Player1 plays a University
	And Player1 gains a Moat
	Then All actions should be resolved
	Then Player1 should have a Moat on top of the discard pile

Scenario: Player plays University and chooses not to gain
	Given A new game with 3 players
	And Player1 has a University in hand instead of a Copper
	When Player1 plays a University
	And Player1 gains nothing
	Then All actions should be resolved
