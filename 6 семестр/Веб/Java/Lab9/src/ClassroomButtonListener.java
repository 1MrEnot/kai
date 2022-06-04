import entities.Classroom;
import models.ClassroomModel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

class ClassroomButtonListener implements ActionListener {

    private final MainForm form;

    public ClassroomButtonListener(MainForm form){
        this.form = form;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        ClassroomModel model = form.classroomModel;

        try{
            Classroom old = TryGetClassroom();

            int teacherIndex = form.teacherTable.getSelectedRow();
            int teacherId = form.teacherModel.teachers.get(teacherIndex).Id;

            int id = Integer.parseInt(form.classIdField.getText());
            int number = Integer.parseInt(form.classNumberField.getText());
            int buildingNumber = Integer.parseInt(form.classBuildingNumberField.getText());
            String name = form.classNameField.getText();
            double area = Double.parseDouble(form.classAreaField.getText());

            if (old == null){
                Classroom classRoom = new Classroom();
                classRoom.Id = id;
                classRoom.Number = number;
                classRoom.BuildingNumber = buildingNumber;
                classRoom.Name = name;
                classRoom.Area = area;
                classRoom.TeacherId = teacherId;

                model.AddClassroom(classRoom);
            }
            else{
                old.Number = number;
                old.BuildingNumber = buildingNumber;
                old.Name = name;
                old.Area = area;
                old.TeacherId = teacherId;
            }

            model.fireTableDataChanged();
            ClearInput();
        }
        catch (Exception ignored){}
    }

    private Classroom TryGetClassroom(){
        int id = Integer.parseInt(form.classIdField.getText());

        for (Classroom c : form.classroomModel.classrooms) {
            if (c.Id == id){
                return c;
            }
        }

        return null;
    }

    private void ClearInput(){
        form.classIdField.setText("");
        form.classNumberField.setText("");
        form.classBuildingNumberField.setText("");
        form.classNameField.setText("");
        form.classAreaField.setText("");
    }
}