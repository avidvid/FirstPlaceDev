<%@ Page Title="" Language="C#" MasterPageFile="~/Old.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="CFHP_FirstPlace.UserClient.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Welcome</h1> 
    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/UserClient/LicenseList.aspx">Click here to see list of items that have not been completed. </asp:HyperLink>
    
</asp:Content>
