Feature: Masquerade

Scenario: Players are expected to choose a card to pass when Masquerade is played
	Given A new game with 3 players	
	And Player1 has a Masquerade in hand instead of a Copper
	When Player1 plays a Masquerade
	Then Player1 should have 6 cards in hand
	And Player1 must select a card to pass
	And Player2 must select a card to pass
	And Player3 must select a card to pass	

Scenario: Cards are not exchanged until all players have made their selections
	Given A new game with 3 players	
	And Player1 has a hand of Masquerade, Curse, Curse, Curse, Curse
	And Player1 has a deck of Curse, Curse, Curse, Curse, Curse
	And Player2 has a hand of Estate, Estate, Estate, Estate, Estate
	And Player3 has a hand of Silver, Silver, Silver, Silver, Silver
	When Player1 plays a Masquerade
	And Player1 selects a Curse to pass	
	And Player3 selects a Silver to pass
	Then Player1 should have a hand of Curse, Curse, Curse, Curse, Curse, Curse	
	And Player2 should have a hand of Estate, Estate, Estate, Estate, Estate
	And Player3 should have a hand of Silver, Silver, Silver, Silver, Silver

Scenario: Cards are exchanged once all players have made their selections
	Given A new game with 3 players	
	And Player1 has a hand of Masquerade, Curse, Curse, Curse, Curse
	And Player1 has a deck of Curse, Curse, Curse, Curse, Curse
	And Player2 has a hand of Estate, Estate, Estate, Estate, Estate
	And Player3 has a hand of Silver, Silver, Silver, Silver, Silver
	When Player1 plays a Masquerade
	And Player1 selects a Curse to pass
	And Player2 selects a Estate to pass
	And Player3 selects a Silver to pass
	Then Player1 should have a hand of Curse, Curse, Curse, Curse, Curse, Silver
	Then Player2 should have a hand of Estate, Estate, Estate, Estate, Curse
	Then Player3 should have a hand of Silver, Silver, Silver, Silver, Estate
	Then Player1 may select up to 1 card from their hand to trash

Scenario: Player trashes a card
	Given A new game with 3 players	
	And Player1 has a hand of Masquerade, Curse, Curse, Copper, Copper
	And Player1 has a deck of Copper, Copper, Copper, Copper, Copper
	And Player2 has a hand of Estate, Estate, Estate, Estate, Estate
	And Player3 has a hand of Silver, Silver, Silver, Silver, Silver
	When Player1 plays a Masquerade
	And Player1 selects a Curse to pass
	And Player2 selects a Estate to pass
	And Player3 selects a Silver to pass
	And Player1 selects a Curse to trash
	Then Player1 should have a hand of Copper, Copper, Copper, Copper, Silver
	Then Player2 should have a hand of Estate, Estate, Estate, Estate, Curse
	Then Player3 should have a hand of Silver, Silver, Silver, Silver, Estate
	Then All actions should be resolved
