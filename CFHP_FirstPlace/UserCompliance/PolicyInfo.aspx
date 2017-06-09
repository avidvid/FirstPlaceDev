<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="PolicyInfo.aspx.cs" Inherits="CFHP_FirstPlace.UserCompliance.PolicyInfo" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>First Place Policy</h1>
        <h1><asp:Label ID="LabelTitle" runat="server" Text="Add New Policy"></asp:Label></h1>
        <table  class='list'>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LabelError" runat="server" Text="" CssClass="ErrorMsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">Subject</td>
                <td align="left">
                    <asp:TextBox ID="TextSubject" runat="server" Width="200"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidatorTextTitle" runat="server"
                        CssClass="validationError" Display="Dynamic" ControlToValidate="TextSubject" 
                        ValidationExpression="^[^\\\/\%\&\?\,\'\;\:\!]+$" > These characters are not valid in here (\ / % & ? , ' ; : !).
                        </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextTitle" runat="server" 
                        ControlToValidate="TextSubject" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Subject is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Classification</td>
                <td align="left">
                    <asp:TextBox ID="TextClassification" runat="server" Width="200"></asp:TextBox>
                    <asp:CheckBox ID="CheckBoxHeader" runat="server" Text="It is a Header " />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextClassification" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Classification is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Category</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownCategory" runat="server" Width="200">
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
                    <asp:HiddenField ID="HiddenFieldFile" runat="server" />
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
                <td><asp:CheckBox ID="CheckBoxAvailable" runat="server" Text="Available " /></td>
                <td><asp:CheckBox ID="CheckBoxRequiredReading" runat="server" Text="RequiredReading" /></td>
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