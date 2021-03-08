package com.company.Lab2;

public interface IArgsParser {
    int MinValue = -4;
    String BadSymbol = "1";

    ParsingResult Parse(String[] arguments) throws Exception;
}
