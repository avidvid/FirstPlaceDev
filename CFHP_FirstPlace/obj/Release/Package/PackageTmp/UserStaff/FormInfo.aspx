<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="FormInfo.aspx.cs" Inherits="CFHP_FirstPlace.UserStaff.FormInfo" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>First Place Information</h1>
        <h1><asp:Label ID="LabelTitle" runat="server" Text="Add New Form"></asp:Label></h1>
        <table  class='list'>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LabelError" runat="server" Text="" CssClass="ErrorMsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">Title</td>
                <td align="left">
                    <asp:TextBox ID="TextTitle" runat="server" Width="200"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidatorTextTitle" runat="server"
                        CssClass="validationError" Display="Dynamic" ControlToValidate="TextTitle" 
                        ValidationExpression="^[^\\\/\%\&\?\,\'\;\:\!]+$" > These characters are not valid in here (\ / % & ? , ' ; : !).
                        </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextTitle" runat="server" 
                        ControlToValidate="TextTitle" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Title is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Form Type</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownFormType" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">ADD File / Link</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownInfo" runat="server" Width="200" 
                        AutoPostBack="True" onselectedindexchanged="DropDownInfo_SelectedIndexChanged">
                         <asp:ListItem Text="No Information" Value="0" />
                         <asp:ListItem Text="Add Link" Value="1" />
                         <asp:ListItem Text="Add Document" Value="2" />
                    </asp:DropDownList>
                    <asp:HyperLink ID="HyperLinkFormView" runat="server" Visible="false">View Form</asp:HyperLink>
                    <asp:HiddenField ID="HiddenFieldLink" runat="server" />
                    <asp:HiddenField ID="HiddenFieldFile" runat="server" />
                    <asp:HiddenField ID="HiddenFieldIsLink" runat="server" />
                </td>
            </tr> 
            <tr>
                <td align="center" >
                    <asp:Label ID="LabelInfo" runat="server" Text=""></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="TextBoxLink1" runat="server" Visible="false" Width="200" CssClass="UserTextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorURL" runat="server"  Enabled="false"
                        ControlToValidate="TextBoxLink1" ErrorMessage="RequiredFieldValidator" CssClass="validationError">URL is required</asp:RequiredFieldValidator>
                    <br /> 
                    <asp:FileUpload ID="FileUpload1" runat="server"  Visible="false"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFile" runat="server" Enabled="false"
                        ControlToValidate="FileUpload1" ErrorMessage="RequiredFieldValidator" CssClass="validationError">File is required</asp:RequiredFieldValidator>
                    <br />
                </td>
            </tr> 
            <tr>
                <td align="right">&nbsp;</td>
                <td align="left">
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete This Form" 
                        onclick="ButtonDelete_Click" Visible="False" BackColor="#CC0000" />
                </td>
            </tr>          
            <tr>
                <td>
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" 
                        CausesValidation="False" onclick="ButtonCancel_Click"  />
                </td>
                <td>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit"  
                        onclick="ButtonSubmit_Click"  />
                </td>
            </tr>
        </table>
        </asp:Content>