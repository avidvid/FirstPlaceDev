<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="CFHP_FirstPlace.Departments.Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="Info">
        <h3>
            <asp:Label ID="LableDepName" runat="server" Text="" ></asp:Label>
        </h3>
        <asp:Label ID="LableDepContent" runat="server" Text="" ></asp:Label>
    </div>
    <asp:Repeater ID="Repeater_Emploee" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="Frame">
                <div class="Frame_Pic">
                    <a href='<%# "Employee.aspx?UserID="+DataBinder.Eval(Container.DataItem, "UserID").ToString() %>'  >
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "/EmployeeResource/Employees/"+DataBinder.Eval(Container.DataItem, "picture").ToString() %>' AlternateText='<%# DataBinder.Eval(Container.DataItem, "FullName").ToString() %>' CssClass="PersonalPic" />
                    </a>
                &nbsp;</div>
                <div class="Frame_Name">
                    <asp:Label runat="server" ID="Label1" text='<%# DataBinder.Eval(Container.DataItem, "FullName").ToString() %>' />
                    <br />
                    <asp:Label runat="server" ID="Label2" text='<%# DataBinder.Eval(Container.DataItem, "Ext").ToString() %>' />
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
