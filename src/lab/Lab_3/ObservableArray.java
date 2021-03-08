package lab.Lab_3;

import java.util.Arrays;
import java.util.Observable;

public class ObservableArray<T> extends Observable {

    private final T[] array;

    public ObservableArray(T[] array){
        this.array = array;
    }

    public T[] GetArray(){
        setChanged();
        notifyObservers("Доступ к массиву " + Arrays.toString(array));
        return array;
    }
}
