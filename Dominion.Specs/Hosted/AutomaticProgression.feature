Feature: Automatic Progression
	In order to speed up gameplay
	As a dominion player
	I want the game state to advance automatically when there are no possible actions

Scenario: Immediately move to buy step when no actions
	Given A new hosted game with 3 players			
	And I am going first
	When The game begins
	Then I should be in my buy step

Scenario: Allow player to play actions
	Given A new hosted game with 3 players			
	And I am going first
	And I have a Smithy in hand instead of a Copper
	When The game begins
	Then I should have 1 remaining action

Scenario: Immediately move to buy step when previous player uses their buys and current player has no actions
	Given A new hosted game with 3 players
	And I am going first
	When The game begins
	And Player1 tells the host to buy Copper
	Then Player2 should be in the buy step

Scenario: Automatic progression with multiple buys
	Given A new hosted game with 3 players
	And I am going first
	And I have a Woodcutter in hand instead of a Copper	
	When The game begins
	And Player1 tells the host to play Woodcutter
	And Player1 tells the host to buy Copper
	Then Player1 should be in the buy step