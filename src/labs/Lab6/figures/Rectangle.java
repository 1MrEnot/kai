package labs.Lab6.figures;

import java.awt.*;

public class Rectangle extends BaseFigure {

    public static final int W = 20;
    public static final int H = 30;

    public Rectangle(int id, double vX, double vY, Color color) {
        super(id, vX, vY, color, W, H);
    }

    @Override
    public void Draw(Graphics g) {
        g.setColor(Color);
        g.drawRect(getRoundX(), getRoundY(), Width, Height);
        g.drawString(Integer.toString(getId()), getRoundX(), getRoundY());
    }
}