<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="authorDefault.aspx.cs" Inherits="author_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="~/ui/css/StyleSheet.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .style4
    {
        font-family: "Courier New", Courier, monospace;
        font-size: x-large;
        color:White;
    }
    .style30
    {
        font-family: "Courier New", Courier, monospace;
        font-size: x-large;
        color:#CC0000;
    }
    .style31
    {
        width: 300px;
        height: 300px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="first" class="style4" style="background-color: #CC0000; color:White;">
    
    <strong>Welcome to the Seneca @York ICT Page</strong></div>



<div class="style30">
<h1>Author page</h1> 
<strong>As an author you can create pages and edit them for the viewing of other users.</strong>
<p>It is your duty to keep the pages interesting! So lets get to work now ;)</p>
</div>


    <img alt="" class="style31" src="ui/images/sy.jpg" />
</asp:Content>

