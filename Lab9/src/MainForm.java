import models.ClassroomModel;
import models.TeacherModel;

import javax.swing.*;
    
public class MainForm extends JFrame {
    public JPanel root;
    public JTable classroomTable;
    public JTable teacherTable;
    public JScrollPane classroomScroll;
    public JScrollPane teacherScroll;

    public JButton classroomButton;
    public JTextField classIdField;
    public JTextField classBuildingNumberField;
    public JTextField classNumberField;
    public JTextField classNameField;
    public JTextField classAreaField;

    public JButton teacherButton;
    public JTextField teacherIdField;
    public JTextField teacherNumberField;
    public JTextField teacherAgeField;
    public JTextField teacherRankField;
    public JTextField teacherNameField;

    public JButton allTeachersButton;
    public JButton allBuildingsButton;
    public JTextArea textArea;
    public JButton classesInBuildingButton;
    public JTextField classesInBuildingField;
    public JButton teacherClassesButton;
    public JTextField teacherClassesField;
    private JButton resetButton;

    public final ClassroomModel classroomModel = new ClassroomModel();
    public final TeacherModel teacherModel = new TeacherModel();

    public MainForm(String name){
        super(name);
        add(root);
        super.setSize(640, 480);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        classroomTable.setModel(classroomModel);
        classroomButton.addActionListener(new ClassroomButtonListener(this));

        teacherTable.setModel(teacherModel);
        teacherButton.addActionListener(new TeacherButtonListener(this));

        allTeachersButton.addActionListener(new AllTeachersButtonListener(this));
        allBuildingsButton.addActionListener(new AllBuildingsButtonListener(this));
        classesInBuildingButton.addActionListener(new BuildingClassroomButtonListener(this));
        teacherClassesButton.addActionListener(new TeacherClassroomsButtonListener(this));
        resetButton.addActionListener(new ResetButtonListener(this));
    }
}
