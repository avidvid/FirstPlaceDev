<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllStaff.aspx.cs" Inherits="CFHP_FirstPlace.AllStaff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSourceAllstaff" CssClass="CenterTable">
        <ItemTemplate>
            <asp:HyperLink ID="HyperLinkPDF" runat="server" Text='<%# Eval("AllStaffName")%>' NavigateUrl='<%# Eval("AllStaffFileName") %>' CssClass="BoldMsg"/>
        </ItemTemplate>
    </asp:DataList>
    <asp:SqlDataSource ID="SqlDataSourceAllstaff" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommand="SELECT [AllStaffName], [AllStaffFileName] FROM [First_AllStaff] where [Active] = 1 order by [PK_AllStaff]">
    </asp:SqlDataSource>
</asp:Content>
