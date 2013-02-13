<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="addAdmin.aspx.cs" Inherits="admin_Addadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style17
        {
            font-size: x-large;
            color: #CC0000;
            font-family: "Broadway BT";
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="header">
<br />
<br />

    <span class="style17">Admin account creation page
</span>
<br class="style17" /> 
    <br />
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="White" 
        BorderColor="#CC0000" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Verdana" Font-Size="0.8em" 
        oncreateduser="CreateUserWizard1_CreatedUser" style="margin-top: 0px" 
        ForeColor="Black">
        <CompleteSuccessTextStyle BackColor="White" ForeColor="#CC0000" />
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
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" 
                Title="Making new admin account">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
        <FinishCompleteButtonStyle ForeColor="#CC0000" />
        <HeaderStyle BackColor="#CC0000" BorderColor="#CC0000" BorderStyle="Solid" 
            BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="#CC0000" 
            HorizontalAlign="Center" />
        <NavigationButtonStyle BackColor="White" BorderColor="#CC9966" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            ForeColor="#990000" />
        <SideBarButtonStyle ForeColor="White" />
        <SideBarStyle BackColor="#990000" Font-Size="0.9em" VerticalAlign="Top" />
    </asp:CreateUserWizard>
    <br />

</div>
   
    

</asp:Content>

