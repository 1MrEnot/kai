import entities.Classroom;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class BuildingClassroomButtonListener implements ActionListener {

    private final MainForm form;

    public BuildingClassroomButtonListener(MainForm form){
        this.form = form;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        try{
            form.textArea.setText("");

            int teacherId = Integer.parseInt(form.teacherClassesField.getText());
            for (Classroom c: form.classroomModel.classrooms) {
                if(c.TeacherId == teacherId){
                    form.textArea.append(c.Number + "\n");
                }
            }
        }

        catch (Exception ignored){}
    }
}
