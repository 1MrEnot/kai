import entities.Classroom;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class TeacherClassroomsButtonListener implements ActionListener {

    private final MainForm form;

    public TeacherClassroomsButtonListener(MainForm form){
        this.form = form;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        try{
            form.textArea.setText("");

            int buildingNumber = Integer.parseInt(form.classesInBuildingField.getText());
            for (Classroom c: form.classroomModel.classrooms) {
                if(c.BuildingNumber == buildingNumber){
                    form.textArea.append(c.Number + "\n");
                }
            }
        }

        catch (Exception ignored){}
    }
}