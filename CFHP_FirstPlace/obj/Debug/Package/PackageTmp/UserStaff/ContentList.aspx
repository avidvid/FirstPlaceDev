<%@ Page Title="Content List" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="ContentList.aspx.cs" Inherits="CFHP_FirstPlace.UserStaff.ContentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Content List</h1>
    <br />
    <asp:HyperLink ID="HyperLinkNew" runat="server" NavigateUrl="~/UserStaff/ContentInfo.aspx">Add new Content</asp:HyperLink>
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="PK_Content" DataSourceID="SqlDataSourceContents"  CssClass="GridViewStyle" 
        EmptyDataText="No content" EnableViewState = "false" >
        <Columns>
            <asp:BoundField DataField="ContentHeader"   HeaderText="Title" SortExpression="ContentHeader" />
            <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "ContentInfo.aspx?ContentID="+ Eval("PK_Content") %>' >Edit</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <RowStyle CssClass="GridViewRowStyle"  />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceContents" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommand="	SELECT     
                            PK_Content, 
                            ContentHeader, 
                            FK_Department, 
                            FK_ContentType, 
                            ISNULL(ltrim(rtrim(DepartmentName)), '')+ ' News & Information' as DepartmentName, 
                            ContentMedia
                        FROM         
                            Content as C 
                            INNER JOIN
                            ContentType as CT
                            ON C.FK_ContentType = CT.PK_ContentType 
                            left outer join
                            First_Departments as Dep ON C.FK_Department = Dep.DepartmentIDOld
                        WHERE  
                            C.ContentActive = 1
                            and C.FK_ContentType in (1,4,10)
                        order by ContentAdded desc">
    </asp:SqlDataSource>

</asp:Content>
