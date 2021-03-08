package lab.Lab_3;

import java.io.*;
import java.util.ArrayList;
import java.util.Observer;

public class Main {

    private static final int M = 13;

    public static void main(String[] args){

        // Создание консоли, наблюдается чтение, дублирование в консоль
        ObservableConsole console = new ObservableConsole();
        Observer consoleObserver = new ToConsoleObserver();
        console.addObserver(consoleObserver);

        // Получнеие пути к файлу с логами
        String logFileName = console.GetString();

        try{
            // Создание файла для логов, наблюдается запись, идёт подсчёт
            ObservableOutFile file = new ObservableOutFile(new FileWriter(logFileName));
            CountObserver countObserver = new CountObserver();
            file.addObserver(countObserver);

            // Получение пути к файлу с данными
            console.WriteLine("Введите путь к файлу с данными");
            file.WriteLine("Введите путь к файлу с данными");
            String dataFileName = console.GetString();

            // Сохранение чисел из файла, больших М
            File f = new File(dataFileName);
            BufferedReader reader = new BufferedReader(new FileReader(f.getAbsoluteFile()));
            ArrayList<Integer> numList = new ArrayList<>();
            String s;
            while ((s = reader.readLine()) != null){
                if (Integer.parseInt(s) > M)
                    numList.add(Integer.parseInt(s));
            }

            // Преобразование чисел в наблюдаемый массив, подсчёт обращений
            Integer[] nums = numList.toArray(new Integer[0]);
            ObservableArray<Integer> obsNums = new ObservableArray<>(nums);
            CountObserver arrObserver = new CountObserver();
            obsNums.addObserver(arrObserver);

            // Подчет произведения
            if(obsNums.GetArray().length == 0){
                console.WriteLine("Нет чисел больше М");
                file.WriteLine("Нет чисел больше М");
            }
            else{
                int res = 1;
                for (int x: obsNums.GetArray()) {
                    res = res * x;
                }
                console.WriteLine(Integer.toString(res));
                file.WriteLine(Integer.toString(res));
            }

            // Вывод статистики обращений к массиву
            int arrayEventCount = arrObserver.getEventCount();
            console.WriteLine("Обращений к массиву: " + arrayEventCount);
            file.WriteLine("Обращений к массиву: " + arrayEventCount);

            // Вывод статистики вывода в файл
            int fileEventCount = countObserver.getEventCount();
            console.WriteLine("В файл было записано строк: " + fileEventCount);
            file.WriteLine("В файл было записано строк: " + fileEventCount);
            file.Close();
        }
        catch (Exception e){
            System.out.println(e.getMessage());
        }
    }
}
