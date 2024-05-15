using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Container container = new Container();
        public Brush[] brushes = { Brushes.Gold, Brushes.Blue, Brushes.Purple, Brushes.Black, Brushes.Green, Brushes.Red };
        public BrushEnumerator brushenumerator;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            brushenumerator = new BrushEnumerator(brushes);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X >= pictureBox1.Left && e.X <= pictureBox1.Right - 30 && e.Y >= pictureBox1.Top && e.Y <= pictureBox1.Bottom)
            {
                container.checkCursorAll(e.X, e.Y, checkBox1.Checked, checkBox2.Checked, chooseClass.SelectedIndex, pictureBox1.Left, pictureBox1.Right - 30, pictureBox1.Top, pictureBox1.Bottom - 30);
                pictureBox1.Invalidate();
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            container.paintAllShapes(e.Graphics);
            Pen pen = new Pen(Color.Black, 1);
            e.Graphics.DrawRectangle(pen, pictureBox1.Left, pictureBox1.Top, pictureBox1.Width - 30, pictureBox1.Height - 30);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                container.delete_objects();

            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                checkBox2.Checked = true;
            }
            else if (e.KeyCode == Keys.D1)
            {
                container.changeSizeAll(true, pictureBox1.Left, pictureBox1.Right - 30, pictureBox1.Top, pictureBox1.Bottom - 30);

            }
            else if (e.KeyCode == Keys.D2)
            {
                container.changeSizeAll(false, pictureBox1.Left, pictureBox1.Right - 30, pictureBox1.Top, pictureBox1.Bottom - 30);
            }
            else if (e.KeyCode == Keys.D0)
            {
                container.changeColorAll(brushenumerator.next());

            }
            else if (e.KeyCode == Keys.Up)
            {
                container.moveShapes(0, -5, pictureBox1.Left, pictureBox1.Right - 30, pictureBox1.Top, pictureBox1.Bottom - 30);
            }
            else if (e.KeyCode == Keys.Down)
            {
                container.moveShapes(0, 5, pictureBox1.Left, pictureBox1.Right - 30, pictureBox1.Top, pictureBox1.Bottom - 30);
            }
            else if (e.KeyCode == Keys.Left)
            {
                container.moveShapes(-5, 0, pictureBox1.Left, pictureBox1.Right - 30, pictureBox1.Top, pictureBox1.Bottom - 30);
            }
            else if (e.KeyCode == Keys.Right)
            {
                container.moveShapes(5, 0, pictureBox1.Left, pictureBox1.Right - 30, pictureBox1.Top, pictureBox1.Bottom - 30);
            }
            pictureBox1.Invalidate();

        }
        private void CheckBox2_Enter(object sender, EventArgs e)
        {
            this.ActiveControl = chooseClass;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                checkBox2.Checked = false;
            }
        }

        private void chooseClass_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

    }


}
