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
using System.Windows.Shapes;

namespace NGClient
{
    /// <summary>
    /// Interaktionslogik für StartDlg.xaml
    /// </summary>
    public partial class StartDlg : Window
    {
        public int cols { get; set; }
        public int rows { get; set; }
        public int N { get; set; }

        public StartDlg()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cols = Convert.ToInt16(tbR.Text);
                rows = Convert.ToInt16(tbS.Text);
                N = Convert.ToInt16(tbN.Text);

                if(N < 3)
                {
                    throw new Exception("N < 3");
                }
                else
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message, "Eingabefehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        private void TextBox_GotFocus_2(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
