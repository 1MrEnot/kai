package com.company.Lab2;

public class Printer implements IPrinter{

    @Override
    public void Print(ParsingResult result) {

        if (result.ChetOtr > result.NechetPol){
            System.out.println("Chet otr > nechet pol");
        }
        else if (result.ChetOtr < result.NechetPol){
            System.out.println("Chet otr < nechet pol");
        }
        else{
            System.out.println("Porovnu");
        }
    }
}
