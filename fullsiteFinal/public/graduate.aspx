<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="graduate.aspx.cs" Inherits="student_graduate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div><h3>List of all the graduate certificate programs</h3></div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        DataSourceID="ObjectDataSource1" ForeColor="Black" GridLines="Vertical" 
        Width="1200px">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="graduateID" HeaderText="graduateID" 
                SortExpression="graduateID" />
            <asp:BoundField DataField="graduateName" HeaderText="graduateName" 
                SortExpression="graduateName" />
            <asp:BoundField DataField="graduateDuration" HeaderText="graduateDuration" 
                SortExpression="graduateDuration" />
            <asp:BoundField DataField="graduateDescription" HeaderText="graduateDescription" 
                SortExpression="graduateDescription" />
            <asp:BoundField DataField="graduateCode" HeaderText="graduateCode" 
                SortExpression="graduateCode" />
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
        SelectMethod="GetGraduatePrograms" TypeName="AssignmentModelClass">
    </asp:ObjectDataSource>
<br /><br />

<br />
</asp:Content>

