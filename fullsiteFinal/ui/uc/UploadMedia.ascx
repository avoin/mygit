<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadMedia.ascx.cs" Inherits="ui_uc_UploadMedia" %>
<br />
<table>
    <tr>
        <td>
           <b> Title:</b>
        </td>
        <td>
            <asp:TextBox ID="tbTItle" runat="server"></asp:TextBox><br />
        </td>
    </tr>
    <tr>
        <td>
            <b>Description:</b>
        </td>
        <td>
            <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox><br />
        </td>
    </tr>
    <tr>
        <td><b>Uploaded File: </b>
            
        </td>
        <td>
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        </td>
    </tr>
    <tr>
        <td colspan='2'>
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
