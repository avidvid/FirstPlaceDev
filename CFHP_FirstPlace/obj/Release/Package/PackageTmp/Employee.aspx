<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="CFHP_FirstPlace.Employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:DetailsView ID="DetailsViewEmployee" runat="server" Width="100%" CssClass="Center"
        AutoGenerateRows="False" DataKeyNames="UserID" 
        DataSourceID="SqlDataSourceEmployee" BorderStyle="None" GridLines="None">
        <Fields >
            <asp:BoundField DataField="UserID" HeaderText="UserID" Visible="false"
                InsertVisible="False" ReadOnly="True" SortExpression="UserID" />
            <asp:BoundField DataField="FullName"  ReadOnly="True" SortExpression="FullName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="picture" runat="server" ImageUrl='<%# "~/EmployeeResource/Employees/"+ Eval("picture") %>' AlternateText='<%# Eval("FirstName") %>'  BorderStyle="None" Width="30%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Title"  SortExpression="Title" />
            <asp:BoundField DataField="DepartmentName"  SortExpression="DepartmentName" />
            <asp:BoundField DataField="DepartmentSubName"  SortExpression="DepartmentSubName" />
            <asp:BoundField DataField="Ext"   SortExpression="Ext" />
            <asp:BoundField DataField="Email"  SortExpression="Email" />
            <asp:BoundField DataField="DOB1"  SortExpression="DOB1" />
            <asp:BoundField DataField="Biography" SortExpression="Biography" />
        </Fields>
    </asp:DetailsView>

    <asp:SqlDataSource ID="SqlDataSourceEmployee" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommandType="StoredProcedure"
        SelectCommand="First_DepartmentEmploees">
        <selectparameters>
            <asp:QueryStringParameter Name="UserID" QueryStringField="UserID" />
        </selectparameters>
    </asp:SqlDataSource>
</asp:Content>
