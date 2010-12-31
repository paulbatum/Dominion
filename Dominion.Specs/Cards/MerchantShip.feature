Feature: Merchant Ship

Scenario: Player plays Merchant Ship and gains +2 coin immediately
	Given A new game with 2 players	
	And Player1 has a MerchantShip in hand instead of a Copper
	When Player1 plays a MerchantShip
	Then Player1 should have 2 to spend

Scenario: Player plays Merchant Ship and gains +2 coin on the following turn
	Given A new game with 2 players	
	And Player1 has a MerchantShip in hand instead of a Copper
	When Player1 plays a MerchantShip
	And Player1 ends their turn
	And Player2 ends their turn
	Then Player1 should have 2 to spend	