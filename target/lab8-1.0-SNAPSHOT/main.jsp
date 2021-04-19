<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <title>Два</title>
</head>

<body>
<jsp:useBean id="mybean" scope="session" class="com.example.lab8.LabHandler" />

<input type="text" name="numbers">

<form name="Finish form" action="finish.jsp">
    <input type="submit" value="На финальную" name="finishButton" />
    <jsp:setProperty name="mybean" property="numbers" />
</form>
</body>

</html>