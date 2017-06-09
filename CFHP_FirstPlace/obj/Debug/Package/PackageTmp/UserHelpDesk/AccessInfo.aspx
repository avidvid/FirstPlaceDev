<%@ Page Title="" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="AccessInfo.aspx.cs" Inherits="CFHP_FirstPlace.UserHelpDesk.AccessInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="LeftBar">
        <h1>
            List of Accesses for
            <asp:Label ID="LabelName" runat="server" Text="" ></asp:Label>
        </h1>
        <asp:Label ID="LabelOk" runat="server" Text="" CssClass="OkMsg" Visible="False"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" CellPadding="4" 
            AutoGenerateColumns="False" CssClass="GridViewStyle" EnableViewState = "False">
            <Columns>
                <asp:BoundField AccessibleHeaderText="ReportName" DataField="ReportName" HeaderText="Report Name" />
                <asp:BoundField AccessibleHeaderText="DepartmentName" DataField="DepartmentName" HeaderText="DepartmentName" />
                
                <asp:HyperLinkField AccessibleHeaderText="Delete" HeaderText="Delete" Text="Delete" ControlStyle-ForeColor="Red"
                    DataNavigateUrlFields="ReportID,UserID"
                    DataNavigateUrlFormatString="~/UserHelpDesk/AccessDelete.aspx?ReportID={0}&UserID={1}" />

            </Columns>
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <RowStyle CssClass="GridViewRowStyle"  />
        </asp:GridView>
        <asp:HyperLink ID="HyperLinkDelete" runat="server"  ForeColor="Red" >Delete All Access</asp:HyperLink>
    </div>
    <div id="RightBar">         
        <h2>Search accesses name to give to this user</h2>  
        <table border="0">
        <tr>
            <td align="right">Access Name</td>
            <td align="left"><asp:TextBox ID="TextBoxAccess" runat="server" Width="200"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="ButtonSearch2" runat="server" Text="Search" 
                    onclick="ButtonSearch2_Click"  />
            </td>
            <td>
                <asp:Button ID="ButtonBack" runat="server" Text="Back" 
                    onclick="ButtonBack_Click" />
            </td>
        </tr>
        </table>
        <asp:Label ID="LabelResultAccess" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridView3" runat="server" CellPadding="4" 
             AutoGenerateColumns="False" CssClass="GridViewStyle" EnableViewState = "false">
            <Columns>
                <asp:BoundField AccessibleHeaderText="ReportName" DataField="ReportName" 
                    HeaderText="Access Name" />
                <asp:BoundField AccessibleHeaderText="DepartmentName" DataField="DepartmentName" 
                    HeaderText="Department" />
                <asp:BoundField AccessibleHeaderText="ReportID" DataField="ReportID" 
                    HeaderText="ReportID" Visible="False" />
                <asp:TemplateField HeaderText="Add">
                    <ItemTemplate>
                        <asp:HyperLink ID="link" runat="server" NavigateUrl='<% #"~/UserHelpDesk/AccessCopy.aspx?ReportID="+Eval("ReportID")+"&UserID="+Request.QueryString["UserID"]%>' Text='<%# Eval("DontHave").ToString() == "1" ? "Add": "" %>'  Enabled='<%# Eval("DontHave").ToString() == "1" ? true:false %>' ></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <RowStyle CssClass="GridViewRowStyle"  />
        </asp:GridView>
        <h2>Search Employees to copy their Access</h2>  
        <table border="0">
        <tr>
            <td align="right">Name</td>
            <td align="left"><asp:TextBox ID="TextBoxName" runat="server" Width="200"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">Department</td>
            <td align="left">
                <asp:DropDownList ID="DropDownDepartment" runat="server" Width="200">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="ButtonSearch" runat="server" Text="Search" 
                    onclick="ButtonSearch_Click"  />
            </td>
        </tr>
        </table>
        <asp:Label ID="LabelResultEmployee" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
             AutoGenerateColumns="False" CssClass="GridViewStyle" EnableViewState = "false">
            <Columns>
                <asp:BoundField AccessibleHeaderText="FullName" DataField="FullName" HeaderText="Full Name" />
                <asp:BoundField AccessibleHeaderText="DepartmentName" DataField="DepartmentName" HeaderText="DepartmentName" />
                <asp:BoundField AccessibleHeaderText="UserID" DataField="UserID" HeaderText="UserID" Visible="False" />
                <asp:TemplateField HeaderText="Copy">
                    <ItemTemplate>
                        <asp:HyperLink ID="link" runat="server" NavigateUrl='<% #"~/UserHelpDesk/AccessCopy.aspx?UserMirrorID="+Eval("UserID")+"&UserID="+Request.QueryString["UserID"]%>' Text="Copy"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <RowStyle CssClass="GridViewRowStyle"  />
        </asp:GridView>
    </div>

</asp:Content>
