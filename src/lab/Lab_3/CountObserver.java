package lab.Lab_3;

import java.util.Observable;
import java.util.Observer;

public class CountObserver implements Observer {

    private int eventCount = 0;

    @Override
    public void update(Observable o, Object arg) {
        eventCount += 1;
    }

    public int getEventCount() {
        return eventCount;
    }
}
