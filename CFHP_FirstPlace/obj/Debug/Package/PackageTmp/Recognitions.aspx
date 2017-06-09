<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recognitions.aspx.cs" Inherits="CFHP_FirstPlace.Recognitions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Repeater ID="Repeater_Birthday" runat="server">
        <HeaderTemplate>
            <h2 class="Center">Happy Birthday To our Members</h2>
        </HeaderTemplate>
        <ItemTemplate>
        <div class="PicFram">
            <a href='<%# "Employee.aspx?UserID="+DataBinder.Eval(Container.DataItem, "UserID").ToString() %>'  >
            <img height='200' width='90%'  src='<%# "/EmployeeResource/Employees/"+DataBinder.Eval(Container.DataItem, "picture").ToString() %>' ><br />
            <b><asp:Label runat="server" ID="Label1" text='<%# DataBinder.Eval(Container.DataItem, "FullName").ToString() %>' /> <br />
                <asp:Label runat="server" ID="Label2" text='<%# DataBinder.Eval(Container.DataItem, "DOB1").ToString() %>' /><br />
                <asp:Label runat="server" ID="Label3" text='<%# DataBinder.Eval(Container.DataItem, "Department").ToString() %>' /></b>
            </a>
        </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
