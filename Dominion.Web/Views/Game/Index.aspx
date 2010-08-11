<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Dominion.Web.ViewModels.GameViewModel>" MasterPageFile="~/Views/Shared/site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div id="market"></div>
    <div id="playArea"></div>
    <div id="hand"></div>
</asp:Content>
