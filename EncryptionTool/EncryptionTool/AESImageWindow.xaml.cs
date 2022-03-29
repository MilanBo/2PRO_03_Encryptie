using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EncryptionTool
{
    /// <summary>
    /// Interaction logic for AESImageWindow.xaml
    /// </summary>
    public partial class AESImageWindow : Window
    {
        public AESImageWindow()
        {
            InitializeComponent();
        }

        public static byte[] CreateByteArrayFromFile(string fileName)
        {
            byte[] returnValue = null;
            if (File.Exists(fileName))
            {
                using (var ms = new MemoryStream())
                {
                    using (var fs = File.OpenRead(fileName))
                    {
                        fs.CopyTo(ms);
                    }
                    returnValue = ms.ToArray();
                }
            }
            return returnValue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateByteArrayFromFile();
        }
    }
}
