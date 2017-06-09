<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CFHP_FirstPlace.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Login ID="Login1" runat="server" Height="98px" Width="251px"
    onauthenticate="Login1_Authenticate" PasswordLabelText="Password" 
    UserNameLabelText="User Name" DisplayRememberMe="False" TitleText="" CssClass="Center">
        <LabelStyle Font-Bold="False" Wrap="False" />
        <LoginButtonStyle />
        <TextBoxStyle Width="130px" />
    </asp:Login>
    <div id="ForgotPass">
        <a href="ForgetPasword.aspx">Forgot your username or password?</a>
    </div>
</asp:Content>
