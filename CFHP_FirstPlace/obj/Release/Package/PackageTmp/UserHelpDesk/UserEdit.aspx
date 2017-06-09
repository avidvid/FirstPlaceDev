<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="CFHP_FirstPlace.UserHelpDesk.UserEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <!-- <link href="~/Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="~/Scripts/jquery-ui.js" type="text/javascript"></script> -->

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css">


    <script type="text/javascript" >
        $(function() {
            var availableTags = document.getElementById("<%= hdnTitle.ClientID %>").value.split(";");
            $(document.getElementById("<%= TextTitle.ClientID %>")).autocomplete({ source: availableTags });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <h1><asp:Label ID="LabelTitle" runat="server" Text="Add New User"></asp:Label></h1>

        <table  class='list'>
            <tr>
                <td align="right">First Name</td>
                <td align="left">
                    <asp:TextBox ID="TextFName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError ="true"
                        ControlToValidate="TextFName" ErrorMessage="RequiredFieldValidator" CssClass="validationError">First name is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Last Name</td>
                <td align="left">
                    <asp:TextBox ID="TextLName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  SetFocusOnError ="true"
                        ControlToValidate="TextLName" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Last name is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Department</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownDepartment" runat="server" Width="200">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  SetFocusOnError ="true"
                        ControlToValidate="DropDownDepartment" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Department is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Sub Department</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownSubDepartment" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">Title</td>
                <td align="left">
                    <asp:TextBox ID="TextTitle" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="CheckBoxSenior" runat="server" Text="Senior Staff" />
                    <asp:HiddenField ID="hdnTitle" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">User Name</td>
                <td align="left">
                    <asp:TextBox ID="TextUsername" runat="server" MaxLength ="7"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  SetFocusOnError ="true"
                        ControlToValidate="TextUsername" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Username is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Password</td>
                <td align="left">
                    <asp:TextBox ID="TextPassword" runat="server"  MaxLength ="15"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  SetFocusOnError ="true"
                        ControlToValidate="TextPassword" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Password is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Authorization</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownAuthorization" runat="server" Width="200">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="RangeValidator2" runat="server"  SetFocusOnError ="true"
                        ErrorMessage="RangeValidator" MinimumValue="1"  CssClass="validationError" 
                        ControlToValidate="DropDownAuthorization" MaximumValue="1000" 
                        Type="Integer">Authorization is required</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Extention</td>
                <td align="left">
                    <asp:TextBox ID="TextExt" runat="server" MaxLength ="4"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator3" runat="server"  SetFocusOnError ="true"
                        ControlToValidate="TextExt" CssClass="validationError" 
                        ErrorMessage="RangeValidator" MaximumValue="9999" MinimumValue="1000" 
                        Type="Integer">Invalid extention (4 Digit)</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Email</td>
                <td align="left">
                    <asp:TextBox ID="TextEmail" runat="server" MaxLength ="20"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" 
                        Text="Invalid e-mail address." ControlToValidate="TextEmail" CssClass="validationError"  SetFocusOnError ="true"
                        ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" 
                        Enabled="False"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Home Phone</td>
                <td align="left">
                    <asp:TextBox ID="TextHomePhone" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ErrorMessage="Invalid phone" CssClass="validationError" 
                    ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" SetFocusOnError ="true"
                    ControlToValidate="TextHomePhone"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Mobile Phone</td>
                <td align="left">
                    <asp:TextBox ID="TextMobilePhone" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                    ErrorMessage="Invalid Mobile phone" CssClass="validationError" 
                    ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" SetFocusOnError ="true"
                    ControlToValidate="TextMobilePhone"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Pager</td>
                <td align="left">
                    <asp:TextBox ID="TextPager" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                    ErrorMessage="Invalid Pager" CssClass="validationError" 
                    ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" SetFocusOnError ="true"
                    ControlToValidate="TextPager"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Birthday</td>
                <td align="left">
                    <asp:TextBox ID="TextBirthDate" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="CheckBoxBirthDay" runat="server" Text="Publish the birthday" />
                    <br />
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator2" runat="server" ErrorMessage="RegularExpressionValidator"  SetFocusOnError ="true"
                        Text="Invalid Date ( EX: MM/DD/YYYY)" ControlToValidate="TextBirthDate" CssClass="validationError" 
                        ValidationExpression="(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/](19|20)\d\d"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right"></td>
                <td align="left">
                    <asp:CheckBox ID="CheckBoxActive" runat="server" Text="Active Account" Checked="true"/>
                </td>
            </tr>
            <tr>
                <td align="right"></td>
                <td align="left">
                    <asp:CheckBox ID="CheckBoxSendMail" runat="server" Checked="true" Text="Send notification Email to the user " />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit"  
                        onclick="ButtonSubmit_Click"   />
                </td>
                <td>
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" 
                        CausesValidation="false" onclick="ButtonCancel_Click"  />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LabelError" runat="server" Text="" CssClass="ErrorMsg"></asp:Label>
                </td>
            </tr>
        </table>

</asp:Content>
