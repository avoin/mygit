<%@ Page Title="" Language="C#" MasterPageFile="~/ui/mp/Master.master" AutoEventWireup="true" CodeFile="diplomas.aspx.cs" Inherits="student_diplomas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div><h3>List of all the diplomas programs</h3></div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
        DataSourceID="ObjectDataSource1" ForeColor="Black" GridLines="Vertical" 
        Width="1200px">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="diplomaID" HeaderText="diplomaID" 
                SortExpression="diplomaID" />
            <asp:BoundField DataField="diplomaName" HeaderText="diplomaName" 
                SortExpression="diplomaName" />
            <asp:BoundField DataField="diplomaDuration" HeaderText="diplomaDuration" 
                SortExpression="diplomaDuration" />
            <asp:BoundField DataField="diplomaDescription" HeaderText="diplomaDescription" 
                SortExpression="diplomaDescription" />
            <asp:BoundField DataField="diplomaCode" HeaderText="diplomaCode" 
                SortExpression="diplomaCode" />
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
        SelectMethod="GetDiplomaPrograms" TypeName="AssignmentModelClass">
    </asp:ObjectDataSource>
<br /><br />

<br />
</asp:Content>

