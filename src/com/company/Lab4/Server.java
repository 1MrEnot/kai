package com.company.Lab4;

import java.io.*;
import java.net.*;

public class Server {
    public final int port;
    private ServerSocket serverSocket;
    private PrintWriter log;

    int[][] integers = {
        {1, 2},
        {3, 4},
        {5, 6}
    };

    double[][] doubles = {
            {1.2, 3.4},
            {5.6, 7.8},
            {9.1, 2.3}
    };

    String[][] strings = {
            {"foo", "bar"},
            {"bazz", "quux"},
            {"spam", "eggs"}
    };

    public Server(int port, PrintWriter log) {
        this.port = port;
        this.log = log;
        try{
            serverSocket = new ServerSocket(port);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void Run() {
        System.out.println("Сервер запущен на порту " + port);

        while (true) {
            try {
                Socket socket = serverSocket.accept();
                Listener listener = new Listener(socket, this, log);
                Thread thread = new Thread(listener);
                thread.run();
            }catch(IOException e){
                System.err.println("Исключение: " + e.toString());
            }
        }
    }
}