package com.company.Lab4;

import java.io.*;
import java.util.Scanner;

public class ServerRun {

    public static void main(String[] args) throws IOException {
        Scanner scanner = new Scanner(System.in);

        System.out.println("Введите название файла для логов сервера: ");
        String logFileName = scanner.nextLine();
        PrintWriter log = new PrintWriter(logFileName);

        BufferedReader reader = new BufferedReader(new FileReader("serverConfig.txt"));
        int serverPort = Integer.parseInt(reader.readLine());

        Runnable r = () -> {
            Server srv = new Server(serverPort, log);
            srv.Run();
        };

        new Thread(r).start();

        scanner.nextLine();
        log.close();
    }
}
