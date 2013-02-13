<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style17
        {
            font-size: xx-large;
            font-family: "Broadway BT";
            text-align: left;
        }
        .style18
        {
            text-align: left;
        }
        .style19
        {
            text-decoration: underline;
            font-size: large;
        }
        .style20
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="header" class="style17">

Create New Account
</div>

<div id="content" class="style18">
    <span class="style20">If your email address ends with "</span><span 
        class="style19"><strong><em>@learn.senecac.on.ca</em></strong></span><span 
        class="style20">", then you will be given Student privilages. </span>
<br class="style20" /><br />
    <span class="style20">If your email address ends with "</span><span 
        class="style19"><strong><em>@senecacollege.ca</em></strong></span><span 
        class="style20">", then you will given Staff privilages. </span>
<br class="style20" /><br />
    <span class="style20">If your email address ends with any other email format, then you will be given Community privilages.
    </span>
<br class="style20" /><br />


</div>


<div id="createuser" align="left">



    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="White" 
        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Verdana" Font-Size="0.8em" 
        oncreateduser="CreateUserWizard1_CreatedUser" Width="305px" 
        ContinueDestinationPageUrl="~/Default.aspx">
        <ContinueButtonStyle BackColor="White" BorderColor="#CC0000" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#CC0000" />
        <CreateUserButtonStyle BackColor="White" BorderColor="#CC0000" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#CC0000" />
        <MailDefinition BodyFileName="~/App_Data/SignupConfirmation.txt" 
            Subject="Your new account at ICT Seneca website!">
        </MailDefinition>
        <TitleTextStyle BackColor="#CC0000" Font-Bold="True" ForeColor="White" />
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" />
            <asp:CompleteWizardStep runat="server" />
        </WizardSteps>
        <HeaderStyle BackColor="#CC0000" BorderColor="#CC0000" BorderStyle="Solid" 
            BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="#CC0000" 
            HorizontalAlign="Center" />
        <NavigationButtonStyle BackColor="White" BorderColor="White" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#CC0000" />
        <SideBarButtonStyle ForeColor="White" />
        <SideBarStyle BackColor="#CC0000" Font-Size="0.9em" VerticalAlign="Top" />
    </asp:CreateUserWizard>



</div>

</asp:Content>

