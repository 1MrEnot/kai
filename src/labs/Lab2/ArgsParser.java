package labs.Lab2;

import labs.Lab2.Exceptions.ArgumentLessException;
import labs.Lab2.Exceptions.BadSymbolException;
import labs.Lab2.Exceptions.FondLiteralException;

public class ArgsParser implements IArgsParser{

    @Override
    public ParsingResult Parse(String[] arguments) throws Exception {

        ParsingResult res = new ParsingResult();

        for (String s: arguments){

            if (s.contains(BadSymbol)){
                throw new BadSymbolException(s);
            }

            int i;
            try{
                i = Integer.parseInt(s);
            }
            catch (Exception ex){
                throw new FondLiteralException(s);
            }


            if (i < MinValue){
                throw new ArgumentLessException(i);
            }

            if (Math.abs(i) % 2 == 0 && i < 0){
                res.ChetOtr++;
            }
            else if(Math.abs(i) % 2 == 1 && i > 0){
                res.NechetPol++;
            }
        }

        return res;

    }
}
