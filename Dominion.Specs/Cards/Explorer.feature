Feature: Explorer

Scenario: Player must decide whether to reveal province after playing explorer
	Given A new game with 3 players
	And Player1 has a hand of Explorer, Province, Copper, Copper, Estate
	When Player1 plays an Explorer
	Then Player1 must choose whether to reveal a Province (Yes or No)

Scenario: Player reveals a province and gains a gold
	Given A new game with 3 players
	And Player1 has a hand of Explorer, Province, Copper, Copper, Estate
	When Player1 plays an Explorer
	And Player1 chooses to gain a Gold (Yes)
	Then Player1 should have a hand of Province, Copper, Copper, Estate, Gold

Scenario: Player chooses not to reveal a province and gains a silver
	Given A new game with 3 players
	And Player1 has a hand of Explorer, Province, Copper, Copper, Estate
	When Player1 plays an Explorer
	And Player1 chooses to gain a Silver (No)
	Then Player1 should have a hand of Province, Copper, Copper, Estate, Silver

Scenario: Player does not have a Province in hand and gains a silver
	Given A new game with 3 players
	And Player1 has a hand of Explorer, Estate, Copper, Copper, Estate
	When Player1 plays an Explorer
	Then All actions should be resolved
	And Player1 should have a hand of Estate, Copper, Copper, Estate, Silver







