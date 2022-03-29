using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace EncryptionTool
{
    /// <summary>
    /// Interaction logic for AESWindow.xaml
    /// </summary>
    public partial class AESWindow : Window
    {
        public AESWindow(AESHelper AESHelper)
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            using (Aes myAes = Aes.Create())
            {
                byte[] encrypted = AESHelper.EncryptStringToBytes_Aes(TxtInput.Text, myAes.Key, myAes.IV);
                string text = "";
                for (int i = 0 ; i < encrypted.Length; i++)
                {
                    text += encrypted[i].ToString();
                }
                TxtOutput.Text = text;
            }

        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {

            using (Aes myAes = Aes.Create())
            {
                // Convert a C# string to a byte array  
                byte[] encrypted = Encoding.ASCII.GetBytes(TxtInput.Text);
                string roundtrip = AESHelper.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                TxtOutput.Text = roundtrip;
            }
        }



        //// Decrypt the bytes to a string.
        //string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

        ////Display the original data and the decrypted data.
        //Console.WriteLine("Original:   {0}", original);
        //            Console.WriteLine("Round Trip: {0}", roundtrip);
        //        }
        //}

        private void ChooseFileToEncrypt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            string text = "";

            if (dialog.ShowDialog() == true)
            {
                //Get the path of specified file
                var filePath = dialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = dialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    using (Aes myAes = Aes.Create())
                    {
                        byte[] encrypted =  AESHelper.EncryptStringToBytes_Aes(fileContent, myAes.Key, myAes.IV);

                        for (int i = 0; i < encrypted.Length; i++){
                            text += encrypted[i].ToString();
                        }
                        TxtOutput.Text = text;
                    }
                }
            }
        }

        private void ChooseFileToDecrypt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == true)
            {
                //Get the path of specified file
                var filePath = dialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = dialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string encrypted = reader.ReadToEnd();
                    byte[] cipherText = Encoding.ASCII.GetBytes(encrypted);
                    using (Aes myAes = Aes.Create())
                    {
                        string plaintext = AESHelper.DecryptStringFromBytes_Aes(cipherText, myAes.Key, myAes.IV);

                        TxtOutput.Text = plaintext;
                    }
                }
            }
        }
    }
}
