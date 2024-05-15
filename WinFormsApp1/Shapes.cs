using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinFormsApp1
{

    public abstract class Shape
    {
        protected int x;
        protected int y;
        protected bool isSelected;
        protected Rectangle rect;
        protected Brush color;
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.isSelected = false;
            this.color = Brushes.Red;
        }
        public abstract void paintShape(Graphics g);

        public virtual bool checkCursor(int mouseX, int mouseY)
        {
            return false;
        }
        public bool check_boundaries(int dx, int dy, int pictBoxLeft, int pictBoxRight, int pictBoxTop, int pictBoxBottom)
        {
            if (rect.Left + dx >= pictBoxLeft && rect.Right + dx <= pictBoxRight && rect.Top + dy >= pictBoxTop && rect.Bottom + dy <= pictBoxBottom)
            {
                return true;
            }
            return false;
        }
        public virtual void moveShape(int dx, int dy, int pictBoxLeft, int pictBoxRight, int pictBoxTop, int pictBoxBottom)
        {
            int newX = x + dx;
            int newY = y + dy;

            if (check_boundaries(dx, dy, pictBoxLeft, pictBoxRight, pictBoxTop, pictBoxBottom))
            {
                x = newX;
                y = newY;
            }
            else if (rect.Left - pictBoxLeft < dx) x -= rect.Left - pictBoxLeft;
            else if (pictBoxRight - rect.Right < dx) x += pictBoxRight - rect.Right;
            else if (pictBoxBottom - rect.Bottom < dy) y += pictBoxBottom - rect.Bottom;
            else if (rect.Top - pictBoxTop < dy) y -= rect.Top - pictBoxTop;
        }


        public bool selected()
        {
            return isSelected;
        }

        public void select()
        {
            isSelected = true;
        }

        public void unselect()
        {
            isSelected = false;
        }

        public abstract void change_size(bool setting, int pictureBoxLeft, int pictureBoxRight, int pictureBoxTop, int pictureBoxBottom);

        public void change_color(Brush color)
        {
            this.color = color;
        }



    }

    public class CCircle : Shape
    {
        private int radius;


        public CCircle(int x, int y, int radius) : base(x, y)
        {
            this.radius = radius;
            rect = new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public override void paintShape(Graphics g)
        {


            rect.X = x - radius; rect.Y = y - radius; rect.Width = 2 * radius; rect.Height = 2 * radius;

            if (isSelected)
            {
                Pen pen = new Pen(Color.Black, 2);
                g.FillEllipse(color, rect);
                g.DrawRectangle(pen, rect);

            }

            else
            {
                g.FillEllipse(color, rect);
            }

        }

        public override void change_size(bool setting, int pictureBoxLeft, int pictureBoxRight, int pictureBoxTop, int pictureBoxBottom)
        {
            int size_ratio;
            if (setting) { size_ratio = 3; }
            else { size_ratio = -3; }

            if (!setting && radius > 30 - size_ratio)
            {
                radius += size_ratio;
            }
            else if (setting)
            {
                if (rect.Left - size_ratio >= pictureBoxLeft && rect.Right + size_ratio <= pictureBoxRight && rect.Top - size_ratio >= pictureBoxTop && rect.Bottom + size_ratio <= pictureBoxBottom)
                    radius += size_ratio;
                else if (rect.Left - pictureBoxLeft < size_ratio) radius += (rect.Left - pictureBoxLeft) / 2;
                else if (pictureBoxRight - rect.Right < size_ratio) radius += (pictureBoxRight - rect.Right) / 2;
                else if (pictureBoxBottom - rect.Bottom < size_ratio) radius += (pictureBoxBottom - rect.Bottom) / 2;
                else if (rect.Top - pictureBoxTop < size_ratio) radius += (rect.Top - pictureBoxTop) / 2;
            }

        }

        public override bool checkCursor(int mouseX, int mouseY)
        {
            int dx = mouseX - x;
            int dy = mouseY - y;
            return dx * dx + dy * dy <= radius * radius;
        }



    }

    public class MyRectangle : Shape
    {
        private int width;
        private int height;
        public MyRectangle(int x, int y, int width, int height) : base(x, y)
        {
            this.width = width;
            this.height = height;
            rect = new Rectangle(x - width / 2, y - height / 2, width, height);
        }

        public override void paintShape(Graphics g)
        {

            rect.X = x - width / 2; rect.Y = y - height / 2; rect.Width = width; rect.Height = height;
            if (isSelected)
            {
                Pen pen = new Pen(Color.Black, 2);
                g.DrawRectangle(pen, rect);
                g.FillRectangle(color, rect);
            }

            else
            {
                g.FillRectangle(color, rect);
            }
        }

        public override bool checkCursor(int mouseX, int mouseY)
        {
            int dx = mouseX - x;
            int dy = mouseY - y;
            return Math.Abs(dx) <= (width) / 2 && Math.Abs(dy) <= (height) / 2;
        }

        public override void change_size(bool setting, int pictBoxLeft, int pictBoxRight, int pictBoxTop, int pictBoxBottom)
        {
            int size_ratio;
            if (setting) { size_ratio = 3; }
            else { size_ratio = -3; }

            if (!setting && width >= 60 - size_ratio)
            {
                width += size_ratio;
                height += size_ratio;
            }
            else if (setting)
            {
                if (rect.Left - size_ratio >= pictBoxLeft && rect.Right + size_ratio <= pictBoxRight && rect.Top - size_ratio >= pictBoxTop && rect.Bottom + size_ratio <= pictBoxBottom)
                {
                    width += size_ratio;
                    height += size_ratio;
                }
                else if (rect.Left - pictBoxLeft < size_ratio)
                {
                    width += (rect.Left - pictBoxLeft) / 2;
                    height += (rect.Left - pictBoxLeft) / 2;
                }
                else if (pictBoxRight - rect.Right < size_ratio)
                {
                    width += (pictBoxRight - rect.Right) / 2;
                    height += (pictBoxRight - rect.Right) / 2;
                }
                else if (pictBoxBottom - rect.Bottom < size_ratio)
                {
                    width += (pictBoxBottom - rect.Bottom) / 2;
                    height += (pictBoxBottom - rect.Bottom) / 2;
                }
                else if (rect.Top - pictBoxTop < size_ratio)
                {
                    width += (rect.Top - pictBoxTop) / 2;
                    height += (rect.Top - pictBoxTop) / 2;
                }
            }
        }
    }

    public class Triangle : Shape
    {
        private Point[] points = new Point[3];
        private double defS;
        private double currS;
        public Triangle(int x, int y) : base(x, y)
        {
            points[0] = new Point(x, y - 30);
            points[1] = new Point(x - 30, y + 30);
            points[2] = new Point(x + 30, y + 30);
            rect = new Rectangle(points[1].X, points[0].Y, points[2].X - points[1].X, points[1].Y - points[0].Y);
            defS = rect.Width * rect.Height;

        }

        public override void paintShape(Graphics g)
        {

            if (isSelected)
            {
                Pen pen = new Pen(Color.Black, 2);
                points[0] = new Point(points[0].X, points[0].Y);
                points[1] = new Point(points[1].X, points[1].Y);
                points[2] = new Point(points[2].X, points[2].Y);
                rect.X = points[1].X; rect.Y = points[0].Y; rect.Width = points[2].X - points[1].X; rect.Height = points[1].Y - points[0].Y;
                currS = rect.Width * rect.Height;
                g.FillPolygon(color, points);
                g.DrawRectangle(pen, rect);

            }

            else
            {
                g.FillPolygon(color, points);
            }
        }

        public override bool checkCursor(int mouseX, int mouseY)
        {

            bool side1 = (mouseX - points[0].X) * (points[1].Y - points[0].Y) - (points[1].X - points[0].X) * (mouseY - points[0].Y) >= 0;
            bool side2 = (mouseX - points[1].X) * (points[2].Y - points[1].Y) - (points[2].X - points[1].X) * (mouseY - points[1].Y) >= 0;
            bool side3 = (mouseX - points[2].X) * (points[0].Y - points[2].Y) - (points[0].X - points[2].X) * (mouseY - points[2].Y) >= 0;
            return side1 == side2 && side2 == side3;
        }

        public override void moveShape(int dx, int dy, int pictBoxLeft, int pictBoxRight, int pictBoxTop, int pictBoxBottom)
        {
            if (check_boundaries(dx, dy, pictBoxLeft, pictBoxRight, pictBoxTop, pictBoxBottom))
            {
                points[0].X += dx; points[0].Y += dy;
                points[1].X += dx; points[1].Y += dy;
                points[2].X += dx; points[2].Y += dy;
            }
        }

        public override void change_size(bool setting, int pictureBoxLeft, int pictureBoxRight, int pictureBoxTop, int pictureBoxBottom)
        {
            int size_ratio;
            if (setting) { size_ratio = 3; }
            else { size_ratio = -3; }

            if (!setting && currS > defS)
            {
                points[0].Y -= size_ratio;
                points[1].X -= size_ratio; points[1].Y += size_ratio;
                points[2].X += size_ratio; points[2].Y += size_ratio;
            }
            else if (setting)
            {
                if (rect.Left - size_ratio >= pictureBoxLeft && rect.Right + size_ratio <= pictureBoxRight && rect.Top - size_ratio >= pictureBoxTop && rect.Bottom + size_ratio <= pictureBoxBottom)
                {
                    points[0].Y -= size_ratio;
                    points[1].X -= size_ratio; points[1].Y += size_ratio;
                    points[2].X += size_ratio; points[2].Y += size_ratio;
                }
                else if (rect.Left - pictureBoxLeft < size_ratio)
                {
                    points[0].Y -= rect.Left - pictureBoxLeft;
                    points[1].X -= rect.Left - pictureBoxLeft; points[1].Y += rect.Left - pictureBoxLeft;
                    points[2].X += rect.Left - pictureBoxLeft; points[2].Y += rect.Left - pictureBoxLeft;
                }
                else if (pictureBoxRight - rect.Right < size_ratio)
                {
                    points[0].Y -= pictureBoxRight - rect.Right;
                    points[1].X -= pictureBoxRight - rect.Right; points[1].Y += pictureBoxRight - rect.Right;
                    points[2].X += pictureBoxRight - rect.Right; points[2].Y += pictureBoxRight - rect.Right;
                }
                else if (pictureBoxBottom - rect.Bottom < size_ratio)
                {
                    points[0].Y -= pictureBoxBottom - rect.Bottom;
                    points[1].X -= pictureBoxBottom - rect.Bottom; points[1].Y += pictureBoxBottom - rect.Bottom;
                    points[2].X += pictureBoxBottom - rect.Bottom; points[2].Y += pictureBoxBottom - rect.Bottom;
                }
                else if (rect.Top - pictureBoxTop < size_ratio)
                {
                    points[0].Y -= rect.Top - pictureBoxTop;
                    points[1].X -= rect.Top - pictureBoxTop; points[1].Y += rect.Top - pictureBoxTop;
                    points[2].X += rect.Top - pictureBoxTop; points[2].Y += rect.Top - pictureBoxTop;
                }
            }


        }

    }
}
