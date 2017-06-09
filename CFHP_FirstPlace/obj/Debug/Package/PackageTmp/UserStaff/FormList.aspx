<%@ Page Title="Content List" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="FormList.aspx.cs" Inherits="CFHP_FirstPlace.UserStaff.FormList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Form List</h1>
    <br />
    <asp:HyperLink ID="HyperLinkNew" runat="server" NavigateUrl="~/UserStaff/FormInfo.aspx">Add new Form</asp:HyperLink>
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="FormID" DataSourceID="SqlDataSourceContents"  CssClass="GridViewStyle" 
        EmptyDataText="No content" EnableViewState = "false" >
        <Columns>
            <asp:BoundField DataField="FormName"   HeaderText="Title" SortExpression="FormName" />
            <asp:BoundField DataField="FormTypeName" HeaderText="Form Type" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "FormInfo.aspx?FormID="+ Eval("FormID") %>' >Edit</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <RowStyle CssClass="GridViewRowStyle"  />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceContents" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommand="First_GetActiveForms"      
        SelectCommandType="StoredProcedure">
    </asp:SqlDataSource>

</asp:Content>
