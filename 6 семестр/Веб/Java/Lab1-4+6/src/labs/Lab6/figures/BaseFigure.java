package labs.Lab6.figures;

import java.awt.*;

public abstract class BaseFigure {

    protected int Id;

    protected double X;
    protected double Y;
    protected double vX;
    protected double vY;

    protected int Width;
    protected int Height;

    protected Color Color;

    protected BaseFigure(int id, double vX, double vY, Color color, int width, int height){
        X = width;
        Y = height;

        Width = width;
        Height = height;
        Id = id;
        this.vX = vX;
        this.vY = vY;
        this.Color = color;
    }

    public abstract void Draw(Graphics g);

    public void Move(int xMax, int yMax){
        X += vX;
        Y += vY;

        if (X-Width < 0 || X+Width > xMax){
            vX = -vX;
        }

        if (Y-Height < 0 || Y+Height > yMax){
            vY = -vY;
        }
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public void setColor(Color color){
        this.Color = color;
    }

    public double getX() {
        return X;
    }

    public double getY(){
        return Y;
    }

    public int getRoundX(){
        return (int) Math.round(X);
    }

    public int getRoundY(){
        return (int) Math.round(Y);
    }

    public double getvX() {
        return vX;
    }

    public double getvY() {
        return vY;
    }

    public void setvX(double vX) {
        this.vX = vX;
    }

    public void setvY(double vY) {
        this.vY = vY;
    }

    @Override
    public String toString() {
        return "BaseFigure{" +
                "Id=" + Id +
                ", X=" + X +
                ", Y=" + Y +
                ", vX=" + vX +
                ", vY=" + vY +
                ", Color=" + Color +
                '}';
    }
}
