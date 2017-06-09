<%@ Page Title="Event List" Language="C#" MasterPageFile="~/SiteUser.Master" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="CFHP_FirstPlace.UserStaff.EventList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Event List</h1>
    <br />
    <asp:HyperLink ID="HyperLinkNew" runat="server" NavigateUrl="~/UserStaff/EventInfo.aspx">Add new Event</asp:HyperLink>
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="PK_Event" DataSourceID="SqlDataSourceContents"  CssClass="GridViewStyle" 
        EmptyDataText="No Event" EnableViewState = "false" >
        <Columns>
            <asp:BoundField DataField="EventType"   HeaderText="Type" SortExpression="EventType" />
            <asp:BoundField DataField="EventHeader"   HeaderText="Title" SortExpression="EventHeader" />
            <asp:BoundField DataField="EventTime" HeaderText="EventTime" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "EventInfo.aspx?EventID="+ Eval("PK_Event") %>' >Edit</a>
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
	                        EC.PK_Event, 
	                        ET.EventColor, 
	                        EC.EventStartTime,
	                        EC.EventendTime,
                            EC.EventHeader,
	                        case 
		                        when Convert(VarChar, getdate(), 106) between Convert(VarChar, EC.EventStartTime, 106) and Convert(VarChar, EC.EventEndTime, 106)
			                        then 'Today'
		                        when EC.EventStartTime=EC.EventEndTime 
			                        then Convert(VarChar, EC.EventStartTime, 106) 
		                        else Convert(VarChar, EC.EventStartTime, 106)+ ' to ' +Convert(VarChar, EC.EventEndTime, 106) end 
			                        as EventTime,
	                        ET.EventType
                        FROM         
	                        First_Event_Type ET
	                        INNER JOIN
                            First_Events EC ON ET.PK_EventType = EC.FK_EventType
                        WHERE     
	                        EC.EventActive = 1
                        order by EC.EventEndTime desc">
    </asp:SqlDataSource>

</asp:Content>
