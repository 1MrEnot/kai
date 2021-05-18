import entities.Classroom;
import entities.Teacher;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.InputStreamReader;

public class ResetButtonListener implements ActionListener {

    private final MainForm form;

    public ResetButtonListener(MainForm form){
        this.form = form;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        try{
            String path = "reset.txt";
            FileInputStream fstream = new FileInputStream(path);
            BufferedReader br = new BufferedReader(new InputStreamReader(fstream));

            String cName = br.readLine();
            double cArea = Double.parseDouble(br.readLine());
            int cNumber = Integer.parseInt(br.readLine());
            ResetClasses(cName, cArea, cNumber);

            String tName = br.readLine();
            String tNumber = br.readLine();
            String tRank = br.readLine();
            int tAge = Integer.parseInt(br.readLine());
            ResetTeachers(tName, tNumber, tRank, tAge);
        }

        catch (Exception ignored){}

        form.classroomModel.fireTableDataChanged();
        form.teacherModel.fireTableDataChanged();
    }

    private void ResetClasses(String name, double area, int number){
        for (Classroom c: form.classroomModel.classrooms) {
            c.Name = name;
            c.Area = area;
            c.Number = number;
        }
    }

    private void ResetTeachers(String name, String number, String rank, int age){
        for (Teacher t: form.teacherModel.teachers) {
            t.Name = name;
            t.Age = age;
            t.PhoneNumber = number;
            t.Rank = rank;
        }
    }
}