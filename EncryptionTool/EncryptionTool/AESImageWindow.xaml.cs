using Microsoft.Win32;
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

        public void CreateByteArrayFromFile(string fileName)
        {
            byte[] returnValue = null;
            string text = "";
            if (File.Exists(fileName))
            {
                using (var ms = new MemoryStream())
                {
                    using (var fs = File.OpenRead(fileName))
                    {
                        fs.CopyTo(ms);
                    }
                    returnValue = ms.ToArray();
                    //TxtResult.Text = returnValue.ToString();
                }
                for(int i = 0;i < returnValue.Length; i++)
                {
                    text += returnValue[i].ToString();
                }
                TxtResult.Text = text;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                //Filter = "Alle bestanden (*.*)|*.*|Tekstbestanden (*.txt) |*.txt",
                FilterIndex = 2, // index start vanaf 1, niet 0 hier! 2 wil zeggen hier filteren op .txt
                //FileName = "punten.txt",
                Multiselect = true, // je kan meerdere bestanden selecteren (true, anders false)

                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) // start in My Documents
                                                                  // == OF ==
                                                                  // InitialDirectory = System.IO.Path.GetFullPath(@"..\..\Bestanden"), // volledig pad
                                                                  // == OF ==
                                                                  // InitialDirectory = Environment.CurrentDirectory // onder onze \Debug map
            };
            if (ofd.ShowDialog() == true) // als de OpenFileDialog getoond kan worden
            {
                string bestandsnaam = ofd.FileName;
                //TxtResult.Text = bestandsnaam;
                CreateByteArrayFromFile(bestandsnaam);
            }

        }
    }
}
