package labs.Lab6.figures;

import java.awt.*;

public class Square extends BaseFigure {

    public static final int Size = 20;

    public Square(int id, double vX, double vY, Color color) {
        super(id, vX, vY, color, Size, Size);
    }

    @Override
    public void Draw(Graphics g) {
        g.setColor(Color);
        g.drawRect(getRoundX(), getRoundY(), Width, Height);
        g.drawString(Integer.toString(getId()), getRoundX(), getRoundY());
    }
}
