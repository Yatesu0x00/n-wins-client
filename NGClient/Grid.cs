using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace NGClient
{
    class Grid
    {
        public Rectangle Rect { get; set; } = new Rectangle();
        public Line HorLine { get; set; } = new Line();
        public Line[] VertLines { get; set; }
        public double ChipSize { get; set; }
        public double MarginLeft { get; set; } = 50;
        public double MarginTop { get; set; } = 40;
        public double MarginRight { get; set; } = 50;
        public double MarginBottom { get; set; } = 100;
        public int Cols { get; set; }
        public int Rows { get; set; }
        public int[] Floor { get; set; }

        public Grid(int cols, int rows)
        {
            Cols = cols;
            Rows = rows;

            VertLines = new Line[Cols + 1];

            // Die Falltiefe ist abhängig davon, ob sich schon Chips im Schacht befinden
            Floor = new int[Cols + 1];

            ChipSize = Math.Min((SystemParameters.PrimaryScreenWidth - MarginLeft - MarginRight) / Cols,
                                (SystemParameters.PrimaryScreenHeight - SystemParameters.WindowCaptionHeight - MarginTop - MarginBottom) / (Rows + 1));

            Rect.Width = Cols * ChipSize;
            Rect.Height = Rows * ChipSize;

            Canvas.SetLeft(Rect, MarginLeft);
            Canvas.SetTop(Rect, ChipSize + MarginTop);

            LinearGradientBrush brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop(Colors.Azure, 0.0));
            brush.GradientStops.Add(new GradientStop(Colors.Aquamarine, 1.0));

            Rect.Fill = brush;

            HorLine.Stroke = Brushes.SteelBlue;
            HorLine.StrokeThickness = 4;
            HorLine.X1 = MarginLeft;
            HorLine.Y1 = Rect.Height + ChipSize + MarginTop;
            HorLine.X2 = cols * ChipSize + MarginLeft;
            HorLine.Y2 = HorLine.Y1;

            for (int i = 0; i < cols + 1; i++)
            {
                VertLines[i] = new Line();
                VertLines[i].Stroke = HorLine.Stroke;
                VertLines[i].StrokeThickness = 4;
                VertLines[i].X1 = i * ChipSize + MarginLeft;
                VertLines[i].Y1 = ChipSize + MarginTop;
                VertLines[i].X2 = i * ChipSize + MarginLeft;
                VertLines[i].Y2 = Rect.Height + ChipSize + MarginTop;
            }
        }

        public void Undraw(Canvas canvas)
        {
            if (canvas.Children.Contains(Rect))
            {
                canvas.Children.Remove(Rect);
            }
            if (canvas.Children.Contains(HorLine))
            {
                canvas.Children.Remove(HorLine);
            }
            foreach (Line line in VertLines)
            {
                if (canvas.Children.Contains(line)) 
                {
                    canvas.Children.Remove(line);
                }
            }
        }

        public void Draw(Canvas canvas)
        {
            if (!canvas.Children.Contains(Rect))                
            {
                canvas.Children.Add(Rect);
            }
            if (!canvas.Children.Contains(HorLine))
            {
                canvas.Children.Add(HorLine);
            }
            foreach (Line line in VertLines)
            {
                if (!canvas.Children.Contains(line))
                {
                    canvas.Children.Add(line);
                }
            }
        }

        public void Resize(double sx, double sy)
        {
            Rect.Width = Rect.Width * sx;
            Rect.Height = Rect.Height * sy;

            Canvas.SetLeft(Rect, sx * Canvas.GetLeft(Rect));
            Canvas.SetTop(Rect, sy * Canvas.GetTop(Rect));

            HorLine.X1 = sx;
            HorLine.Y1 = sy;

            HorLine.X2 = HorLine.X1;
            HorLine.Y2 = HorLine.Y1;

            HorLine.Width *= ((sx + sy) / 2);
            HorLine.Height *= ((sx + sy) / 2);

            Canvas.SetTop(HorLine, sy * Canvas.GetTop(HorLine));
            Canvas.SetLeft(HorLine, sx * Canvas.GetLeft(HorLine));
        }
    }
}
