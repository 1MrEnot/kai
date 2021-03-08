package lab.Lab_3;

import java.io.FileWriter;
import java.io.IOException;
import java.util.Observable;

public class ObservableOutFile extends Observable {

    private final FileWriter writer;

    public ObservableOutFile(FileWriter writer){
        this.writer = writer;
    }

    public void WriteLine(String line) throws IOException {
        setChanged();
        notifyObservers("Записано в файл: " + line);
        writer.write(line + '\n');
    }

    public void Close() throws IOException {
        writer.close();
    }
}
