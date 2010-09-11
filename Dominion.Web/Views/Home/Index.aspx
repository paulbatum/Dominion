<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/site.Master" %>
<%@ Import Namespace="Dominion.Web.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">     
    <form action="Home/NewGame" method="post">       
        <%= Html.DropDownList("numberOfPlayers", 
            Enumerable.Range(1,6).Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString()}),
            "Number of Players"                    
            ) %>
        <%= Html.SubmitButton("submitbutton", "New Game") %>
    </form>
</asp:Content>
