package com.example.lab8;

public class LabHandler {
    private String numbers;
    int min = 2;

    public LabHandler(){
        numbers = "";
    }

    public String getNumbers() {
        return numbers;
    }

    public void setNumbers(String numbers) {
        this.numbers = numbers;
    }

    public int getRes(){
        int res = 1;

        String[] splited = numbers.split("\\s+");

        for (String el: splited) {
            int num = Integer.getInteger(el);
            if(num < min)
                continue;

            res *= num;
        }

        return res;
    }

    public int getMin() {
        return min;
    }
}