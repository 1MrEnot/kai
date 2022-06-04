package client;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.math.BigDecimal;
import compute.Compute;

public class ComputeTask {

    public static void main(String[] args) {
        if (System.getSecurityManager() == null) {
            System.setSecurityManager(new SecurityManager());
        }

        try {
            String name = "Compute";
            Registry registry = LocateRegistry.getRegistry(args[0]);

            int[] numbers = new int[args.length-1];
            for (int i = 0; i < args.length-1; i++) {
                numbers[i] = Integer.parseInt(args[i+1]);
            }
            LabTask task = new LabTask(numbers);

            Compute comp = (Compute) registry.lookup(name);
            System.out.println(comp.executeTask(task));
        } catch (Exception e) {
            System.err.println("Compute exception:");
            e.printStackTrace();
        }
    }
}
