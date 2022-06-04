package labs.Lab4;

import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Scanner;

public class ClientRun {

    public static void main(String[] args) throws IOException {
        Scanner scanner = new Scanner(System.in);

        System.out.println("Введите название файла для логов клиента: ");
        String logFileName = scanner.nextLine();

        System.out.println("Введите имя хоста: ");
        String hostName = scanner.nextLine();

        System.out.println("Введите номер порта хоста: ");
        int hostPort = Integer.parseInt(scanner.nextLine());

        PrintWriter log = new PrintWriter(new FileWriter(logFileName), true);

        Client client = new Client(hostName, hostPort, "MyClient",log);

        Thread thread = new Thread(client);
        thread.start();
    }
}
