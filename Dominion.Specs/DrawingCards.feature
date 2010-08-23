Feature: Drawing Cards
	In order to have my turn
	As a dominion player
	I want to be able to draw cards

@mytag
Scenario: Draw a single card
	Given A game is in progress
	And I have 4 cards in hand	
	And My deck is not empty
	When I draw a card
	Then I should have 5 cards in hand
