<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="News.aspx.cs" Inherits="CFHP_FirstPlace.News" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:Repeater ID="Repeater_News" runat="server">
        <HeaderTemplate>
                <h2 class="Center">Latest News & Information</h2>
                <br />
        </HeaderTemplate>
        <ItemTemplate>
            <div class='<%# "NewsBox"+DataBinder.Eval(Container.DataItem, "RowNum").ToString() %>' >
                <b><asp:Label runat="server" ID="Label2" text='<%# DataBinder.Eval(Container.DataItem, "ContentHeader").ToString() %>' /></b>
                <br />
                <asp:Label runat="server" ID="Label3" text='<%# DataBinder.Eval(Container.DataItem, "ContentSummary").ToString() %>' />
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
       
</asp:Content>
