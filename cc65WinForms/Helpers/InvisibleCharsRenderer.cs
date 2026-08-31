using FastColoredTextBoxNS;
using System.Drawing;

namespace cc65WinForms
{
    /// <summary>
    /// Renders visible markers for normally invisible characters inside a
    /// <see cref="FastColoredTextBoxNS.FastColoredTextBox"/> text range.
    /// Currently draws:
    /// - a small dot for space characters,
    /// - a pilcrow "¶" at end-of-line positions.
    /// </summary>
    public class InvisibleCharsRenderer : Style
    {
        /// <summary>
        /// Pen used for drawing the dot indicators.
        /// The pen's <see cref="Pen.Color"/> is also used to create a brush for text rendering.
        /// </summary>
        Pen pen;

        /// <summary>
        /// Initializes a new instance of <see cref="InvisibleCharsRenderer"/>.
        /// </summary>
        /// <param name="pen">Pen used to draw space markers.</param>
        public InvisibleCharsRenderer(Pen pen)
        {
            this.pen = pen;
        }

        /// <summary>
        /// Draws markers for invisible characters in the specified <paramref name="range"/>.
        /// Called by the fast-colored text box rendering pipeline.
        /// </summary>
        /// <param name="gr">Graphics surface to draw on.</param>
        /// <param name="position">Top-left position of the visible area (provided by the caller).</param>
        /// <param name="range">The text range for which to render markers. The range's <see cref="FastColoredTextBox"/>
        /// instance is used to translate text places to pixel coordinates and to obtain font/metrics.</param>
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

                    // If place is the last character in the line, draw a pilcrow to indicate EOL.
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
