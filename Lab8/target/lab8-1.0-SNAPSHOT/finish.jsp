<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <title>Три</title>
</head>

<body>
<jsp:useBean id="mybean" scope="session" class="com.example.lab8.LabHandler" />
<% mybean.setNumbers(request.getParameter("numbers"));%>
<% mybean.flag = !mybean.flag;%>


<table border="1">
    <tr>
        <td>Произведение чисел, больших ${mybean.min}:</td>
        <td>${mybean.res}</td>
    </tr>
</table>

<form action="index.jsp">
    <input type="submit" value="В начало"/>
</form>

</body>

</html>