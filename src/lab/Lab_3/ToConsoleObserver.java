package lab.Lab_3;

import java.util.Observable;
import java.util.Observer;

public class ToConsoleObserver implements Observer {

    @Override
    public void update(Observable o, Object arg) {
        System.out.println("Event: " + arg);
    }
}
