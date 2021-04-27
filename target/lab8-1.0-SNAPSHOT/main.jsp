<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <title>Два</title>
</head>

<body>

<jsp:useBean id="mybean" scope="session" class="com.example.lab8.LabHandler">
    <jsp:setProperty name="mybean" property="*" />
</jsp:useBean>

<h3>Значение флага: ${mybean.flag}</h3>

<form name="Finish form" action="finish.jsp">
    <input name="numbers">
    <input type="submit" value="На финальную" name="finishButton" />
</form>
</body>

</html>

