<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="d.aspx.cs" Inherits="OnlineTestApp.UI.d" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <table border="0" style="width: 100%" cellpadding="10" cellspacing="0">
            <tr>
                <td><a href="/">Back To Home</a></td>
            </tr>            
            <tr>
                <td>
                    <h2>Password Helper</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="10" cellspacing="0">
                        <tr>
                            <td>User Name : </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtUserName" /></td>
                        </tr>
                        <tr>
                            <td>User Password</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtUserPassword" /></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button Text="E" OnClick="Encrypt_Click" ID="Encrypt" runat="server" />
                                <asp:Button Text="D" OnClick="Dencrypt_Click" ID="Dencrypt" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label Text="" ID="lblResult" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>

    </form>
</body>
</html>
