<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Signup_Bad.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIGN UP BAD</title>
</head>
<body>
    <form id="form1" runat="server">
     <%--   <asp:Login ID = "Login1" runat = "server" OnAuthenticate= "ValidateUser"></asp:Login>--%>
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>
    
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        </div>
    
    <p>
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox></p>
        <p>
            <asp:Button ID="SignUp" runat="server" OnClick="SignUp_Click" Text="Sign Up" />
        </p>
        </form>
</body>
</html>
