package com.example.lab7;

import java.io.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;

/*
1) При перезагрузке отображать значение счётчика обращений к странице после запуска                           | V
2) Организовать вывод результатов работы сервлета в видимой таблице с произвольным числом столбцов и строк    | V
3) При обновлении страицы уменьшать размер текста до заданной величины,
   а на странице уведомлять, что дальнейшее уменьшение невозможно                                             | V
4) Релизовать возможность сброса размера текста в таблице через параметр строки url запроса
   до значения по умолчанию                                                                                   | V
5) Передавать в сервлет фио и номер группы, отображать на странице                                            | V
6) Изменить порт на любой                                                                                     | V
 */


@WebServlet(urlPatterns = {"/LabServlet"})
public class HelloServlet extends HttpServlet {
    final int defaultFontSize = 2;
    final int minFontSize = 4;

    long counter;
    int fontSize;

    public HelloServlet() {
        counter = 0;
        fontSize = defaultFontSize;
    }

    protected void processRequest(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {
            out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<head>");
            out.println("<title>Lab7</title>");
            out.println("</head>");
            out.println("<body>");
            out.println("<h1>ServletApp" + request.getServletPath() + "</h1>");
            out.println("<h3>Еремеев Максим 4311</h3>");

            counter++;
            fontSize = minFontSize;

            String resetValue = request.getParameter("reset");

            if(resetValue != null && resetValue.equals("true")){
                fontSize = defaultFontSize;
            }

            out.println("<h3>Обращений к странице : " + counter + "</h3>");

            String[][] tbl = new String[3][3];
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    tbl[i][j] = (i + 1) + "&" + (j + 1);
                }
            }

            String prm1 = request.getParameter("prm1");
            String prm2 = request.getParameter("prm2");
            String prm3 = request.getParameter("prm3");

            out.println("<h" + fontSize + "><table border>" +
                    "<tr>" + "<td>" + tbl[0][0] + "</td>" + "<td>prm1=" + prm1 + "</td>" + "<td>1.3</td>" +
                    "<tr>" + "<td>prm2=" + prm2 + "</td>" + "<td>pr2</td>" + "<td>2.3</td>" +
                    "<tr>" + "<td>prm3=" + prm3 + "</td>" + "<td>3.2</td>" + "<td>" + tbl[2][2] + "</td>" +
                    "</tr>"
                    + "</table></h" + fontSize + ">");

            if(fontSize == minFontSize){
                out.println("<h5>Достигнут минимальный размер страницы</h3>");
            }

            out.println("</body>");
            out.println("</html>");
        }
    }

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        processRequest(request, response);
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
        processRequest(request, response);
    }

    @Override
    public String getServletInfo() {
        return "Short description";
    }
}