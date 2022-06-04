package com.example.lab8;

public class LabHandler {
    private String numbers;
    public boolean flag;
    int min = 2;

    public LabHandler(){
        numbers = "";
        flag = false;
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
            int num = Integer.parseInt(el);
            if(num <= min)
                continue;

            res *= num;
        }

        return res;
    }

    public int getMin() {
        return min;
    }

    public boolean getFlag() {
        return flag;
    }
}