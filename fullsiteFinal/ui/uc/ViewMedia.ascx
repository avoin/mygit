<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewMedia.ascx.cs" Inherits="ui_uc_ViewMediaascx" %>
    <h3>List of media items</h3>
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
    DataSourceID="ObjectDataSource1" ForeColor="Black" GridLines="Vertical">
    <AlternatingRowStyle BackColor="#CCCCCC" />
    <Columns>
        <asp:HyperLinkField DataNavigateUrlFields="Id" 
            DataNavigateUrlFormatString="~/assets/images/?id={0}" DataTextField="Title" 
            HeaderText="Title" Target="_blank" />
        <asp:BoundField DataField="Description" HeaderText="Description" 
            SortExpression="Description" />
        <asp:BoundField DataField="MIMEType" HeaderText="MIMEType" 
            SortExpression="MIMEType" />
        <asp:BoundField DataField="Size" HeaderText="Size" 
            SortExpression="Size" />
        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
            SortExpression="DateCreated" />
        <asp:BoundField DataField="DateModified" HeaderText="DateModified" 
            SortExpression="DateModified" />
    </Columns>
    <FooterStyle BackColor="White" ForeColor="#CC0000" />
    <HeaderStyle BackColor="#CC0000" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#808080" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#383838" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="GetAllMedia" TypeName="AssignmentModelClass">
</asp:ObjectDataSource>
<br />




