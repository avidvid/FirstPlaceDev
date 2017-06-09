<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="ContentInfo.aspx.cs" Inherits="CFHP_FirstPlace.UserStaff.ContentInfo" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>First Place Information</h1>
        <h1><asp:Label ID="LabelTitle" runat="server" Text="Add New Post"></asp:Label></h1>
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
                        ValidationExpression="^[^\\\/\%\&\?\'\;\:\!]+$" > These characters are not valid in here (\ / % & ? , ' ; : !).
                        </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextTitle" runat="server" 
                        ControlToValidate="TextTitle" ErrorMessage="RequiredFieldValidator" CssClass="validationError">Title is required</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">Department</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownDepartment" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">Category</td>
                <td align="left">
                    <asp:DropDownList ID="DropDownMedia" runat="server" Width="200">
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
                         <asp:ListItem Text="Add File (Picture, Document)" Value="2" />
                    </asp:DropDownList>
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
                    <asp:Label ID="LabelLinkName1" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxLinkName1" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextBoxLink2" runat="server" Visible="false" Width="200" CssClass="UserTextBox"></asp:TextBox>
                    <br />
                    <asp:Label ID="LabelLinkName2" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxLinkName2" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="TextBoxLink3" runat="server" Visible="false" Width="200" CssClass="UserTextBox"></asp:TextBox>
                    <br />
                    <asp:Label ID="LabelLinkName3" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxLinkName3" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br />
                    <asp:Label ID="LabelFileName1" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxFileName1" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server"  Visible="false"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFile" runat="server" Enabled="false"
                        ControlToValidate="FileUpload1" ErrorMessage="RequiredFieldValidator" CssClass="validationError">File is required</asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="LabelFileName2" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxFileName2" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br />  
                    <asp:FileUpload ID="FileUpload2" runat="server"  Visible="false"/>
                    <br /> 
                    <asp:Label ID="LabelFileName3" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxFileName3" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br />  
                    <asp:FileUpload ID="FileUpload3" runat="server"  Visible="false"/>
                    <br />  
                    <asp:Label ID="LabelFileName4" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxFileName4" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br /> 
                    <asp:FileUpload ID="FileUpload4" runat="server"  Visible="false"/>
                    <br />  
                    <asp:Label ID="LabelFileName5" runat="server" Text="Click here for" Visible="false"></asp:Label> 
                    <asp:TextBox ID="TextBoxFileName5" runat="server" Text="Add Text" Visible="false" Width="200"></asp:TextBox>
                    <br /> 
                    <asp:FileUpload ID="FileUpload5" runat="server"  Visible="false"/>
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