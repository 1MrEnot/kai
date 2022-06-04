package labs.Lab4;

import java.io.*;
import java.net.Socket;
import java.util.Arrays;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

class Listener implements Runnable {
    Socket socket;
    Server server;

    BufferedReader input;
    BufferedWriter output;

    Pattern pattern = Pattern.compile("(\\w+)\\[(\\d)]\\[(\\d)](\\s*=\\s*(.*))?");

    PrintWriter log;


    public Listener(Socket socket, Server server, PrintWriter log) throws FileNotFoundException {
        this.socket = socket;
        this.server = server;
        this.log = log;
    }

    public void run() {
        try {

            output = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
            input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            Log("Слушатель запущен");

            String command, response;

            try{
                while (true){
                    command = input.readLine();
                    response = Process(command);

                    Log("GOT: " + command);
                    Log("SENT: " + response);
                    Write(response);
                }
            }
            finally {
                socket.close();
                input.close();
                output.close();
            }

        } catch (IOException e) {
            Log("Исключение: " + e.toString());
        }
    }

    private void Write(String message) throws IOException {
        output.write(message + "\n");
        output.flush();
    }

    // int[2][2]
    // double[3][1]=4.2
    private String Process(String command){
        Matcher matcher = pattern.matcher(command);

        String res = "FAIL";

        if (!matcher.find()){
            return res;
        }

        try{
            String type = matcher.group(1);
            int x = Integer.parseInt(matcher.group(2));
            int y = Integer.parseInt(matcher.group(3));

            String value = matcher.group(5);

            // SET
            if (value != null){
                String arrValue = "";
                boolean setResult = false;

                try{
                    if (type.equals("int")){
                        setResult = SetInt(x, y, value);
                        arrValue = GetInts();
                    }
                    if (type.equals("double")){
                        setResult = SetDouble(x, y, value);
                        arrValue = GetDoubles();
                    }
                    if (type.equals("string")) {
                        setResult = SetString(x, y, value);
                        arrValue = GetStrings();
                    }
                }
                catch (Exception e){
                    return "INVALID COMMAND";
                }

                if (!setResult){
                    return "YOU HAVE NO PERMISSIONS";
                }

                return "SUCCESS: " + arrValue;

            }
            // GET
            else{
                if (type.equals("int")){
                    return Integer.toString(GetInt(x, y));
                }
                if (type.equals("double")){
                    return Double.toString(GetDouble(x, y));
                }
                if (type.equals("string")) {
                    return GetString(x, y);
                }
            }

        }
        catch (Exception ignored){

        }

        return res;
    }

    private int GetInt(int x, int y){
        return server.integers[x][y];
    }

    private double GetDouble(int x, int y){
        return server.doubles[x][y];
    }

    private String GetString(int x, int y){
        return server.strings[x][y];
    }

    private boolean CanSet(int x, int y){
        return Arrays.stream(server.forbiddenRows).noneMatch(el -> el == x);
    }

    private boolean SetInt(int x, int y, String value){
        if (!CanSet(x, y)){
            return false;
        }

        server.integers[x][y] = Integer.parseInt(value);
        return true;
    }

    private boolean SetDouble(int x, int y, String value){
        if (!CanSet(x, y)){
            return false;
        }

        server.doubles[x][y] = Double.parseDouble(value);
        return true;
    }

    private boolean SetString(int x, int y, String value){
        if (!CanSet(x, y)){
            return false;
        }

        server.strings[x][y] = value;
        return true;
    }

    private String GetInts(){
        return "Int's: " + Arrays.deepToString(server.integers);
    }

    private String GetDoubles(){
        return "Double's: " + Arrays.deepToString(server.doubles);
    }

    private String GetStrings(){
        return "String's: " + Arrays.deepToString(server.strings);
    }

    private void Log(String logMessage){
        System.out.println(logMessage);
        log.println(logMessage);
    }
}