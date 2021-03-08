package com.company.Lab3;

import java.io.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class Lab3 {

    private static List<String> rows = new ArrayList<>();
    private static String logFileName;

    public static void Run(String[] args){

        try{
            CountObserver getConsoleObserver = new CountObserver();
            CountObserver writeConsoleObserver = new CountObserver();
            CountObserver varObserver = new CountObserver();

            ObservableConsole console = new ObservableConsole(getConsoleObserver, writeConsoleObserver);

            String filename = console.GetString();
            ReadFile(filename);
            File logFile = CreateLogFile(logFileName);
            PrintWriter logPrintWriter = new PrintWriter(logFile.getAbsoluteFile());
            console.SetPrintWriter(logPrintWriter);

            ObservableVar<Integer> chetOtr = new ObservableVar<>(varObserver, 0);
            int nechetPol = 0;

            if (rows.size() == 0){
                String line = new Scanner(System.in).nextLine();
                rows = Arrays.asList(line.split("\\s"));
            }

            for (String row: rows) {
                int num = Integer.parseInt(row);
                if (Math.abs(num) % 2 == 0 && num <= 0){
                    chetOtr.SetVar(chetOtr.GetVar() + 1);
                }
                if (Math.abs(num) % 2 == 1 && num > 0){
                    nechetPol++;
                }
            }

            console.WriteLine("Chet < 0: " + chetOtr.GetVar());
            console.WriteLine("Nechet > 0: " + nechetPol);

            console.WriteLine("Var chahged times: " + varObserver.GetCount());
            console.WriteLine("Read from console times: " + getConsoleObserver.GetCount());
            console.WriteLine("Printed to console times: " + writeConsoleObserver.GetCount());

            logPrintWriter.close();
        }
        catch (Exception ex){
            System.out.println(ex.getMessage());
        }
    }

    private static void ReadFile(String filename) throws Exception{
        File f = new File(filename);
        BufferedReader reader = new BufferedReader(new FileReader(f.getAbsoluteFile()));
        String s;
        logFileName = reader.readLine();
        while ((s = reader.readLine()) != null){
            rows.add(s);
        }
    }

    private static File CreateLogFile(String logFileName) throws Exception {
        File log = new File(logFileName);
        if (!log.exists()){
            log.createNewFile();
        }
        return log;
    }
}
