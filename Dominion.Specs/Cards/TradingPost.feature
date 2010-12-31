Feature: Trading Post

Scenario: Player plays trading post and must choose what to trash
Given A new game with 3 players
	And Player1 has a hand of TradingPost, Estate, Estate, Copper, Copper
	When Player1 plays a TradingPost
	Then Player1 must select 2 cards to trash

Scenario: Player plays trading post and trashes a copper and an estate, gaining a silver
	Given A new game with 3 players
	And Player1 has a hand of TradingPost, Estate, Estate, Copper, Copper
	When Player1 plays a TradingPost
	And Player1 selects cards [Estate, Copper] to trash
	Then The trash pile should be Estate, Copper
	And Player1 should have a hand of Estate, Copper, Silver

Scenario: Player plays trading post with only one other card in hand - it is trashed and no silver is gained
	Given A new game with 3 players
	And Player1 has a hand of TradingPost, Estate
	When Player1 plays a TradingPost	
	Then All actions should be resolved
	And The trash pile should be Estate
	And Player1 should have 0 cards in hand

Scenario: Player plays trading post with no other cards in hand
	Given A new game with 3 players
	And Player1 has a hand of TradingPost
	When Player1 plays a TradingPost	
	Then All actions should be resolved	
