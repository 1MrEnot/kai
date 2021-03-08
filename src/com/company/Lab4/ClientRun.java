package com.company.Lab4;

import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.Scanner;

public class ClientRun {

    public static void main(String[] args) throws FileNotFoundException {
        Scanner scanner = new Scanner(System.in);

        System.out.println("Введите название файла для логов клиента: ");
        String logFileName = scanner.nextLine();

        System.out.println("Введите имя хоста: ");
        String hostName = scanner.nextLine();

        System.out.println("Введите номер порта хоста: ");
        int hostPort = Integer.parseInt(scanner.nextLine());

        Client client = new Client(hostName, hostPort, "MyClient", new PrintWriter(logFileName));

        Thread thread = new Thread(client);
        thread.start();
    }
}
