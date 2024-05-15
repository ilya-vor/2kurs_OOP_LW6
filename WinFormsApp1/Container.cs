using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public abstract class Strategy
    {
        public abstract void push_back(ref Shape[] arr, ref int current_size, Shape obj);
    }
    public class StrategyPushBackDefault : Strategy
    {
        public override void push_back(ref Shape[] arr, ref int current_size, Shape obj)
        {
            arr[current_size++] = obj;
        }

    }

    public class StrategyPushBackLimit : Strategy
    {
        public override void push_back(ref Shape[] arr, ref int current_size, Shape obj)
        {
            Shape[] new_container = new Shape[current_size + 10];
            for (int i = 0; i < current_size; i++)
                new_container[i] = arr[i];

            new_container[current_size++] = obj;
            arr = new_container;
        }
    }


    public class Container
    {
        private int capacity; 
        private Shape[] container;
        private int current_size = 0;
        private Strategy strategy = new StrategyPushBackDefault();
        private int size_ratio = 1;

        public Container()
        {
            this.capacity = 10;
            container = new Shape[this.capacity];

        }

        public Container(int capacity)
        {
            this.capacity = capacity;
            container = new Shape[this.capacity];
        }

        public void push_back(Shape obj)
        {
            if (current_size >= capacity)
            {
                strategy = new StrategyPushBackLimit();
                capacity += 10;
                strategy.push_back(ref container, ref current_size, obj);

            }
            else
                strategy.push_back(ref container, ref current_size, obj);
        }

        public void paintAllShapes(Graphics g)
        {
            for (int i = 0; i < current_size; i++)
            {
                container[i].paintShape(g);
            }
        }
        

        public void checkCursorAll(int mouseX, int mouseY, bool checkBox1State, bool checkBox2State, int chooseClassIndex, int pictureBox1_Left, int pictureBox1_Right, int pictureBox1_Top, int pictureBox1_Bottom)
        {
            List<int> indexes = cursorOnShape(mouseX, mouseY);
            if (indexes.Count == 0 && chooseClassIndex!=-1)
            {
                unselectall();
             
                    if (mouseX - 30 >= pictureBox1_Left && mouseX <= pictureBox1_Right - 30) { }
                    else if (mouseX - 30 < pictureBox1_Left) { mouseX = pictureBox1_Left + 30; }
                    else { mouseX = pictureBox1_Right - 30; }

                    if (mouseY - 30 >= pictureBox1_Top && mouseY <= pictureBox1_Bottom - 30) { }
                    else if (mouseY - 30 < pictureBox1_Top) { mouseY = pictureBox1_Top + 30; }
                    else { mouseY = pictureBox1_Bottom - 30; }


                    if (chooseClassIndex == 0)
                        push_back(new CCircle(mouseX, mouseY, 30));
                    else if (chooseClassIndex == 1)
                        push_back(new MyRectangle(mouseX, mouseY, 60, 60));
                    else if (chooseClassIndex == 2)
                        push_back(new Triangle(mouseX, mouseY));
                    container[current_size - 1].select();
                

               
            }
            else if (indexes.All(i => container[i].selected()) && get_selected_circles_count() > indexes.Count)
            {
                for (int i = 0; i < indexes.Count; i++)
                    container[indexes[i]].unselect();
            }

            else if (!checkBox1State && !checkBox2State)
            {

                for (int i = 0; i < indexes.Count; i++)
                {
                    unselectall();
                    container[indexes[i]].select();
                }
            }
            else if (!checkBox1State && checkBox2State)
            {

                for (int i = 0; i < indexes.Count; i++)
                {
                    container[indexes[i]].select();
                    break;
                }
            }
            else if (checkBox1State && !checkBox2State)
            {
                unselectall();
                for (int i = 0; i < indexes.Count; i++)
                {
                    container[indexes[i]].select();
                }
            }
            else
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    container[indexes[i]].select();
                }
            }
        }

        public void moveShapes(int dx, int dy, int pictBoxLeft, int pictBoxRight, int pictBoxTop, int pictBoxBottom)
        {
            foreach (Shape shape in container)
            {
                if (shape != null && shape.selected())
                    shape.moveShape(dx, dy, pictBoxLeft, pictBoxRight, pictBoxTop, pictBoxBottom);
            }
        }

        public void changeSizeAll(bool setting, int pictureBoxLeft, int pictureBoxRight, int pictureBoxTop, int pictureBoxBottom)
        {
            foreach(Shape p in container)
            {
               if (p!= null && p.selected())
                   p.change_size(setting, pictureBoxLeft,pictureBoxRight, pictureBoxTop,pictureBoxBottom);
            }
        }

        public void changeColorAll(Brush color)
        {
            foreach (Shape p in container)
            {
                if (p != null && p.selected())
                    p.change_color(color);
            }
        }
        

        public int get_selected_circles_count()
        {
            int count = 0;
            for (int i = 0; i < current_size; i++)
            {
                if (container[i].selected())
                    count++;

            }
            return count;
        }

        public void delete_objects()
        {
            Shape[] new_container = new Shape[capacity];
            int new_container_index = 0;
            for (int i = 0; i < current_size; i++)
            {
                if (!container[i].selected())
                {
                    new_container[new_container_index++] = container[i];
                }
            }
            current_size = new_container_index;
            if (current_size != 0)
                new_container[current_size - 1].select();
            container = new_container;
        }

        public List<int> cursorOnShape(int mouseX, int mouseY)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < current_size; i++)
            {
                if (container[i].checkCursor(mouseX, mouseY))
                    indexes.Add(i);
            }

            return indexes;
        }

        public void unselectall()
        {
            for (int i = 0; i < current_size; i++)
            {
                container[i].unselect();

            }
        }

        public int get_size()
        {
            return current_size;
        }

        public Shape getCircle(int ind)
        {
            return container[ind];
        }
    }

    public class BrushEnumerator
    {
        private Brush[] brushes;
        private int current_color;

        public BrushEnumerator(Brush[] brushes)
        {
            this.brushes = brushes;
            this.current_color = 0;
        }

        public Brush next()
        {
            if (!isEOL())
                return brushes[current_color++];
            else
            {
                current_color = 0;
                return brushes[current_color++];
            }
        }
        private bool isEOL()
        {
            return current_color >= brushes.Length;
        }
    }
}
