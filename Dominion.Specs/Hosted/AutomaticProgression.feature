Feature: Automatic Progression
	In order to speed up gameplay
	As a dominion player
	I want the game state to advance automatically when there are no possible actions

Scenario: Immediately move to buy step when no actions
	Given A new hosted game with 3 players				
	When The game begins
	Then Player1 should be in the buy step

Scenario: Allow player to play actions
	Given A new hosted game with 3 players			
	And Player1 has a Smithy in hand instead of a Copper
	When The game begins
	Then Player1 should have 1 remaining action

Scenario: Automatic progression when previous player uses their buys and current player has no actions
	Given A new hosted game with 3 players	
	When The game begins
	And Player1 tells the host to buy Copper
	Then Player2 should be in the buy step

Scenario: Automatic progression when using 1 of 2 buys
	Given A new hosted game with 3 players
	And Player1 has a Woodcutter in hand instead of a Copper	
	When The game begins
	And Player1 tells the host to play Woodcutter
	And Player1 tells the host to buy Copper
	Then Player1 should be in the buy step

Scenario: Automatic progression when using 2 of 2 buys
	Given A new hosted game with 3 players	
	And Player1 has a Woodcutter in hand instead of a Copper	
	When The game begins
	And Player1 tells the host to play Woodcutter
	And Player1 tells the host to buy Copper
	And Player1 tells the host to buy Copper
	Then Player2 should be in the buy step

Scenario: Automatic progression at the game end
	Given A new hosted game with 3 players
	But There is only 1 Province left	
	And Player1 has 5 Gold in hand	
	When The game begins
	And Player1 tells the host to buy Province	
	Then The game should have ended
	And Player1 should be the winner

Scenario: Automatic progression when an effect is in progress
	Given A new hosted game with 3 players		
	And Player1 has a Militia in hand instead of a Copper	
	When The game begins
	And Player1 tells the host to play Militia
	Then Player1 should be in the action step
