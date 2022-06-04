package labs.Lab3;

public class ObservableVar<T> {

    private final CountObserver observer;
    private T variable;

    public ObservableVar(CountObserver obs, T var){
        observer = obs;
        variable = var;
    }

    public T GetVar(){
        return variable;
    }

    public void SetVar(T newVal){
        observer.Trigger(variable);
        variable = newVal;
    }
}
