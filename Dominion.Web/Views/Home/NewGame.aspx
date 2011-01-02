<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Dominion.Web.ViewModels.NewGameViewModel>" MasterPageFile="~/Views/Shared/site.Master" %>
<%@ Import Namespace="Dominion.Web.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
<script type="text/javascript">
    $(document).ready(function () {

        $('#numberOfPlayers')
            .change(function (event) {
                populateNames(event.target.value);
            });

        $('#numberOfPlayers').val(4);
        populateNames(4);

        checkIfSubmitAllowed();
    });

    function populateNames(numberOfPlayers) {
        var names = "Player1";
        for (var i = 2; i <= numberOfPlayers; i++)
            names += i % 2 == 0 ? ", SimpleAI" : ", BigMoneyAI"; 
        $('#Names')
            .val(names);
    }

    function checkIfSubmitAllowed() {
        $("#submitbutton").attr("disabled", $(".someCard:checked").length != 10);
    }
</script>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">     
    <form action="NewGame" method="post">       
        <%= Html.DropDownList("numberOfPlayers", 
            Enumerable.Range(1,6).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString()}),
            "Number of Players"   
            ) %>
        Player names: <%= Html.TextBox("Names", "", new { style = "width:250px" })%>
        <br />
        <br />
        <%= Html.EditorFor(x => x.UseProsperty) %> Use Prosperity cards (Platinum & Colony)
        <br />
        <br />        
        <% foreach (var cardName in Model.CardsToChooseFrom.OrderBy(c => c))
           {%>
           <div style="float:left; padding:5px;">
               <img style="height:200px" src="<%= ResolveUrl("~/content/images/cards/" + cardName) %>.jpg" />
               <br />
               <%--<%= Html.CheckBox("chosenCards", Model.ChosenCards.Contains(cardName), new { value = cardName, @class ="someCard", onclick = "checkIfSubmitAllowed()" })%>--%>
               <input type="checkbox" name="chosenCards" class="someCard" value="<%=cardName%>" onclick="checkIfSubmitAllowed()" <%= Model.ChosenCards.Contains(cardName) ? "checked=checked" : "" %> />
               <%= cardName%>
           </div>
        <% } %>
        <br style="clear:both" />
        <%= Html.SubmitButton("submitbutton", "New Game") %>
        <br />
    </form>
</asp:Content>
