<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="CFHP_FirstPlace.Events.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
        Font-Names="Verdana" Font-Size="15pt" BorderStyle="None"
        ForeColor="Black"  Width="90%" Font-Bold="True"
        DayNameFormat="Full" NextMonthText="Next&amp;gt;" 
        PrevMonthText="&amp;lt;Previous" 
        onselectionchanged="Calendar1_SelectionChanged" 
        ondayrender="Calendar1_DayRender" >
        <DayHeaderStyle  Font-Size="10pt" BackColor="#336600" ForeColor="White" BorderStyle="None"  />
        <NextPrevStyle VerticalAlign="Bottom" Width="20%" Font-Bold="False"  Height="30"  />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#74E871" ForeColor="Black"   Height="30"  />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle   Font-Bold="True"  BackColor="White" CssClass="NoMargin" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle ForeColor="Red"/>
        <DayStyle Height="30" BorderStyle="None"  Font-Size="10pt" />
    </asp:Calendar>
        <asp:Repeater ID="Repeater_Day" runat="server">
            <HeaderTemplate>
                <h1 class="Center"><asp:Label runat="server" ID="LabelHeaderDay" text="" /></h1>
                <br />
                <table width='75%'>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td rowspan="2" width="200">     
                        <a href='<%# "CalendarEvent.aspx?EventID="+DataBinder.Eval(Container.DataItem, "PK_Event").ToString() %>'  > 
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "/EmployeeResource/Event/"+DataBinder.Eval(Container.DataItem, "EventPicture").ToString() %>' AlternateText='<%# DataBinder.Eval(Container.DataItem, "EventHeader").ToString() %>' CssClass="EventPic" />
                        </a>
                    </td>
                    <td>
                        <a href='<%# "CalendarEvent.aspx?EventID="+DataBinder.Eval(Container.DataItem, "PK_Event").ToString() %>'  >
                            <b><asp:Label runat="server" ID="Label2" text='<%# DataBinder.Eval(Container.DataItem, "EventHeader").ToString() +" ("+ DataBinder.Eval(Container.DataItem, "EventTime").ToString() +")" %>' /></b>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label runat="server" ID="Label3" text='<%# DataBinder.Eval(Container.DataItem, "EventText").ToString() %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Repeater ID="Repeater_Events" runat="server">
            <HeaderTemplate>
                <h2>Upcoming Events</h2>
                <br />
                <table width="70%" >
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td width="25%">     
                        <a href='<%# "CalendarEvent.aspx?EventID="+DataBinder.Eval(Container.DataItem, "PK_Event").ToString() %>'  > 
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "/EmployeeResource/Event/"+DataBinder.Eval(Container.DataItem, "EventPicture").ToString() %>' AlternateText='<%# DataBinder.Eval(Container.DataItem, "EventHeader").ToString() %>' CssClass="EventPic" />
                        </a>
                    </td>
                    <td >
                        <a href='<%# "CalendarEvent.aspx?EventID="+DataBinder.Eval(Container.DataItem, "PK_Event").ToString() %>'  >
                                <b><asp:Label runat="server" ID="Label2" text='<%# DataBinder.Eval(Container.DataItem, "EventHeader").ToString() +" ("+ DataBinder.Eval(Container.DataItem, "EventTime").ToString() +")" %>' /></b>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
