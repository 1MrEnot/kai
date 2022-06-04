package client;

import compute.Task;

import java.io.Serializable;

public class LabTask implements Task<String>, Serializable {

    private int[] numbers;

    public LabTask(int[] numbers){
        this.numbers = numbers;
    }

    @Override
    public String execute() {
        int chetOtr = 0, nechetPol = 0;

        for (int number : numbers) {
            if (Math.abs(number) % 2 == 0 && number < 0){
                chetOtr++;
            }
            else if(Math.abs(number) % 2 == 1 && number > 0){
                nechetPol++;
            }
        }

        if (chetOtr > nechetPol){
            return "Чётных отрицательных больше нечетных положительных";
        }

        if (chetOtr < nechetPol){
            return "Чётных отрицательных меньше нечетных положительных";
        }

        return "Чётных отрицательных столько же, сколько нечетных положительных";
    }
}
