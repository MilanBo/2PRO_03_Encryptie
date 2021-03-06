using EncryptionTool.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace EncryptionTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AESWindow windows = new AESWindow(TxtNaamSleutel.Text);
            windows.Closed += (s, args) => this.Show();
            windows.Show();
        }

        private void BtnRSAWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RSAWindow windows = new RSAWindow(TxtNaamSleutel.Text);
            windows.Closed += (s, args) => this.Show();
            windows.Show();
        }

        private void Btn_Verlaat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
