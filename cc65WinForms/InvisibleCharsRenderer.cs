using FastColoredTextBoxNS;
using System.Drawing;

namespace cc65WinForms
{
    public class InvisibleCharsRenderer : Style
    {
        Pen pen;

        public InvisibleCharsRenderer(Pen pen)
        {
            this.pen = pen;
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            FastColoredTextBox tb = range.tb;
            using (Brush brush = new SolidBrush(pen.Color))
            {
                foreach (Place place in range)
                {
                    switch (tb[place].c)
                    {
                        case ' ':
                            Point point = tb.PlaceToPoint(place);
                            point.Offset(tb.CharWidth / 2, tb.CharHeight / 2);
                            gr.DrawLine(pen, point.X, point.Y, point.X + 1, point.Y);
                            break;
                    }

                    if (tb[place.iLine].Count - 1 == place.iChar)
                    {
                        Point point = tb.PlaceToPoint(place);
                        point.Offset(tb.CharWidth, 0);
                        gr.DrawString("¶", tb.Font, brush, point);
                    }
                }
            }
        }
    }
}
