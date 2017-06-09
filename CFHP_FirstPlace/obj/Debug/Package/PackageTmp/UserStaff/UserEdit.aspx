<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="CFHP_FirstPlace.UserStaff.UserEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <h1><asp:Label ID="FirstName" runat="server"></asp:Label></h1>
        <asp:HiddenField ID="PictureName" runat="server" />
        <asp:HiddenField ID="PictureNameOld" runat="server" />
        <table  class='list'>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LabelError" runat="server" Text="" CssClass="ErrorMsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">Birthday</td>
                <td align="left">
                    <asp:TextBox ID="BirthDate" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" 
                    Text="Invalid Date ( EX: MM/DD/YYYY)" ControlToValidate="BirthDate" CssClass="validationError"  SetFocusOnError ="true"
                    ValidationExpression="(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/](19|20)\d\d"></asp:RegularExpressionValidator>
                </td>
                <td rowspan="4"> 
                    <asp:Image ID="ImageEmp" runat="server" Width="100" Height="100"/>
                </td>
            </tr>
            <tr>
                <td></td>
                <td align="left">
                    <asp:CheckBox ID="BirthdayPublish" runat="server" Text="Publish the birthday" />
                </td>
            </tr>
            <tr>
                <td align="right">Employee's Picture</td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Cancel" 
                        onclick="ButtonCancel_Click" CausesValidation="False" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Submit" 
                        onclick="ButtonOk_Click" />

                </td>
            </tr>
        </table>

</asp:Content>
