<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="programOverview.aspx.cs" Inherits="student_programOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div><h3>Here is an overview of all the programs in this school</h3></div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        DataSourceID="ObjectDataSource1" ForeColor="Black" GridLines="Vertical" 
        Width="1200px">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="ProgramName" HeaderText="ProgramName" 
                SortExpression="ProgramName" />
            <asp:BoundField DataField="ProgramCode" HeaderText="ProgramCode" 
                SortExpression="ProgramCode" />
            <asp:BoundField DataField="ProgramDescription" HeaderText="ProgramDescription" 
                SortExpression="ProgramDescription" />
            <asp:BoundField DataField="ProgramDuration" HeaderText="ProgramDuration" 
                SortExpression="ProgramDuration" />
            <asp:BoundField DataField="ProgramCredential" HeaderText="ProgramCredential" 
                SortExpression="ProgramCredential" />
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
        SelectMethod="GetOverview" TypeName="AssignmentModelClass">
    </asp:ObjectDataSource>
<br /><br />

<br />
</asp:Content>

