<%@ Page Title="" Language="C#" MasterPageFile="~/Old.Master" AutoEventWireup="true" CodeBehind="LicenseView.aspx.cs" Inherits="CFHP_FirstPlace.UserClient.LicenseView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LableMessage" runat="server" CssClass="ErrorMsg" 
        Text="To complete the webinar, please use Internet Explorer.  You cannot use Firefox or other browsers to complete training."  ></asp:Label>
    <asp:DetailsView ID="DetailsViewLicense" runat="server" Height="50px" Width="50%" 
        AutoGenerateRows="False" DataKeyNames="PK_License" EmptyDataText ="This license is not available for you" 
        DataSourceID="SqlDataSourceLicense" CellPadding="4" ForeColor="#333333" 
        GridLines="None" onload="DetailsViewLicense_Load" >
        <Fields>
            <asp:BoundField DataField="PK_License"  InsertVisible="False" ReadOnly="True" SortExpression="PK_License" Visible="false" />
            <asp:BoundField DataField="LicenseName"  SortExpression="LicenseName" />        
            <asp:TemplateField > 
                <ItemTemplate >
                  <asp:Placeholder ID="PlaceholderVideo" runat="server" Visible='<%#Convert.ToBoolean(Eval("VideoPlayer"))%>'>
                        <object width="600" height="400" >
                            <param name="Video" value='<%#  Eval("LicenseName") %>'>
                            <embed src='<%#  Eval("LicenseFileLocation") %>' width="600" height="400">
                            </embed>
                        </object>
                   </asp:Placeholder>
                  <asp:Placeholder ID="PlaceholderPDF" runat="server" Visible='<%#Convert.ToBoolean(Eval("LinkView"))%>'>
                        <asp:HyperLink ID="HyperLinkPDF" runat="server" Text="Click Here" NavigateUrl='<%# Eval("LicenseFileLocation") %>' CssClass="BoldMsg"/>
                   </asp:Placeholder>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LicenseDescription"  SortExpression="LicenseDescription" />
        </Fields>
    </asp:DetailsView>
    <br />
    <br />
        <table >
            <tr>
                <td>Employee ID</td>
                <td><asp:TextBox ID="TextBoxEmployeeID" runat="server" MaxLength="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTextBoxEmployeeID" runat="server" 
                ErrorMessage="*" ControlToValidate ="TextBoxEmployeeID" CssClass="validationError"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxEmployeeID" runat="server" 
                ControlToValidate="TextBoxEmployeeID" Display="Dynamic" ErrorMessage="Digit only" 
                ValidationExpression="[\d]{4,6}"  CssClass="validationError"></asp:RegularExpressionValidator></td>
                
            </tr>

            
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="LabelName" runat="server" Text=""  ></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <a href="LicenseList.aspx">Back</a>
                </td>
                <td>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Confirm"  Enabled="false"
                        onclick="ButtonSubmit_Click"  OnClientClick="if ( !confirm('Are you sure you already viewed the materials and read the attestation?')) return false;"/>
                </td>
            </tr>
        </table> 
    <asp:SqlDataSource ID="SqlDataSourceLicense" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommand="SELECT 
	                        [PK_License]
                            ,FL.[LicenseName]
                            ,[FK_LicenseType]
                            ,case when FLT.[LicenseName] = 'Video' then 1 else 0 end as VideoPlayer
                            ,case when FLT.[LicenseName] <> 'Video' then 1 else 0 end as LinkView
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
                        WHERE PK_License not in 
	                        (SELECT 
	                        [FK_License] 
	                        FROM [CFHPFirstPlace].[dbo].[First_UsersLicenses]
	                        WHERE
	                        UserLicenseExpiration > getdate()
	                        and FK_User = @User
	                        )
                            and [PK_License] = @PK_License
	                        and ([FK_Department] = @Department or [FK_Department] = 1)">
        <SelectParameters>
            <asp:Parameter DefaultValue="Anonymous" Name="User" Type="String" />
            <asp:Parameter DefaultValue="Anonymous" Name="PK_License" Type="String" />
            <asp:Parameter DefaultValue="Anonymous" Name="Department" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
