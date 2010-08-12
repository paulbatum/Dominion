<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Dominion.Web.ViewModels.GameViewModel>" MasterPageFile="~/Views/Shared/site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">

        $(document).ready(function () {
            loadGame();
            bindHand();
            bindBank();
            bindCommands();
        });

        function loadGame() {
            $.getJSON('GameData', {}, updateGameState);
        }

        function updateGameState(data) {
            updateBank(data.Bank);
            updateHand(data.Hand);
            updateStatus(data.Status);
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

        function bindHand() {
            $('#hand .card')
                .live('click', function (e) {
                    var data = $.tmplItem(e.target).data;
                    $.post('PlayCard', { id: data.Id }, function (response) {
                        if (response.Success)
                            updateGameState(response.GameState);
                        else
                            alert(response.ErrorMessage);
                    });
                });
        }

        function bindBank() {
            $('#bank .cardpile')
                .live('click', function (e) {
                    var data = $.tmplItem(e.target).data;
                    $.post('BuyCard', { id: data.Id }, function (response) {
                        if (response.Success)
                            updateGameState(response.GameState);
                        else
                            alert(response.ErrorMessage);
                    });
                });
        }

        function bindCommands() {
            $('form').ajaxForm(function (response) {
                if (response.Success)
                    updateGameState(response.GameState);
                else
                    alert(response.ErrorMessage);
            });

            $('#commands')
                .buttonset();
        }

        

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
    <div id="bank"></div>
    <div id="main">        
        <div id="display">
            <div id="status" class="container"></div>
            <div id="commands" class="container">
                <form id="buyForm" action="DoBuys" method="post">
                    <input type="submit" value="Move to buy step" />
                </form>
                <form id="endTurnForm" action="EndTurn" method="post">
                    <input type="submit" value="End turn"/>
                </form>
            </div>
        </div>        
        <div id="playArea"></div>
    </div>        
    <div id="hand"></div>
</asp:Content>
