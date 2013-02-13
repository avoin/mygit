<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="changePassword.aspx.cs" Inherits="changePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
<div>
    <asp:ChangePassword ID="ChangePassword1" runat="server" 
        onchangedpassword="ChangePassword1_ChangedPassword" 
        CancelDestinationPageUrl="~/Default.aspx" 
        ContinueDestinationPageUrl="~/Default.aspx">
        <CancelButtonStyle BackColor="#CC0000" BorderColor="Black" ForeColor="White" />
        <ChangePasswordButtonStyle BackColor="#CC0000" BorderColor="Black" 
            ForeColor="White" />
        <ContinueButtonStyle BackColor="#CC0000" BorderColor="Black" 
            ForeColor="White" />
        <MailDefinition BodyFileName="~/App_Data/ChangedPassword.txt" 
            Subject="Changed password for ICT Seneca website">
        </MailDefinition>
        <TitleTextStyle BackColor="#CC0000" BorderColor="Black" ForeColor="White" 
            Wrap="True" />
    </asp:ChangePassword>
</div>
</div>
</asp:Content>

