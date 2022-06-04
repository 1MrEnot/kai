package labs.Lab6.figures;

import java.awt.*;

public class Oval extends BaseFigure {

    public static final int W = 20;
    public static final int H = 30;

    public Oval(int Id, double vX, double vY, Color color) {
        super(Id, vX, vY, color, W, H);
    }

    @Override
    public void Draw(Graphics g) {
        g.setColor(Color);
        g.drawOval(getRoundX(), getRoundY(), Width, Height);
        g.drawString(Integer.toString(getId()), getRoundX(), getRoundY());
    }
}
