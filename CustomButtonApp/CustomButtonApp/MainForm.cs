using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomButtonApp
{
    public class MainForm : Form
    {
        private CustomButton customButton;

        public MainForm()
        {
            Text = "Custom Button";
            Width = 400;
            Height = 300;

            customButton = new CustomButton("Click Me", new Rectangle(100, 100, 200, 50));
            customButton.Click += CustomButton_Click;

            this.Paint += MainForm_Paint;
            this.MouseMove += MainForm_MouseMove;
            this.MouseClick += MainForm_MouseClick;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            customButton.Draw(e.Graphics);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            customButton.HandleMouseMove(e.Location);
            Invalidate();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            customButton.HandleMouseClick(e.Location);
        }

        private void CustomButton_Click(object sender, ButtonEventArgs e)
        {
            MessageBox.Show($"Button '{e.ButtonText}' clicked!");
        }
    }
}
