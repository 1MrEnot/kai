<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
    <title>Три</title>
</head>

<body>
<jsp:useBean id="mybean" scope="session" class="com.example.lab8.LabHandler" />
<jsp:setProperty name="mybean" property="numbers" />

<h3>Произведение чисел, больших ${mybean.min}: ${mybean.res}</h3>

</body>

</html>