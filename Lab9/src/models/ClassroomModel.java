package models;

import entities.Classroom;

import javax.swing.table.AbstractTableModel;
import java.util.ArrayList;

public class ClassroomModel extends AbstractTableModel {

    public final ArrayList<Classroom> classrooms = new ArrayList<>();
    private final String[] columns = new String[]{
            "Id",
            "Номер здания",
            "Номер аудитории",
            "Название",
            "Площадь",
            "Id ответсвенного"
    };

    @Override
    public int getRowCount() {
        return classrooms.size();
    }

    @Override
    public int getColumnCount() {
        return columns.length;
    }

    @Override
    public Object getValueAt(int rowIndex, int columnIndex) {
        Classroom classroom = classrooms.get(rowIndex);

        switch (columnIndex){
            case 0:
                return classroom.Id;
            case 1:
                return classroom.BuildingNumber;
            case 2:
                return classroom.Number;
            case 3:
                return classroom.Name;
            case 4:
                return classroom.Area;
            case 5:
                return classroom.TeacherId;
            default:
                return null;
        }
    }

    @Override
    public String getColumnName(int column) {
        return columns[column];
    }

    public void AddClassroom(Classroom classroom){
        classrooms.add(classroom);
    }
}
