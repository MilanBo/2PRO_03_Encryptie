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
    /// bron : https://www.youtube.com/watch?v=LOmgFxPHop0
    public partial class AESWindow : Window
    {
        public AESWindow()
        {
            InitializeComponent();
            AESHelper.Init();
            TxtKey.Content = AESHelper.GetKey();
        }

        #region simple En-/Decrypt
        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
           TxtOutput.Text = AESHelper.Encrypt(TxtInput.Text);
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
          // Convert a C# string to a byte array  
          TxtOutput.Text = AESHelper.Decrypt(TxtInput.Text);
        }
        #endregion

        #region File En-/Decrypt
        private void ChooseFileToEncrypt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString()
            };

            if (ofd.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(ofd.FileName))
                {
                        string encrypted = AESHelper.Encrypt(reader.ReadToEnd());

                        TxtOutput.Text = encrypted;
                        using (StreamWriter writer = new StreamWriter(ofd.FileName+"encrypted.txt"))
                        {
                            writer.WriteLine(encrypted);
                        }
                }
            }
        }

        private void ChooseFileToDecrypt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() 
            {
                InitialDirectory= Environment.SpecialFolder.DesktopDirectory.ToString() 
            };

            if (ofd.ShowDialog() == true)
            {
                using (StreamReader sr = new StreamReader(ofd.FileName))
                {
                    using (Aes myAes = Aes.Create())
                    {
                        string plaintext = AESHelper.Decrypt(sr.ReadToEnd());

                        TxtOutput.Text = plaintext;
                        using (StreamWriter writer = new StreamWriter(ofd.FileName + "encrypted.txt"))
                        {
                            writer.WriteLine(plaintext);
                        }
                    }
                    
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
        #endregion
    }
}
