<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Dominion.GameHost.GameData>" MasterPageFile="~/Views/Shared/site.Master" %>
<%@ Import Namespace="Dominion.Web.Controllers" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    Game key: <%= Model.GameKey %>
    <% foreach(var item in Model.Slots)
       {  
           using(Html.BeginForm("JoinGame", "Home", new {id = Model.GameKey, playerId = item.Key }))
            { %>
                <%= item.Value %> <%=Html.SubmitButton("join", "Play") %>
          <%}  
       }
    %>
</asp:Content>
