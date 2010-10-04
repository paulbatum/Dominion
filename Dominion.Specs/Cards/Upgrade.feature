Feature: Upgrade

Background:
	Given A new game with 3 players and bank of Caravan, Coppersmith, Militia, Envoy, Familiar, Moat, Remodel, Thief, Upgrade, Chapel
	And Player1 has a hand of Upgrade, Caravan, Estate, Copper, Copper
	When Player1 plays a Upgrade

Scenario: Player must choose a card to Upgrade
	Then Player1 must select 1 card to Upgrade

Scenario: Upgraded card is trashed
	And Player1 selects a Caravan to Upgrade
	Then There should be a Caravan on top of the trash pile

Scenario: Player must Upgrade to an exact cost
	And Player1 selects a Caravan to Upgrade
	Then Player1 must gain a card of exact cost 5

Scenario: Player Upgrades a card with no Upgrade option
	And Player1 selects a Copper to Upgrade
	Then All actions should be resolved
