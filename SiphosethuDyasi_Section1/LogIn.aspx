<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="SiphosethuDyasi_Section1.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
	  <link rel="stylesheet" href="Style_Sheet.css">
</head>
<body>
	<div class="main">
		<h3 class="sign" align="center">Sign In</h3>
		<form class="form1" runat="server" autocomplete="off">
			<asp:TextBox runat="server" CssClass="logInText" ID="txtUsername" Placeholder="Username"/>
			<asp:TextBox runat="server" CssClass="logInText" ID="txtPassword" Placeholder="Password" TextMode="Password"/>
			<asp:Button runat="server" Text="Log In" ID="btnLogIn" CssClass="submit" OnClick="logIn"/>
			<p><asp:Label runat="server" ID="lblErrorMessage" Visible="false" /></p>
		</form>
	</div>
</body>
</html>
