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
        int chipIndex = 0;
        double speed = 10; //Chip dropspeed
        int counter = 1;

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
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);

            timer.Start();

            grid = new Grid(dlg.cols, dlg.rows);
            grid.Draw(c);

            chip = new Chip[dlg.rows * dlg.cols];
            for (int i = 0; i < dlg.rows * dlg.cols; i++)
            {
                chip[i] = new Chip(grid, speed, 0, null);
            }
            chip[chipIndex].Draw(c);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            

            if (Keyboard.IsKeyDown(Key.Left))
            {
                if(counter > 1) 
                {
                    chip[chipIndex].Move(-1);
                    counter -= 1;
                }
                
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                if(counter < dlg.cols) 
                {
                    chip[chipIndex].Move(1);
                    counter += 1;
                }
                
            }
            else if (Keyboard.IsKeyDown(Key.Down))
            {
                //chip[chipIndex].Drop
            }
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

        
    }
}
