<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="deleteUser.aspx.cs" Inherits="admin_Deleteuser" %>

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
<br />
<span class="style17">
    Account removal page
    </span>
<br />
<br />
<br />

</div>


<div id="details">
<table><tr><td><asp:Label ID="Label1" runat="server" Text="User Name: " 
        ForeColor="#CC0000"></asp:Label></td><td><asp:TextBox ID="TextBox1"
     runat="server"></asp:TextBox></td><td><asp:Button ID="deleteUser" runat="server" Text="Delete User" 
        onclick="deleteUser_Click" BackColor="White" BorderColor="#CC0000" 
            BorderStyle="Solid" ForeColor="#CC0000" /></td></tr></table>

 

<br />
<br />


        <br /><br />

</div>
   


</asp:Content>

