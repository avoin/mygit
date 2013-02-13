<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="recoverPassword.aspx.cs" Inherits="recoverPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" BackColor="White" 
            ForeColor="Black" SuccessPageUrl="~/Default.aspx">
        <SubmitButtonStyle BackColor="#CC0000" BorderColor="Black" ForeColor="White" />
        <MailDefinition BodyFileName="~/App_Data/recoveredPassword.txt" 
            Subject="Password Recovery successful!">
        </MailDefinition>
        <SuccessTextStyle ForeColor="#CC0000" />
    </asp:PasswordRecovery>
    </div>
</asp:Content>

