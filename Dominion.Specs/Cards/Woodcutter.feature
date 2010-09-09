Feature: Woodcutter

Background:
	Given A new game with 3 players
	And I am going first
	And I have a Woodcutter in hand instead of a Copper

Scenario: Woodcutter grants extra buy	
	When I play Woodcutter
	Then I should have 2 buys

Scenario: Woodcutter is +2 money	
	When I play Woodcutter
	Then I should have 2 to spend	