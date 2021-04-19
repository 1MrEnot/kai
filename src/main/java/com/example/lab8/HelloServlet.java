package com.example.lab8;

import java.io.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;

/*
Старовая страница - информация о задании                                      | V
Главная страница - передача параметров для вычисления задания из 1 лабы       | V
Финишная страница - показ результатов                                         | X

Код функции                - в bean                                           | V
Формат старотовой страницы - текст задания, ФИО, ссыль на главную страницу    | V
Вывод результатов          - в видимой таблицу                                | X
При возврате на главную    - изменение триггера в Bean-компоненте             | X
 */

@WebServlet(name = "helloServlet", value = "/hello-servlet")
public class HelloServlet extends HttpServlet {
    private String message;

    public void init() {
        message = "Hello World!";
    }

    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");

        // Hello
        PrintWriter out = response.getWriter();
        out.println("<html><body>");
        out.println("<h1>" + message + "</h1>");
        out.println("</body></html>");
    }

    public void destroy() {
    }
}