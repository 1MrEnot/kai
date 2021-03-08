package com.company.Lab3;

import java.io.PrintWriter;
import java.util.Scanner;

public class ObservableConsole {

    private final Scanner scanner;
    private final CountObserver getObserver;
    private final CountObserver writeObserver;
    private PrintWriter printWriter;

    public ObservableConsole(CountObserver getObserver, CountObserver writeObserver){
        scanner = new Scanner(System.in);
        this.getObserver = getObserver;
        this.writeObserver = writeObserver;
    }

    public String GetString(){
        String line = scanner.nextLine();
        getObserver.Trigger(line);

        return line;
    }

    public void WriteLine(String line){
        writeObserver.Trigger(line);
        System.out.println(line);
        printWriter.println(line);
    }

    public void SetPrintWriter(PrintWriter printWriter){
        this.printWriter = printWriter;
    }

}
