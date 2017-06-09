<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgetPasword.aspx.cs" Inherits="CFHP_FirstPlace.ForgetPasword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Enter your user name and you will receive an email with your password. 
    Your user name is most cases is the same as your network ID. (ie jf0515).
    If you began working with CFHP before 2007 your username is the first initial of your first name plus your last name. (ie. jflores) 
    <br />
    <asp:TextBox ID="TextBoxUsername" runat="server" Width="200"></asp:TextBox>
    <asp:Button ID="ButtonSubmit" runat="server" Text="Send" onclick="ButtonSubmit_Click" />
    <br />
    <asp:Label ID="LabelReport" runat="server" Text="" ></asp:Label>
    <br />
    <asp:HyperLink ID="HyperLinkLogin" runat="server" NavigateUrl="~/Login.aspx">Login Here</asp:HyperLink>
</asp:Content>
