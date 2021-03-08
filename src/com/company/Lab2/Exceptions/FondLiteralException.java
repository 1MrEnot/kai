package com.company.Lab2.Exceptions;

public class FondLiteralException extends Exception{
    private final String literal;

    public FondLiteralException(String l){
        literal = l;
    }

    public String getMessage(){
        return "Fond literal " + literal;
    }
}
