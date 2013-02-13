<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="stafflist.aspx.cs" Inherits="stafflist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style17
        {
            font-size: x-large;
            font-family: "Broadway BT";
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="header" class="style17">
Staff Information<br /> <br />
</div>

<div id="stafflist">
    <asp:ListView ID="ListView1" runat="server">
    </asp:ListView>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"></asp:ObjectDataSource>



</div>
    
</asp:Content>

