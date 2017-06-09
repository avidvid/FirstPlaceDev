<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="CFHP_FirstPlace.UserClient.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Change Password (Beta)</h1>
    <asp:ChangePassword ID="ChangePassword1" runat="server" 
            CancelDestinationPageUrl="~/Default.aspx"  SuccessPageUrl="~/Default.aspx" 
        ValidatorTextStyle-CssClass="validationError" BackColor="#E3EAEB" 
        BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
        ChangePasswordTitleText="" Font-Names="Verdana" Font-Size="0.8em" 
        Height="127px" Width="386px" 
    onchangingpassword="ChangePassword1_ChangingPassword">
        <CancelButtonStyle BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" />
        <ChangePasswordButtonStyle BackColor="White" BorderColor="#C5BBAF" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#1C5E55" />
        <ContinueButtonStyle BackColor="White" BorderColor="#C5BBAF" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#1C5E55" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <PasswordHintStyle Font-Italic="True" ForeColor="#1C5E55" />
        <TextBoxStyle Font-Size="0.8em" />
        <TitleTextStyle BackColor="#1C5E55" Font-Bold="True" Font-Size="0.9em" 
            ForeColor="White" />
<ValidatorTextStyle CssClass="validationError"></ValidatorTextStyle>
    </asp:ChangePassword>
    <asp:Label ID="LabelError" runat="server" CssClass="validationError"></asp:Label>

    <asp:Label ID="LabelOk" runat="server" CssClass="validationOk"></asp:Label>

</asp:Content>
