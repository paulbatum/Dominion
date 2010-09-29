Feature: Thief

Scenario: Player plays thief but opponent has no treasure
	Given A new game with 2 players
	And Player1 has a Thief in hand instead of a Copper
	And Player2 has a deck of Estate, Estate, Copper, Copper, Copper
	When Player1 plays a Thief
	Then Player2 should have 2 cards in the discard pile
	And All actions should be resolved

Scenario: Player plays thief and opponent has one treasure
	Given A new game with 2 players
	And Player1 has a Thief in hand instead of a Copper
	And Player2 has a deck of Estate, Copper, Estate, Copper, Copper
	When Player1 plays a Thief
	Then Player1 must choose whether to gain the Copper (Yes or No)

Scenario: Player plays thief, opponent has one treasure and player gains it
	Given A new game with 2 players
	And Player1 has a Thief in hand instead of a Copper
	And Player2 has a deck of Estate, Copper, Estate, Copper, Copper
	When Player1 plays a Thief
	And Player1 chooses to gain the Copper (Yes)
	Then Player1 should have a Copper on top of the discard pile
	Then Player2 should have a Estate on top of the discard pile
	And All actions should be resolved

Scenario: Player plays thief, opponent has one treasure and player trashes it
	Given A new game with 2 players
	And Player1 has a Thief in hand instead of a Copper
	And Player2 has a deck of Estate, Copper, Estate, Copper, Copper
	When Player1 plays a Thief
	And Player1 chooses to trash the Copper (No)
	Then There should be a Copper on top of the trash pile
	Then Player2 should have a Estate on top of the discard pile
	And All actions should be resolved

Scenario: Player plays thief, opponent has two treasure
	Given A new game with 2 players
	And Player1 has a Thief in hand instead of a Copper
	And Player2 has a deck of Silver, Copper, Estate, Copper, Copper
	When Player1 plays a Thief
	Then Player1 must select a revealed card from: Silver, Copper

Scenario: Player plays thief, opponent has two treasure, player chooses one
	Given A new game with 2 players
	And Player1 has a Thief in hand instead of a Copper
	And Player2 has a deck of Silver, Copper, Estate, Copper, Copper
	When Player1 plays a Thief
	When Player1 selects Silver from the revealed cards
	Then Player1 must choose whether to gain the Silver (Yes or No)

Scenario: Player plays thief, multiple opponents have treasure
	Given A new game with 3 players
	And Player1 has a Thief in hand instead of a Copper
	And Player2 has a deck of Silver, Copper, Estate, Copper, Copper
	And Player3 has a deck of Gold, Copper, Estate, Copper, Copper
	When Player1 plays a Thief
	And Player1 selects Silver from the revealed cards
	And Player1 chooses to gain the Silver (Yes)
	Then Player1 must select a revealed card from: Gold, Copper