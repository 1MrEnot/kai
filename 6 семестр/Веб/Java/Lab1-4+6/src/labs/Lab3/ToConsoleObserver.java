package labs.Lab3;

import java.io.PrintWriter;

public class ToConsoleObserver {

    private final PrintWriter writer;
    private final ObservableConsole observableConsole;

    public ToConsoleObserver(PrintWriter w, ObservableConsole con){
        writer = w;
        observableConsole = con;
    }

    public void GotString(Object arg) {
        String mes = "Got from console: " + arg;
        observableConsole.WriteLine(mes);
        writer.println(mes);
    }

    public void WroteString(Object arg){
        String mes = "Wrote to console: " + arg;
        observableConsole.WriteLine(mes);
        writer.println(mes);
    }

    public void ChangeVariable(Object was, Object now){
        String mes = "Changed value from '" + was + "' to '" + now + "'";
        observableConsole.WriteLine(mes);
        writer.println(mes);
    }
}
