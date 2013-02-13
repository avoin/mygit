<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="loginpage.aspx.cs" Inherits="loginpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">




    <style type="text/css">
        #logincontrol
        {
            text-align: center;
        height: 335px;
    }
        
    </style>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div><h1>This page can only be viewed after logging in, please do so!</h1>
<br />
<br />
 

<div id="logincontrol">

   

    
             <asp:LoginView ID="LoginView2" runat="server">
    <AnonymousTemplate>
    
    
        <asp:LoginName ID="LoginName1" runat="server" />
    
    
        <asp:Login ID="Login2" runat="server" BorderPadding="5" BorderStyle="Solid" 
            TitleText="Log In Page" Height="50px" Width="100px">
        </asp:Login>
    
    
    </AnonymousTemplate>

    <LoggedInTemplate>
     <asp:LoginName ID="LoginName1" runat="server" />
    
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
    
    </LoggedInTemplate>

    </asp:LoginView>
    </div>
    </div>


</asp:Content>

