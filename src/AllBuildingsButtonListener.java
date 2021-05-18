import entities.Classroom;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

public class AllBuildingsButtonListener implements ActionListener {

    private final MainForm form;

    public AllBuildingsButtonListener(MainForm form){
        this.form = form;
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        form.textArea.setText("");
        ArrayList<Integer> buildings = new ArrayList<>();
        for (Classroom c: form.classroomModel.classrooms) {
            if(!buildings.contains(c.BuildingNumber)){
                buildings.add(c.BuildingNumber);
            }
        }

        for (int num : buildings) {
            form.textArea.append(num + "\n");
        }
    }
}
