package labs.Lab6;

import labs.Lab6.figures.*;
import labs.Lab6.figures.Rectangle;

import java.awt.*;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

import java.util.LinkedList;
import java.awt.event.*;


// Два окна: управляющее и холст
// Из верхнего левого в случайном направлении, отражается
// Закрывать при закрытии любого окна

// 0001011111

// Число ФиО                       : Любое
// ФиО                             : Фигуры
// Задание цвета текста и заливки  : Выпадающий список
// Выбор запускаемого ФиО          : Из текстового поля
// Задание начальной скорости      : Указание в поле
// Способ выбора летящего ФиО      : Из текстового поля где вводится номер
// Номер ФиО                       : Вручную
// Возможность смены номера        : Да
// Регулировка скорости перемещения: Из выпадающего списка (5 скоростей)
// Изменение размера окна          : Да


class LabCanvas extends Frame implements ActionListener, ItemListener, Runnable {
    private final LinkedList<BaseFigure> Figures = new LinkedList<>();

    private final Frame frame;

    private final Choice colorSelector;
    private final Choice speedSelector;

    private final TextField figureTextField;
    private final TextField figureSpeedTextField;
    private final TextField figureIdField;
    private final TextField figureNewIdField;


    private final String launchCommand = "LAUNCH";
    private final Button launchButton;

    private final String changeCommand = "CHANGE";
    private final Button changeButton;

    private int Width = 640;
    private int Height = 480;

    private final int tick = 100;

    public LabCanvas(){

        frame = new Frame();
        frame.setSize(new Dimension(500, 100));
        frame.setTitle("Контроль");
        frame.setLayout(new GridLayout());
        frame.addWindowListener(new ClosingWindowAdapter());

        launchButton = new Button("Пуск");
        launchButton.setActionCommand(launchCommand);
        launchButton.addActionListener(this);
        frame.add(launchButton);

        figureTextField = new TextField();
        frame.add(figureTextField);

        figureSpeedTextField = new TextField();
        frame.add(figureSpeedTextField);


        colorSelector = new Choice();
        colorSelector.addItem("Синий");
        colorSelector.addItem("Зелёный");
        colorSelector.addItem("Красный");
        colorSelector.addItem("Чёрный");
        colorSelector.addItem("Жёлтый");
        colorSelector.addItemListener(this);
        frame.add(colorSelector);

        speedSelector = new Choice();
        speedSelector.addItem("1");
        speedSelector.addItem("2");
        speedSelector.addItem("3");
        speedSelector.addItem("4");
        speedSelector.addItem("5");
        speedSelector.addItemListener(this);
        frame.add(speedSelector);

        figureIdField = new TextField();
        frame.add(figureIdField);

        changeButton = new Button("Изменить");
        changeButton.setSize(new Dimension(10,10));
        changeButton.setActionCommand(changeCommand);
        changeButton.addActionListener(this);
        frame.add(changeButton);

        figureNewIdField = new TextField();
        frame.add(figureNewIdField);

        frame.setVisible(true);

        this.addWindowListener(new ClosingWindowAdapter());
        this.setSize(Width,Height);
        this.setVisible(true);
        this.setLocation(100, 150);
        this.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {
                Width = e.getComponent().getWidth();
                Height = e.getComponent().getHeight();
            }
        });
    }

    public void paint (Graphics g) {
        for (BaseFigure fig: Figures) {
            fig.Draw(g);
        }
    }

    public void itemStateChanged (ItemEvent iE) {}

    public void actionPerformed (ActionEvent aE) {
        if (aE.getActionCommand().equals(launchCommand)){
            LaunchFigure();
        }
        else if (aE.getActionCommand().equals(changeCommand)){
            ChangeFigure();
        }
    }

    @Override
    public void run() {
        while (true){
            for (BaseFigure figure :Figures) {
                figure.Move(Width, Height);
            }

            try {
                Thread.sleep(tick);
            } catch (InterruptedException ignored) {}

            repaint();
        }
    }

    private static double[] GetRandomSpeed(int v){
        double angle = Math.toRadians(Math.random() * 360);
        return new double[]{
                v * Math.sin(angle),
                - v * Math.cos(angle)
        };
    }

    private Color GetColor(){
        switch (colorSelector.getSelectedIndex()) {
            case 0:
                return Color.blue;
            case 1:
                return Color.green;
            case 2:
                return Color.red;
            case 3:
                return Color.black;
            case 4:
                return Color.yellow;
            default:
                return Color.CYAN;
        }
    }

    private int GetNewSpeed(){
        return Integer.parseInt(speedSelector.getSelectedItem());
    }

    private void LaunchFigure(){
        try{
            Color figureColor = GetColor();
            int velocity = Integer.parseInt(figureSpeedTextField.getText());

            double[] velocityXY = GetRandomSpeed(velocity);

            BaseFigure figure = null;

            String text = figureTextField.getText();

            int id = Integer.parseInt(figureIdField.getText());

            for (BaseFigure f : Figures) {
                if (f.getId() == id){
                    return;
                }
            }

            switch (text){
                case "Круг":
                    figure = new Circle(id, velocityXY[0], velocityXY[1], figureColor);
                    break;
                case "Треугольник":
                    figure = new Triangle(id, velocityXY[0], velocityXY[1], figureColor);
                    break;
                case "Квадрат":
                    figure = new Square(id, velocityXY[0], velocityXY[1], figureColor);
                    break;
                case "Овал":
                    figure = new Oval(id, velocityXY[0], velocityXY[1], figureColor);
                    break;
                case "Прямоугольник":
                    figure = new Rectangle(id, velocityXY[0], velocityXY[1], figureColor);
                    break;
            }

            if (figure != null){
                Figures.add(figure);
            }
        }
        catch (Exception ignored){}
    }

    private void ChangeFigure(){
        BaseFigure selectedFigure;

        // Get figure
        try{
            selectedFigure = TryGetById(Integer.parseInt(figureIdField.getText()));
            if (selectedFigure == null){
                return;
            }
        }
        catch (Exception e){
            return;
        }

        // Reset id
        try{
            int newId = Integer.parseInt(figureNewIdField.getText());
            if (TryGetById(newId) == null){
                selectedFigure.setId(newId);
            }
        }
        catch (Exception ignored){}

        // Reset color
        selectedFigure.setColor(GetColor());

        // Reset velocity
        double oldSpeed = Math.sqrt(Math.pow(selectedFigure.getvX(), 2) + Math.pow(selectedFigure.getvY(), 2));
        double speedRatio = GetNewSpeed()/oldSpeed;

        selectedFigure.setvX(selectedFigure.getvX() * speedRatio);
        selectedFigure.setvY(selectedFigure.getvY() * speedRatio);
    }

    private BaseFigure TryGetById(int id){
        for (BaseFigure fig: Figures) {
            if (fig.getId() == id){
                return fig;
            }
        }
        return null;
    }
}

class ClosingWindowAdapter extends WindowAdapter {
    public void windowClosing (WindowEvent wE) {
        System.exit (0);
    }
}
