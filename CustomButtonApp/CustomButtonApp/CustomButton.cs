using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomButtonApp
{
    public class CustomButton
    {
        public string Text { get; set; }
        public Rectangle Bounds { get; set; }
        public Color BackgroundColor { get; set; } = Color.LightGray;
        public Color ForegroundColor { get; set; } = Color.Black;
        public Color HoverColor { get; set; } = Color.Gray;

        private bool isHovered = false;

        public event EventHandler<ButtonEventArgs> Click;

        public CustomButton(string text, Rectangle bounds)
        {
            Text = text;
            Bounds = bounds;
        }

        public void Draw(Graphics graphics)
        {
            Color currentColor = isHovered ? HoverColor : BackgroundColor;
            using (Brush brush = new SolidBrush(currentColor))
            {
                graphics.FillRectangle(brush, Bounds);
            }

            using (Brush textBrush = new SolidBrush(ForegroundColor))
            {
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                graphics.DrawString(Text, SystemFonts.DefaultFont, textBrush, Bounds, stringFormat);
            }
        }

        public void HandleMouseMove(Point location)
        {
            isHovered = Bounds.Contains(location);
        }

        public void HandleMouseClick(Point location)
        {
            if (Bounds.Contains(location))
            {
                Click?.Invoke(this, new ButtonEventArgs(Text));
            }
        }
    }

    public class ButtonEventArgs : EventArgs
    {
        public string ButtonText { get; }

        public ButtonEventArgs(string buttonText)
        {
            ButtonText = buttonText;
        }
    }
}
