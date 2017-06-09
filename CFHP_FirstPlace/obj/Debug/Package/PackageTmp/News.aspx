<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="News.aspx.cs" Inherits="CFHP_FirstPlace.News" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="70%" class="CenterTable">
        <asp:Repeater ID="Repeater_News" runat="server" Visible="false">
            <HeaderTemplate>
                <h2 class="Center">Latest News & Information</h2>
                <br />
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <b><asp:Label runat="server" ID="Label2" text='<%# DataBinder.Eval(Container.DataItem, "ContentHeader").ToString() %>' /></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label3" text='<%# DataBinder.Eval(Container.DataItem, "ContentSummary").ToString() %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        -------------------------------------------------------------------------------------------------------
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
