<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="EventInfo.aspx.cs" Inherits="CFHP_FirstPlace.UserStaff.EventInfo" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#<%=TextBoxStartDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%m/%d/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });

            $("#<%=TextBoxEndDate.ClientID %>").dynDateTime({
                showsTime: false,
                ifFormat: "%m/%d/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>First Place Information</h1>
        <h1><asp:Label ID="LabelTitle" runat="server" Text="Add New Post"></asp:Label></h1>
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
                <td align="right">Type</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownEventType" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">ADD Picture</td>
                <td align="left">
                    <asp:FileUpload ID="FileUploadPicture" runat="server"  />
                    <asp:Image ID="ImageEvent" runat="server" Width="100" Height="100" Visible="false"/>
                </td>
                <td ></td>
                <td ></td>
            </tr> 
            <tr>
                <td align="right">Start Date</td>
                <td align="left">
                    <asp:TextBox ID="TextBoxStartDate" runat="server" ></asp:TextBox>
                    <asp:Image ID="ImageStartDate" runat="server" ImageUrl="~/Images/Icons/calender.png"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" 
                        Text="Invalid Date ( EX: MM/DD/YYYY)" ControlToValidate="TextBoxStartDate" CssClass="validationError"  SetFocusOnError ="true"
                        ValidationExpression="(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/](19|20)\d\d"></asp:RegularExpressionValidator>
                </td>
            </tr> 
            <tr>
                <td align="right">End Date</td>
                <td align="left">
                    <asp:TextBox ID="TextBoxEndDate" runat="server" ></asp:TextBox>
                    <asp:Image ID="ImageEndDate" runat="server" ImageUrl="~/Images/Icons/calender.png"/>
                    <asp:CompareValidator ID="CompareValidatorDate" runat="server" CssClass="validationError" ErrorMessage="End date need to be after Start Date" ControlToValidate="TextBoxEndDate" ControlToCompare="TextBoxStartDate" Operator="GreaterThanEqual"></asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="RegularExpressionValidator" 
                        Text="Invalid Date ( EX: MM/DD/YYYY)" ControlToValidate="TextBoxEndDate" CssClass="validationError"  SetFocusOnError ="true"
                        ValidationExpression="(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/](19|20)\d\d"></asp:RegularExpressionValidator>
                </td>
            </tr> 
            <tr>
                <td align="right">Location</td>
                <td align="left">
                    <asp:TextBox ID="TextBoxLocation" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td align="right">Body</td>
                <td align="left">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
                    <asp:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="TextBody" runat="server" EnableSanitization="false"/>
                    <asp:TextBox ID="TextBody" runat="server" TextMode="MultiLine"  Columns="60" Rows="10" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBody" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Body is required</asp:RequiredFieldValidator>          
                 </td>
            </tr> 
            <tr>
                <td align="right">ADD Link</td>
                <td align="left">
                    <asp:TextBox ID="TextBoxLink" runat="server"  Width="200" CssClass="UserTextBox"></asp:TextBox> 
                </td>
            </tr> 
            <tr>
                <td align="right">Link Name</td>
                <td align="left">
                    <asp:TextBox ID="TextBoxLinkName" runat="server" Text="Add Text"  Width="200"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td align="right">&nbsp;</td>
                <td align="left">
                    <asp:Button ID="ButtonDelete" runat="server" Text="Delete This Post" 
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