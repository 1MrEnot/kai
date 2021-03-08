package com.company.Lab2.Exceptions;

public class BadSymbolException extends Exception{

    private final String badSymbol;

    public BadSymbolException(String s) {
        badSymbol = s;
    }

    public String getMessage(){
        return "Got bad symbol in string: " + badSymbol;
    }
}
