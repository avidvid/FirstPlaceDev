<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CFHP_FirstPlace.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="MainPanel">
        <div class="Panel_Pic">
            <a href="http://www.universityhealthsystem.com/" >
                <img class="ImageCss"  src="Images/Panel/UHSButton.jpg" alt="UHS">
            </a>
        </div>
        <div class="Panel_Pic">
            <a href="Recognitions.aspx" >
                <img class="ImageCss"  src="Images/Panel/EmployeeRECButton.jpg" alt="EmployeeREC">
            </a>
        </div>
        <div class="Panel_Pic">
            <a href="Forms.aspx" >
                <img class="ImageCss"  src="Images/Panel/FormsButton.jpg" alt="Forms">
            </a>
        </div>
        <div class="Panel_Pic">
            <a href="News.aspx">
                <img class="ImageCss"  src="Images/Panel/NewInfoButton.jpg" alt="Calender">
            </a>
        </div>
        <div class="Panel_Pic">
            <a href="#" >
                <img class="ImageCss"  src="Images/Panel/SysFacButton.jpg" alt="SysAlerts"></img>
            </a>
        </div>
        <div class="Panel_Pic">
            <a href="http://isintranet/" >
                <img class="ImageCss"  src="Images/Panel/ISWebButton.jpg" alt="ISWeb">
            </a>
        </div>
        <div class="Panel_Pic">
            <a href="Policy.aspx" >
                <img class="ImageCss"  src="Images/Panel/PoliciesButton.jpg" alt="Facilities">
            </a>
        </div>
        <div class="Panel_Pic">
            <a href="http://co-store.com/cfhp" >
                <img class="ImageCss"  src="Images/Panel/ShopButton.jpg" alt="Shop">
            </a>
        </div>
    </div>
</asp:Content>
