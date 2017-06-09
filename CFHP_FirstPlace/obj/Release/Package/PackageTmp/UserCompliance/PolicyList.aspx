<%@ Page Title="Content List" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="PolicyList.aspx.cs" Inherits="CFHP_FirstPlace.UserCompliance.PolicyList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Policy List</h1>
    <br />
    <asp:HyperLink ID="HyperLinkNew" runat="server" NavigateUrl="~/UserCompliance/PolicyInfo.aspx">Add new Policy</asp:HyperLink>
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="PolicyID" DataSourceID="SqlDataSourceContents"  CssClass="GridViewStyle" 
        EmptyDataText="No content" EnableViewState = "false" >
        <Columns>
            <asp:BoundField DataField="Classification"   HeaderText="Classification" SortExpression="Classification" />
            <asp:BoundField DataField="Subject" HeaderText="Subject" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "PolicyInfo.aspx?PolicyID="+ Eval("PolicyID") %>' >Edit</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <RowStyle CssClass="GridViewRowStyle"  />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceContents" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommand="First_GetActivePolicy"      
        SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>

</asp:Content>
