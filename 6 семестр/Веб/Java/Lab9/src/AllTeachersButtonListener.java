import entities.Teacher;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class AllTeachersButtonListener implements ActionListener {

    private final MainForm form;

    public AllTeachersButtonListener(MainForm form){
        this.form = form;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        form.textArea.setText("");

        for (Teacher t: form.teacherModel.teachers) {
            form.textArea.append(t.Name + "\n");
        }
    }
}
