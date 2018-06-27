<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN</title>
</head>
<body>
    <form id="form1" runat="server">     
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>
    
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        </div>
    
    <p>
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox></p>
        <p>
            <asp:Button ID="Login1" runat="server" OnClick="Login_Click" Text="Login" />
        </p>
        </form>
    <p>
        Don&#39;t have an Account?&nbsp; <a href="Signup.aspx">Sign up here</a></p>
   
        </p>
</body>
</html>
