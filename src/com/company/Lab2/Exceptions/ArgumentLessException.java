package com.company.Lab2.Exceptions;

public class ArgumentLessException extends Exception{

    private final int arg;

    public ArgumentLessException(int a){
        arg = a;
    }

    public String getMessage(){
        return "Got too small number " + arg;
    }
}
