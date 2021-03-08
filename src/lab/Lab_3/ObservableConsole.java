package lab.Lab_3;

import java.util.Observable;
import java.util.Scanner;

public class ObservableConsole extends Observable {

    private final Scanner scanner;

    public ObservableConsole(){
        scanner = new Scanner(System.in);
    }

    public String GetString(){
        String line = scanner.nextLine();
        setChanged();
        notifyObservers("Прочитана строка: " + line);
        return line;
    }

    public void WriteLine(String str){
        System.out.println(str);
    }
}
