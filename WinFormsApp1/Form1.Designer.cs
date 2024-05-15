namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            Menu = new GroupBox();
            chooseClass = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(14, 29);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(581, 677);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(376, 739);
            checkBox1.Margin = new Padding(3, 4, 3, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(335, 24);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Выделение двух фигур при их пересечении";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(14, 739);
            checkBox2.Margin = new Padding(3, 4, 3, 4);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(307, 24);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Включить выделение нескольких фигур";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.Enter += CheckBox2_Enter;
            // 
            // Menu
            // 
            Menu.Controls.Add(chooseClass);
            Menu.Font = new Font("Segoe UI", 25F);
            Menu.Location = new Point(663, 29);
            Menu.Margin = new Padding(3, 4, 3, 4);
            Menu.Name = "Menu";
            Menu.Padding = new Padding(3, 4, 3, 4);
            Menu.Size = new Size(285, 677);
            Menu.TabIndex = 3;
            Menu.TabStop = false;
            Menu.Text = "Menu";
            // 
            // chooseClass
            // 
            chooseClass.DropDownStyle = ComboBoxStyle.DropDownList;
            chooseClass.Font = new Font("Segoe UI", 10F);
            chooseClass.FormattingEnabled = true;
            chooseClass.Items.AddRange(new object[] { "Круг", "Квадрат", "Треугольник" });
            chooseClass.Location = new Point(7, 68);
            chooseClass.Margin = new Padding(3, 4, 3, 4);
            chooseClass.Name = "chooseClass";
            chooseClass.Size = new Size(229, 31);
            chooseClass.TabIndex = 0;
            chooseClass.KeyDown += chooseClass_KeyDown;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(947, 812);
            Controls.Add(Menu);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(pictureBox1);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(950, 820);
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            Menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }









        #endregion

        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private GroupBox Menu;
        private ComboBox chooseClass;
    }
}
