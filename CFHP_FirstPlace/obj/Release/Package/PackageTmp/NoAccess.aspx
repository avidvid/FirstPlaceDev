<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoAccess.aspx.cs" Inherits="CFHP_FirstPlace.NoAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="validationError">You don't have permission to access</h1>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">Back to Login</asp:HyperLink>
</asp:Content>
