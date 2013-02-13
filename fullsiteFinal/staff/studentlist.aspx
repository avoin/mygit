<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="studentlist.aspx.cs" Inherits="student_studentlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div><h3>A list of current students</h3></div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        DataSourceID="ObjectDataSource1" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="studentID" HeaderText="studentID" 
                SortExpression="studentID" />
            <asp:BoundField DataField="firstName" HeaderText="firstName" 
                SortExpression="firstName" />
            <asp:BoundField DataField="lastName" HeaderText="lastName" 
                SortExpression="lastName" />
            <asp:BoundField DataField="programName" HeaderText="programName" 
                SortExpression="programName" />
            <asp:BoundField DataField="semester" HeaderText="semester" 
                SortExpression="semester" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="#CC0000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetALLStudents" TypeName="AssignmentModelClass">
    </asp:ObjectDataSource>
<br /><br />

<br />
</asp:Content>

