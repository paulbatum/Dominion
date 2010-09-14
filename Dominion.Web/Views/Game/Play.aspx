<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/site.Master" %>
<%@ Import Namespace="Dominion.Web.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">

        $(document).ready(function () {
            createLayout();
            loadGame();
            doComet();
            bindHand();
            bindBank();
            bindCommands();

        });

        function loadGame() {            
            $.ajax({
                url: 'GameData',
                dataType: 'json',
                data: {},
                success: updateGameState,
                async: false
            });
        }

        function updateGameState(data) {            
            updateSection('#bank', data.Bank, '#cardpileTemplate');
            updateSection('#hand', data.Hand, '#cardTemplate');
            updateSection('#status', data.Status, '#statusTemplate');
            updateSection('#playArea', data.InPlay, '#cardTemplate');
            updateSection('#deck', data.Deck, '#deckTemplate');
            updateSection('#discards', data.Discards, '#discardpileTemplate');

            $('#playArea')
                .append($('<div>').addClass('playAreaTransferTarget'));


            $('#log')
                .html(data.Log)                
                .animate({ scrollTop: $('#log').attr("scrollHeight") - $('#log').height() }, 1000);
        }
        function doComet() {            
            $.ajax({
                url: 'gamestateloop',
                complete: doComet,
                success: updateGameState,
                error: loadGame,
                cache: false
            });
        }

        function createLayout() {
            var defaults = {
                resizable: false,
                spacing_open: 0,
                autoResize: true
            };

            $('body').layout({
                defaults: defaults,
                north: {                    
                    size: '40%'
                },
                center: {                    
                    size: '30%'
                },
                south: {                    
                    size: '30%'
                }
            });

            $('#main').layout({
                defaults: defaults,
                east: {
                    size:'20%'
                },     
                center: {
                    size:'60%'
                }, 
                west: {
                    size:'20%'            
                }                         
            });

            $('#bottom').layout({
                defaults: defaults
            });
        }       

        function updateSection(sectionSelector, data, templateSelector) {
            $(sectionSelector)
                .html($(templateSelector).tmpl(data));
        }

        function bindHand() {
            $('#hand .card')
                .live('click', function (e) {
                    var data = $.tmplItem(e.target).data;
                    $.post('PlayCard', { id: data.Id }, handleInteractionResponse);
                    $(this).effect('transfer', { to: '.playAreaTransferTarget' }, 200);
                });
        }

        function bindBank() {
            $('#bank .cardpile')
                .live('click', function (e) {
                    var data = $.tmplItem(e.target).data;
                    $.post('BuyCard', { id: data.Id }, handleInteractionResponse);
                    $(this).effect('transfer', { to: '#discards .card' }, 200);                    
                });
        }

        function bindCommands() {
            $('form').ajaxForm(handleInteractionResponse);

            $('#doBuys').button();
            $('#endTurn').button();
        }

        function handleInteractionResponse(response) {
            
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
                    <input id="doBuys" type="submit" class="small-button" value="Do Buys" />
                </form>
                <form id="endTurnForm" action="EndTurn" method="post">
                    <input id="endTurn" type="submit" class="small-button" value="End Turn"/>
                </form>
            </div>
        </div>        
        <div id="playArea" class="ui-layout-center container"></div>
        <div id="log" class="ui-layout-east container" style="overflow-y:scroll"></div>
    </div>        
    <div id="bottom" class="ui-layout-south">        
        <div id="deck" class="ui-layout-west"></div>
        <div id="hand" class="ui-layout-center container "></div>
        <div id="discards" class="ui-layout-east"></div>
    </div>    
</asp:Content>
