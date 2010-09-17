<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/site.Master" %>
<%@ Import Namespace="Dominion.Web.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
<script type="text/javascript">
    $(document).ready( function() {
        $('#numberOfPlayers')
            .change( function(event) {
                populateNames(event.target.value);
            });
    });

    function populateNames(numberOfPlayers) {
        var names = "Player1";
        for(var i = 2; i <= numberOfPlayers; i++)
            names += ", Player" + i
        $('#Names')
            .val(names);
    }
</script>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">     
    <form action="NewGame" method="post">       
        <%= Html.DropDownList("numberOfPlayers", 
            Enumerable.Range(1,6).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString()}),
            "Number of Players"                    
            ) %>
        Player names: <%= Html.TextBox("Names", "", new { style="width:250px"}) %>
        <%= Html.SubmitButton("submitbutton", "New Game") %>
    </form>
</asp:Content>
