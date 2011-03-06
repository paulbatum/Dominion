Feature: Upgrade

Scenario: Player must choose a card to Upgrade
	Given A new game with 3 players and bank of Caravan, Coppersmith, Militia, Envoy, Familiar, Moat, Remodel, Thief, Upgrade, Chapel
	And Player1 has a hand of Upgrade, Caravan, Estate, Copper, Copper
	When Player1 plays a Upgrade
	Then Player1 must select 1 card to Upgrade

Scenario: Upgraded card is trashed
	Given A new game with 3 players and bank of Caravan, Coppersmith, Militia, Envoy, Familiar, Moat, Remodel, Thief, Upgrade, Chapel
	And Player1 has a hand of Upgrade, Caravan, Estate, Copper, Copper
	When Player1 plays a Upgrade
	And Player1 selects a Caravan to Upgrade
	Then There should be a Caravan on top of the trash pile

Scenario: Player must Upgrade to an exact cost
	Given A new game with 3 players and bank of Caravan, Coppersmith, Militia, Envoy, Familiar, Moat, Remodel, Thief, Upgrade, Chapel
	And Player1 has a hand of Upgrade, Caravan, Estate, Copper, Copper
	When Player1 plays a Upgrade
	And Player1 selects a Caravan to Upgrade
	Then Player1 must gain a card of exact cost 5

Scenario: Player Upgrades a card with no Upgrade option
	Given A new game with 3 players and bank of Caravan, Coppersmith, Militia, Envoy, Familiar, Moat, Remodel, Thief, Upgrade, Chapel
	And Player1 has a hand of Upgrade, Caravan, Estate, Copper, Copper
	When Player1 plays a Upgrade
	And Player1 selects a Copper to Upgrade
	Then All actions should be resolved

Scenario: Player plays Upgrade with no cards in hand or deck
	Given A new game with 3 players 
	And Player1 has a hand of Upgrade
	And Player1 has an empty deck
	When Player1 plays a Upgrade	
	Then All actions should be resolved