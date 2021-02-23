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
        double speed = 10; //Chip dropspeed
        int counter = 0;
        int chipIndex = 0;
        bool keyPressed;
        bool isRed;
        int maxChips;
        //int anz;
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

            maxChips = dlg.rows * dlg.cols;
            chip = new Chip[maxChips + 1];
            
            chip[chipIndex] = new Chip(grid, speed, System.Drawing.Color.Red);

            chip[chipIndex].Draw(c);           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Double ticks = Environment.TickCount;

            if (keyPressed == true)
            {
                if (chip[chipIndex].Drop(grid, counter, ticks - ticks_old) == true)
                {
                    keyPressed = false;
                    counter = 0;
                    chipIndex = chipIndex + 1;
                    
                    if (isRed == true)
                    {
                        isRed = false;
                        if(chipIndex <= maxChips - 1)
                        {
                            chip[chipIndex] = new Chip(grid, speed, System.Drawing.Color.Red);
                            chip[chipIndex].Draw(c);
                            //chip[chipIndex].Owner = 1;
                        }
                    }
                    else
                    {
                        isRed = true;
                        if (chipIndex <= maxChips - 1)
                        {
                            chip[chipIndex] = new Chip(grid, speed, System.Drawing.Color.Blue);
                            chip[chipIndex].Draw(c);
                            //chip[chipIndex].Owner = -1;
                        }
                    }                
                }
            }
            ticks_old = ticks;
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
                if(grid.Floor[counter] != dlg.rows) 
                {
                    keyPressed = true;
                }
                else 
                {
                    keyPressed = false;
                }
            }
        }

        private void checkWinner() 
        {
            //for (int y = 0; y < 4; y++)
            //{
            //    for (int x = 0; x < 4; x++)
            //    {
            //        for (int i = 0; i < dlg.cols; i++)
            //        {
            //            for (int j = 0; j < dlg.rows; j++)
            //            {

            //            }
            //        }
            //    }
            //}                    
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

        private void c_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                Double sx = e.NewSize.Width / e.PreviousSize.Width;
                Double sy = e.NewSize.Height / e.PreviousSize.Height;

                //grid.Resize(sx, sy);

                lbInfo.FontSize *= ((sx + sy) / 2);
                lbInfo.Height *= sy;
                lbInfo.Width *= sx;
                Canvas.SetLeft(lbInfo, sx * Canvas.GetLeft(lbInfo));
                Canvas.SetTop(lbInfo, sy * Canvas.GetTop(lbInfo));
            }
            catch { }
        }
    }
}
