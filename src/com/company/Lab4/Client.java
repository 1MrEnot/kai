package com.company.Lab4;

import java.io.*;
import java.net.Socket;

public class Client implements Runnable {

    public final int port;
    public final String host;
    private final String name;
    private final String exit = "exit";

    BufferedReader input;
    BufferedWriter output;
    BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
    PrintWriter log;

    public Client(String host, int port, String name, PrintWriter log) {
        this.host = host;
        this.port = port;
        this.name = name;
        this.log = log;
    }

    public void run(){

        try{
            Socket socket = new Socket(host, port);
            output = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
            input = new BufferedReader(new InputStreamReader(socket.getInputStream()));

            Log("Клиент " + name + " подключился");

            while(socket.isConnected()){
                String message = reader.readLine();
                if (message.equals(exit)){
                    Log("Exit");
                    break;
                }

                Write(message);

                String response = input.readLine();
                Log(response);
            }

            Log("Соединение разорвано");

        }
        catch (IOException e) {
            Log("Исключение: " + e.toString());
        }
        log.close();
    }

    private void Write(String message) throws IOException {
        output.write(message + "\n");
        output.flush();
    }

    private void Log(String logMessage){
        System.out.println(logMessage);
        log.println(logMessage);
    }
}
