<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="pageCreate.aspx.cs" Inherits="author_pageCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div><p><h3>Create a new editable page</h3></p>
<p>Select a folder that will hold the page and then enter the page name below</p>
<table><tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="Select the folder: "></asp:Label></td>
<td>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label2" runat="server" Text="Enter a page name: "></asp:Label></td>
<td>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td></td>
<td>
    <asp:Button ID="Button1" runat="server" Text="Create new page" 
        onclick="Button1_Click" /></td>
</tr>
</table>
</div>

</asp:Content>

