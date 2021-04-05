package labs.Lab4;

import java.io.*;
import java.util.Scanner;

public class ServerRun {

    public static void main(String[] args) throws IOException {
        Scanner scanner = new Scanner(System.in);

        System.out.println("Введите название файла для логов сервера: ");
        String logFileName = scanner.nextLine();
        PrintWriter log = new PrintWriter(new FileWriter(logFileName), true);

        BufferedReader reader = new BufferedReader(new FileReader("serverConfig.txt"));
        int serverPort = Integer.parseInt(reader.readLine());

        int[] forbiddenRows = new int[args.length];
        int i = 0;
        for (String s : args) {
            forbiddenRows[i] = Integer.parseInt(s);
            i++;
        }

        Server srv = new Server(serverPort, log, forbiddenRows);
        srv.Run();
    }
}
