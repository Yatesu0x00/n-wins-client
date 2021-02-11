using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace NGClient
{
    class Chip
    {
        Ellipse circle = new Ellipse();
        double speed { get; set; }  // Fallgeschwindigkeit
        public bool canMoveH { get; set; }

        public Chip(Grid grid, double speed, System.Drawing.Color? color = null)
        {
            canMoveH = true;

            this.speed = speed;

            circle.Width = grid.ChipSize;
            circle.Height = grid.ChipSize;

            if (color == null)
            {
                circle.Fill = Brushes.Red;
            }
            else
            {
                System.Drawing.Color c = (System.Drawing.Color)color;

                RadialGradientBrush brush = new RadialGradientBrush();
                brush.GradientOrigin = new System.Windows.Point(0.75, 0.25);
                brush.GradientStops.Add(new GradientStop(Colors.White, 0.0));
                brush.GradientStops.Add(new GradientStop(Colors.Beige, 0.2));
                brush.GradientStops.Add(new GradientStop(Color.FromArgb(c.A, c.R, c.G, c.B), 1.0));

                circle.Fill = brush;
            }

            Canvas.SetLeft(circle, grid.MarginLeft);
            Canvas.SetTop(circle, grid.MarginTop - 5);
        }

        public void Move(int dir)
        {
            Canvas.SetLeft(circle, Canvas.GetLeft(circle) + circle.Width * dir);
        }

        public bool Drop(Grid grid, int target, double dt, int chipIndex)
        {
            canMoveH = false;

            if (Canvas.GetTop(circle) + circle.Height <= Canvas.GetTop(grid.Rect) + grid.Rect.Height - (grid.Floor[target] * grid.ChipSize))
            {
                Canvas.SetTop(circle, Canvas.GetTop(circle) + (speed * dt / 10));
                //Console.WriteLine("Falling");
                //Console.WriteLine("" + grid.MarginBottom);
                //Console.WriteLine("" + Canvas.GetTop(circle));               
                return true;
            }
            else 
            {
                //Wenn unten angekommen grid.floor++
                grid.Floor[target]++;
                return false;
            }
            

            //    ////    // Bewegt den Chip vertikal
            //    ////    // target: Spalte, in der der Chip herunter fällt, wird für grid.Floor benötigt
            //    ////    // dt:     Zeitintervall seit dem letzten Aufruf
            //    ////    // 
            //    ////    // Rückgabewert kann verwendet werden, um den Bewegungszustand
            //    ////    //anzuzeigen.
            //    ////    // Z.B. Chip fällt -> true, Chip ist unten angekommen -> false             
        }

        public void Undraw(Canvas canvas)
        {
            if (canvas.Children.Contains(circle))
            {
                canvas.Children.Remove(circle);
            }
        }

        public void Draw(Canvas canvas)
        {
            if (!canvas.Children.Contains(circle))
            {
                canvas.Children.Add(circle);
            }
        }

        public void drawNewChip(Grid grid, Canvas canvas, int chipIndex, Chip[] chip) 
        {

        }

        public void Resize(double sx, double sy)
        {
            //X *= sx;
            //Y *= sy;

            //Vx *= sx;
            //Vy *= sy;

            //circle.Width *= (sx + sy) / 2;
            //circle.Height *= (sx + sy) / 2;

            //Canvas.SetLeft(circle, sx * Canvas.GetLeft(circle));
            //Canvas.SetTop(circle, sy * Canvas.GetTop(circle));
        }
    }
}

