package labs.Lab6.figures;

import java.awt.*;

public class Triangle extends BaseFigure{

    public static final int Size = 20;

    public Triangle(int id, double vX, double vY, Color color) {
        super(id, vX, vY, color, Size, Size);
    }

    @Override
    public void Draw(Graphics g) {
        g.setColor(Color);

        /*
               2
              / \
             /   \
            1-----3
        */

        int x = getRoundX(),
            y = getRoundY();

        int[] xPoints = new int[]{x, x+Width/2, x+Width};
        int[] yPoints = new int[]{y+Height, y, y+Height};

        g.drawPolygon(xPoints, yPoints, 3);
        g.drawString(Integer.toString(getId()), getRoundX(), getRoundY());
    }
}
