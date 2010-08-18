<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Dominion.Web.ViewModels.GameViewModel>" MasterPageFile="~/Views/Shared/site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">

        $(document).ready(function () {
            createLayout();


            loadGame();
            bindHand();
            bindBank();
            bindCommands();
        });

        function createLayout() {
            var defaults = {
                resizable: false,
                spacing_open: 0,
                autoResize: true
            };

            $('body').layout({
                defaults: defaults,
                north: {                    
                    size: '33%'
                },
                south: {                    
                    size: '33%'
                },
                center: {                    
                    size: '33%'
                }
            });

            $('#main').layout({
                defaults: defaults,
                east: {
                    minSize: 100
                }
            });

            $('#bottom').layout({
                defaults: defaults,

            });
        }

        function loadGame() {
            $.getJSON('GameData', {}, updateGameState);
        }

        function updateGameState(data) {
            updateBank(data.Bank);
            updateHand(data.Hand);
            updateStatus(data.Status);
            updateInPlay(data.InPlay);
            updateDeck(data.Deck);
            updateSection('#discards', data.Discards, '#discardpileTemplate');
        }

        function updateSection(sectionSelector, data, templateSelector) {
            $(sectionSelector)
                .html($(templateSelector).tmpl(data));
        }

        function updateBank(data) {
            $('#bank')
                .html(
                    $('#cardpileTemplate').tmpl(data)
                );
        }

        function updateHand(data) {
            $('#hand')
                .html(
                    $('#cardTemplate').tmpl(data)
                );
        }

        function updateStatus(data) {
            $('#status')
                .html(
                    $('#statusTemplate').tmpl(data)
                );
        }

        function updateInPlay(data) {
            $('#playArea')
                .html(
                    $('#cardTemplate').tmpl(data)
                );
        }

        function updateDeck(data) {            
            $('#deck')
                .html(
                    $('#deckTemplate').tmpl(data)
                );
        }

        function bindHand() {
            $('#hand .card')
                .live('click', function (e) {
                    var data = $.tmplItem(e.target).data;
                    $.post('PlayCard', { id: data.Id }, function (response) {
                        updateGameState(response.GameState);                        
                    });
                });
        }

        function bindBank() {
            $('#bank .cardpile')
                .live('click', function (e) {
                    var data = $.tmplItem(e.target).data;
                    $.post('BuyCard', { id: data.Id }, function (response) {
                        updateGameState(response.GameState);                        
                    });
                });
        }

        function bindCommands() {
            $('form').ajaxForm(function (response) {
                updateGameState(response.GameState);                
            });

            $('#commands')
                .buttonset();
        }

        

    </script>
    <script id="discardpileTemplate" type="text/html">
        <div class="card">                   
            <img src="${ImageUrl}" />   
            <div>
                Discards (${CountDescription})                        
            </div>
        </div>
    </script>
    <script id="deckTemplate" type="text/html">
        <div class="card">                
            <img src="${ImageUrl}" />   
            <div>
                Deck (${CountDescription})                        
            </div>            
        </div>
    </script>
    <script id="cardpileTemplate" type="text/html">
        <div class="cardpile">            
            <img src="${ImageUrl}" />   
            <div>
                (${CountDescription})                        
            </div>
        </div>
    </script>
    <script id="cardTemplate" type="text/html">
        <div class="card">            
            <img src="${ImageUrl}" />
        </div>
    </script>
    <script id="statusTemplate" type="text/html">
        <table>
            <tr>
                <td>Actions remaining: </td>
                <td>${RemainingActions}</td>
            </tr>
            <tr>
                <td>Buy of: </td>
                <td>${MoneyToSpend}</td>
            </tr>
            <tr>
                <td>Buys: </td>
                <td>${BuyCount}</td>
            </tr>
        </table>
    </script>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div id="bank" class="ui-layout-north container"></div>
    <div id="main" class="ui-layout-center">        
        <div id="display" class="ui-layout-west">
            <div id="status" class="container"></div>
            <div id="commands" class="container">
                <form id="buyForm" action="DoBuys" method="post">
                    <input type="submit" value="Do Buys" />
                </form>
                <form id="endTurnForm" action="EndTurn" method="post">
                    <input type="submit" value="End Turn"/>
                </form>
            </div>
        </div>        
        <div id="playArea" class="ui-layout-center container"></div>
    </div>        
    <div id="bottom" class="ui-layout-south">        
        <div id="deck" class="ui-layout-west"></div>
        <div id="hand" class="ui-layout-center container "></div>
        <div id="discards" class="ui-layout-east"></div>
    </div>    
</asp:Content>
