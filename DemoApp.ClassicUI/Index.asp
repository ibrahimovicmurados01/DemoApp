<!DOCTYPE html>
<html>
<body>
<%
' Read the session data (cookies) set by the ASP.NET Core application
Dim sessionUserName
Dim sessionUserId

sessionUserName = Request.Cookies("Session_UserName")
sessionUserId = Request.Cookies("Session_UserId")

' Check if the cookies exist and have values
If Not sessionUserName = "" And Not sessionUserId = "" Then
 
    ' Assign the values to the session variables
    Session("UserId") = sessionUserId
    Session("UserName") = sessionUserName
    
    ' Output the values
    Response.Write("Cookie (UserName): " & Session("UserName") & "<br>")
    Response.Write("Cookie (UserId): " & Session("UserId") & "<br>")
Else
    Response.Write("Session cookies not found.")
End If
%>

</body>
</html>
