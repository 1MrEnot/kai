package com.company.Lab1;

public class Lab1 {

    public static void Run(String[] args){
        int chetOtr = 0;
        int nechetPol = 0;

        for (String s: args) {
            int i = Integer.parseInt(s);

            if (Math.abs(i) % 2 == 0 && i < 0){
                chetOtr++;
            }
            else if(Math.abs(i) % 2 == 1 && i > 0){
                nechetPol++;
            }
        }

        if (chetOtr > nechetPol){
            System.out.println("Chet otr > nechet pol");
        }
        else if (chetOtr < nechetPol){
            System.out.println("Chet otr < nechet pol");
        }
        else{
            System.out.println("Porovnu");
        }
    }


}
