package models;

import entities.Teacher;

import javax.swing.table.AbstractTableModel;
import java.util.ArrayList;

public class TeacherModel extends AbstractTableModel {

    public final ArrayList<Teacher> teachers = new ArrayList<>();
    private final String[] columns = new String[]{
            "Id",
            "ФИО",
            "Должность",
            "Телефон",
            "Возраст"
    };

    @Override
    public int getRowCount() {
        return teachers.size();
    }

    @Override
    public int getColumnCount() {
        return columns.length;
    }

    @Override
    public Object getValueAt(int rowIndex, int columnIndex) {
        Teacher teacher = teachers.get(rowIndex);

        switch (columnIndex){
            case 0:
                return teacher.Id;
            case 1:
                return teacher.Name;
            case 2:
                return teacher.Rank;
            case 3:
                return teacher.PhoneNumber;
            case 4:
                return teacher.Age;
            default:
                return null;
        }
    }

    @Override
    public String getColumnName(int column) {
        return columns[column];
    }

    public void AddTeacher(Teacher teacher){
        teachers.add(teacher);
    }
}
