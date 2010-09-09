Feature: Automatic Progression
	In order to speed up gameplay
	As a dominion player
	I want the game state to advance automatically when there are no possible actions

Scenario: Immediately move to buy step when no actions
	Given A new hosted game with 3 players			
	And It is my turn
	Then I should be in my buy step

Scenario: Allow player to play actions
	Given A new hosted game with 3 players			
	And It is my turn
	And I have a Smithy in hand instead of a Copper
	And The game has begun
	Then I should have 1 remaining action