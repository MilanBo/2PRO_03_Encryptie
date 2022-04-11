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
using EncryptionTool.Helpers;

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
                //Stringtext = TxtInput.Text;
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
                string filePath = dialog.FileName;

                //Read the contents of the file into a stream
                Stream fileStream = dialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(reader.ReadToEnd());
                    string fileContent = System.Convert.ToBase64String(plainTextBytes);
                    using (Aes myAes = Aes.Create())
                    {
                        byte[] encrypted =  AESHelper.EncryptStringToBytes_Aes(fileContent, myAes.Key, myAes.IV);

                        for (int i = 0; i < encrypted.Length; i++){
                            text += encrypted[i];
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
                string filePath = dialog.FileName;

                //Read the contents of the file into a stream
                Stream fileStream = dialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    //byte[] encrypted = System.Convert.FromBase64String(reader.ReadToEnd());
                    byte[] encrypted = Encoding.ASCII.GetBytes(reader.ReadToEnd());
                    var cipherText = System.Text.Encoding.UTF8.GetString(encrypted);
                    //byte[] cipherText = Encoding.UTF8.GetBytes(encrypted);
                    using (Aes myAes = Aes.Create())
                    {
                        string plaintext = AESHelper.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                        TxtOutput.Text = cipherText;
                    }
                    //using (StreamWriter writer = new StreamWriter(dialog.FileName + "Encrypted.txt"))
                    //{
                    //    writer.WriteLine(System.Convert.ToBase64String(encrypted));
                    //}
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Alle bestanden (*.*)|*.*|Tekstbestanden (*.txt)|*.txt",
                FilterIndex = 2,
                Title = "Geef een bestandsnaam op",
                OverwritePrompt = true, // bevestiging vragen bij overschrijven van een bestand
                AddExtension = true, // extensie wordt toegevoegd
                DefaultExt = "txt", // standaard extensie
                FileName = "EncryptedData.txt",
                InitialDirectory = Environment.CurrentDirectory // onder onze \Debug map
            };
            if (sfd.ShowDialog() == true) // als de SaveFileDialog getoond kan worden
            {
                // volledig pad en bestandsnaam opvragen
                string bestandsnaam = sfd.FileName;

                if (!File.Exists(bestandsnaam)) // controleer of bestand nog niet bestaat
                {
                    using (StreamWriter sw = File.CreateText(bestandsnaam)) // maak StreamWriter en maak bestand aan
                    {
                        sw.WriteLine(TxtOutput.Text);
                    }
                }

            }
        }
    }
}
