<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalendarEvent.aspx.cs" Inherits="CFHP_FirstPlace.CalendarEvent" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <asp:DetailsView ID="DetailsViewEvent" runat="server" Width="100%" CssClass="Center"
        AutoGenerateRows="False" DataKeyNames="EventID" 
        DataSourceID="SqlDataSourceEvent" BorderStyle="None" GridLines="None">
        <Fields >
            <asp:BoundField DataField="EventID" HeaderText="EventID" Visible="false"
                InsertVisible="False" ReadOnly="True" SortExpression="EventID" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="picture" runat="server" ImageUrl='<%# "~/EmployeeResource/Event/"+ Eval("EventPicture") %>' AlternateText='<%# Eval("EventHeader") %>'  BorderStyle="None" Width="30%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="EventHeader"  SortExpression="EventHeader" />
            <asp:BoundField DataField="EventTime"  SortExpression="EventTime" />
            <asp:BoundField DataField="EventText" SortExpression="EventText" />
            <asp:BoundField DataField="EventStartTime2"  SortExpression="EventStartTime2" />
            <asp:BoundField DataField="EventEndTime2"  SortExpression="EventEndTime2" />
            <asp:BoundField DataField="EventLocation"  SortExpression="EventLocation" />
        </Fields>
    </asp:DetailsView>

    <asp:SqlDataSource ID="SqlDataSourceEvent" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CFHPFirstPlaceConnectionString %>" 
        SelectCommandType="StoredProcedure"
        SelectCommand="First_EventView">
        <selectparameters>
            <asp:QueryStringParameter Name="EventID" QueryStringField="EventID" />
        </selectparameters>
    </asp:SqlDataSource>
</asp:Content>
