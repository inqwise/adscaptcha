<% @ Page Language="C#" %>
<%
foreach (string var in Request.ServerVariables)
{
  Response.Write(var + " " + Request[var] + "<br>");
}
%>