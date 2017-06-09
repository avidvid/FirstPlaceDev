<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeSearch.aspx.cs" Inherits="CFHP_FirstPlace.EmployeeSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <h1>Employee Search</h1>
        <p>Enter the person's first or last name or select a department. <br />
            Leave fields blank to get complete Employee Roster.</p> 
        <br />
             <asp:Label ID="LabelOk" runat="server" Text="Your action was successful" CssClass="OkBoldMsg" Visible="False"></asp:Label>
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
                        onclick="ButtonSearch_Click" />
            </td>
        </tr>
        </table> 
        <asp:Label ID="LabelResult" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridView1" runat="server" Visible="False" CellPadding="4" 
            AutoGenerateColumns="False"  CssClass="GridViewStyle" 
            EnableViewState = "False">
            <Columns>
                <asp:HyperLinkField AccessibleHeaderText="FullName" HeaderText="Full Name" 
                    DataNavigateUrlFields ="UserID" DataTextField="FullName" 
                    DataNavigateUrlFormatString="~/Employee.aspx?UserID={0}" />
                <asp:BoundField AccessibleHeaderText="DepartmentName" DataField="DepartmentName" 
                    HeaderText="Department" />
                <asp:BoundField AccessibleHeaderText="UserID" DataField="UserID" 
                    HeaderText="UserID" Visible="False" />
            </Columns>
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <RowStyle CssClass="GridViewRowStyle"  />
        </asp:GridView>

</asp:Content>
