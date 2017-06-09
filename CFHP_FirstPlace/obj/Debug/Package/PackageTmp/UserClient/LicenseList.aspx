<%@ Page Title="" Language="C#" MasterPageFile="~/Old.Master" AutoEventWireup="true" CodeBehind="LicenseList.aspx.cs" Inherits="CFHP_FirstPlace.UserClient.LicenseList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="PK_License" DataSourceID="SqlDataSourceLicenses" 
        EmptyDataText="there is no attestation available for you" Width="70%" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "LicenseView.aspx?LicenseID="+ Eval("PK_License") %>' ><%# Eval("LicenseName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LicenseDueDate"  SortExpression="LicenseDueDate" HeaderText="Due Date" />        
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceLicenses" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommand="SELECT [PK_License]
                              ,FL.[LicenseName]
                              ,[FK_LicenseType]
                              ,FLT.[LicenseName] as LicenseTypeName
                              ,FLT.Priority
                              ,FLT.[LicenseNeedFile]
                              ,FLT.[LicenseMessage]
                              ,[LicenseDescription]
                              ,[LicenseFileLocation]
                              ,[LicenseMandatory]
                              ,[FK_Department]
                              ,[LicenseDateAdd]
                              ,Convert( VARCHAR(12),LicenseDueDate,107) as LicenseDueDate
                              ,[LicenseDateStart]
                              ,[LicenseDateEnd]
                                  ,[LicenseReDo]
                                  ,[LicenseReDoMonths]
                            FROM [CFHPFirstPlace].[dbo].[First_Licenses] as FL
                            left outer join 
                            [CFHPFirstPlace].[dbo].[First_LicenseType]  as FLT on   FL.FK_LicenseType=FLT.PK_LicenseType
                            WHERE 
                                LicenseActive = 1
	                            and ([FK_Department] = @Department or [FK_Department] = 1)
                                and PK_License not in 
	                            (SELECT  [FK_License] 
	                            FROM [CFHPFirstPlace].[dbo].[First_UsersLicenses]
	                            WHERE
                                FK_User = @User
	                            and  UserLicenseExpiration > getdate() )">
        <SelectParameters>
            <asp:Parameter DefaultValue="Anonymous" Name="User" Type="String" />
            <asp:Parameter DefaultValue="Anonymous" Name="Department" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
