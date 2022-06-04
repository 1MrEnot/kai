package labs.Lab2;

public class Lab2 {

    public static void Run(String[] args){
        IArgsParser parser = new ArgsParser();
        IPrinter printer = new Printer();

        ParsingResult res;

        try{
            res = parser.Parse(args);
            printer.Print(res);
        }
        catch (Exception ex){
            System.out.println("RAISED EXCEPTION: " + ex.getMessage());
        }
        finally {
            System.out.println("Finished");
        }

    }
}
