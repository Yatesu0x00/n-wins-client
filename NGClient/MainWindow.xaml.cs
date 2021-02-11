using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NGClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Chip[] chip;
        Grid grid;
        StartDlg dlg;
        DispatcherTimer timer;
        Double ticks_old;
        double speed = 5; //Chip dropspeed
        int counter = 0;
        int chipIndex = 0;
        bool keyPressed;
        bool isRed;
        public MainWindow()
        {
            dlg = new StartDlg();

            if ((bool)dlg.ShowDialog())
            {
                InitializeComponent();
            }
            else
            {
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);

            isRed = false;

            ticks_old = Environment.TickCount;

            timer.Start();

            grid = new Grid(dlg.cols, dlg.rows);
            grid.Draw(c);

            chip = new Chip[dlg.rows * dlg.cols];
            //for (int i = 0; i < dlg.rows * dlg.cols; i++)
            //{
            //    
            //}
            chip[chipIndex] = new Chip(grid, speed, System.Drawing.Color.Red);

            chip[chipIndex].Draw(c);           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Double ticks = Environment.TickCount;

            if (keyPressed == true)
            {
                //chip[chipIndex].Drop(grid, counter, ticks - ticks_old, chipIndex);

                //Canvas.GetTop(circle) + circle.Height >= Canvas.GetTop(grid.Rect) + grid.Rect.Height

                if (chip[chipIndex].Drop(grid, counter, ticks - ticks_old, chipIndex) == false)
                {
                    keyPressed = false;
                    counter = 0;
                    chipIndex = chipIndex + 1;
                    
                    if (isRed == true)
                    {
                        isRed = false;
                        chip[chipIndex] = new Chip(grid, speed, System.Drawing.Color.Red);
                        chip[chipIndex].Draw(c);
                    }
                    else
                    {
                        isRed = true;
                        chip[chipIndex] = new Chip(grid, speed, System.Drawing.Color.Blue);
                        chip[chipIndex].Draw(c);
                    }
                    //chip[chipIndex] = new Chip(grid, speed, System.Drawing.Color.Red);

                    
                }

                //Console.WriteLine("Pressed");
            }
            //chip[chipIndex].drawNewChip(grid, c, chipIndex, chip);

            ticks_old = ticks;
        }

        private void c_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Double sx = e.NewSize.Width / e.PreviousSize.Width;
                Double sy = e.NewSize.Height / e.PreviousSize.Height;

                  //grid.Resize(sx, sy);
                  //chip[chipIndex].Resize(sx, sy);
            }
            catch { }
        }
        private void start_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void ende_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                if (counter > 0)
                {
                    if(chip[chipIndex].canMoveH == true) 
                    {
                        chip[chipIndex].Move(-1);
                        counter -= 1;
                    }                                    
                }
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                if (counter < dlg.cols -1)
                {
                    if (chip[chipIndex].canMoveH == true)
                    {
                        chip[chipIndex].Move(1);
                        counter += 1;
                    }
                }
            }

            if(Keyboard.IsKeyDown(Key.Down))
            {
                keyPressed = true;
            }
        }
    }
}
