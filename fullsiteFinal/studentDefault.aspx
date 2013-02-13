<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="studentDefault.aspx.cs" Inherits="student_Default" %>

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
<h1>Announcement!</h1> 
<strong>Exams starting next week. Good luck on the preparations.</strong>
<p>All students are advised to go check their exam time slots.</p>
</div>


    <img alt="" class="style31" src="ui/images/sy.jpg" />

</asp:Content>

