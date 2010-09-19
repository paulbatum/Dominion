<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/site.Master" %>
<%@ Import Namespace="Dominion.Web.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">

        var actions = {
            buy: function (event) {
                var data = $.tmplItem(event.target).data;
                $.post('BuyCard', { id: data.Id }, handleInteractionResponse);
                $(event.target).effect('transfer', { to: '#discards .card' }, 200);
            },
            play: function (event) {
                var data = $.tmplItem(event.target).data;
                $.post('PlayCard', { id: data.Id }, handleInteractionResponse);
                $(event.target).effect('transfer', { to: '.playAreaTransferTarget' }, 200);
            },
            selectFixedNumberOfCards: function (event, activity) {
                $(event.target).toggleClass('selectedCard');

                var ids = $('#hand .selectedCard')
                            .get()
                            .map($.tmplItem)
                            .map(function (tmpl) { return tmpl.data.Id });

                if (ids.length == activity.Properties.NumberOfCardsToSelect) {
                    $.post('SelectCards', { ids: ids }, handleInteractionResponse);
                }
            },
            makeYesNoChoice: function (choice) {
                $.post('MakeYesNoChoice', { choice: choice }, handleInteractionResponse);
            },
            selectPile: function (event, activity) {
                var data = $.tmplItem(event.target).data;
                $.post('SelectPile', { id: data.Id }, handleInteractionResponse);
            },
            chat: function (text) {
                if (text != "") {
                    $.post('Chat', { message: text }, handleInteractionResponse);
                }
            }
        };

        var controller = {};

        var version = 0;

        $(document).ready(function () {
            jQuery.ajaxSettings.traditional = true;


            createLayout();

            $('#chat').hide();

            $(document).bind('keydown', 'return', function () {
                $('#chat').show();
                $('#chatBox').focus();
            });

            $('#chatBox').bind('keydown', 'return', function (event) {
                event.stopPropagation();
                actions.chat($('#chatBox').val());
                $('#chat').hide();
                $('#chatBox').text('');
            });


            setupHover();
            bindDefaultClickEvents();

            loadGame();
            doComet();
            doChatComet();

            bindCommands();
        });

        function bindActivity(activity) {
            controller.HandClick = function (event) { };
            controller.BankClick = function (event) { };

            if (activity.Type == "SelectFixedNumberOfCards") {
                controller.HandClick = function (event) { actions.selectFixedNumberOfCards(event, activity); };
            }

            if (activity.Type == "MakeYesNoChoice") {
                $('#yesChoice').show();
                $('#noChoice').show();
            }
            else {
                $('#yesChoice').hide();
                $('#noChoice').hide();
            }

            if (activity.Type == "SelectPile") {
                controller.BankClick = function (event) { actions.selectPile(event, activity); };
            }

        }

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

            if (version == data.Version)
                return;
            else
                version = data.Version;

            updateSection('#bank', data.Bank, '#cardpileTemplate');
            updateSection('#hand', data.Hand, '#cardTemplate');
            updateSection('#status', data.Status, '#statusTemplate');
            updateSection('#playArea', data.InPlay, '#cardTemplate');
            updateSection('#deck', data.Deck, '#deckTemplate');
            updateSection('#discards', data.Discards, '#discardpileTemplate');


            if (data.PendingActivity) {
                bindActivity(data.PendingActivity)
                $('#prompt').show()
                $('#message').text(data.PendingActivity.Message);
            }
            else {
                bindDefaultClickEvents();
                $('#prompt').hide();
            }

            $('#playArea')
                .append($('<div>').addClass('playAreaTransferTarget'));

            $('#log')
                .html(data.Log)                
                .animate({ scrollTop: $('#log').attr("scrollHeight") - $('#log').height() }, 1000);
        }

        function bindDefaultClickEvents() {
            controller.HandClick = actions.play;
            controller.BankClick = actions.buy;
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

        function doChatComet() {
            $.ajax({
                url: 'chatloop',
                complete: doChatComet,
                success: updateChat,                
                cache: false
            });
        }

        function updateChat(data) {
            alert(data.message);
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

            $('#middle').layout({
                defaults: defaults,

                south: {
                    initClosed: true
                }
            });

            $('#bottom').layout({
                defaults: defaults,

                south: {
                    initClosed: true
                }
            });            
        }       

        function updateSection(sectionSelector, data, templateSelector) {
            $(sectionSelector)
                .html($(templateSelector).tmpl(data));
        }

        function bindCommands() {
            $('#hand .card').live('click', function (event) { controller.HandClick(event); });
            $('#bank .cardpile').live('click', function (event) { controller.BankClick(event); });

            $('#yesChoice')
                    .click(function () { actions.makeYesNoChoice(true); })
                    .button();

            $('#noChoice')
                    .click(function () { actions.makeYesNoChoice(false); })
                    .button();

            $('form').ajaxForm(handleInteractionResponse);

            $('#doBuys').button();
            $('#endTurn').button();
        }

        function setupHover() {
            $('.cardpile img').thumbPopup({
                imgSmallFlag: "",
                imgLargeFlag: "",
                cursorTopOffset: -50,
                cursorLeftOffset: 15
            });
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
        <div id="middle" class="ui-layout-center">
            <div id="playArea" class="ui-layout-center container"></div>
            <div id="prompt" class="ui-layout-south container">
                <div id="message"></div>
                <input id="noChoice" type="submit" class="promptButton" value="No" />
                <input id="yesChoice" type="submit" class="promptButton" value="Yes" />                
            </div>
        </div>
        <div id="log" class="ui-layout-east container" style="overflow-y:scroll"></div>
    </div>        
    <div id="bottom" class="ui-layout-south">        
        <div id="deck" class="ui-layout-west"></div>
        <div id="hand" class="ui-layout-center container "></div>
        <div id="discards" class="ui-layout-east"></div>
        <div id="chat" class="ui-layout-south">
            <input id="chatBox" type="text" />
        </div>
    </div>    
</asp:Content>
