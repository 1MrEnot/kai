import entities.Teacher;
import models.TeacherModel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class TeacherButtonListener implements ActionListener {

    private final MainForm form;

    public TeacherButtonListener(MainForm form){
        this.form = form;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        TeacherModel model = form.teacherModel;

        try{
            Teacher old = TryGetTeacher();

            String name = form.teacherNameField.getText();
            String rank = form.teacherRankField.getText();
            String number = form.teacherNumberField.getText();
            int age = Integer.parseInt(form.teacherAgeField.getText());

            if (old == null){
                Teacher teacher = new Teacher();
                teacher.Id = Integer.parseInt(form.teacherIdField.getText());
                teacher.Name = name;
                teacher.Rank = rank;
                teacher.PhoneNumber = number;
                teacher.Age = age;

                model.AddTeacher(teacher);
            }
            else{
                old.Name = name;
                old.Rank = rank;
                old.PhoneNumber = number;
                old.Age = age;
            }

            model.fireTableDataChanged();
            ClearInput();
        }
        catch (Exception ignored){}
    }

    private Teacher TryGetTeacher(){
        int id = Integer.parseInt(form.teacherIdField.getText());

        for (Teacher t : form.teacherModel.teachers) {
            if (t.Id == id){
                return t;
            }
        }

        return null;
    }

    private void ClearInput(){
        form.teacherIdField.setText("");
        form.teacherNameField.setText("");
        form.teacherRankField.setText("");
        form.teacherNumberField.setText("");
        form.teacherAgeField.setText("");
    }
}