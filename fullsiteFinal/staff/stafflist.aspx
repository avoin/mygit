<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="stafflist.aspx.cs" Inherits="student_stafflist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div><h3>List of all the professors</h3></div>
<br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        DataSourceID="ObjectDataSource1" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="staffID" HeaderText="staffID" 
                SortExpression="staffID" />
            <asp:BoundField DataField="firstName" HeaderText="firstName" 
                SortExpression="firstName" />
            <asp:BoundField DataField="lastName" HeaderText="lastName" 
                SortExpression="lastName" />
            <asp:BoundField DataField="responsibility" HeaderText="responsibility" 
                SortExpression="responsibility" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:BoundField DataField="extention" HeaderText="extention" 
                SortExpression="extention" />
            <asp:BoundField DataField="office" HeaderText="office" 
                SortExpression="office" />
            <asp:BoundField DataField="website" HeaderText="website" 
                SortExpression="website" />
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
        SelectMethod="GetALLStaff" TypeName="AssignmentModelClass">
    </asp:ObjectDataSource>
    <br />

</asp:Content>

