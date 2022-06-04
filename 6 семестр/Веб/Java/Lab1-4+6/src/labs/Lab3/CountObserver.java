package labs.Lab3;

public class CountObserver {

    private int count = 0;

    public int GetCount(){
        return count;
    }

    public void Trigger(Object arg) {
        count++;
    }


}
