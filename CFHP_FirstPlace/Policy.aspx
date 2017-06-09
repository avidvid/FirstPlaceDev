<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Policy.aspx.cs" Inherits="CFHP_FirstPlace.Policy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div ID="ListView">
        <% Show_Forms();%>
    </div>
</asp:Content>
