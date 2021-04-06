package labs.Lab6.figures;

import java.awt.*;

public class Circle extends BaseFigure {

    public static final int Size = 20;

    public Circle(int Id, double vX, double vY, Color color) {
        super(Id, vX, vY, color, Size, Size);
    }

    @Override
    public void Draw(Graphics g) {
        g.setColor(Color);
        g.drawOval(getRoundX(), getRoundY(), Width, Height);
        g.drawString(Integer.toString(getId()), getRoundX(), getRoundY());
    }
}
