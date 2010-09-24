<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/site.Master" %>
<%@ Import Namespace="Dominion.Web.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
    <%= Html.Script("play.js") %>
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
                <td>Actions: </td>
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
            <div id="chatLog" class="ui-layout-east container log"></div>
            <div id="prompt" class="ui-layout-south container">
                <div id="message"></div>
                <input id="noChoice" type="submit" class="promptButton" value="No" />
                <input id="yesChoice" type="submit" class="promptButton" value="Yes" />                
                <input id="doneChoice" type="submit" class="promptButton" value="Done" />      
            </div>
        </div>
        <div id="log" class="ui-layout-east container log"></div>
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
