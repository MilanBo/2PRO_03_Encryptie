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
    /// Interaction logic for RSAWindow.xaml
    /// </summary>
    public partial class RSAWindow : Window
    {
        string _key;
        public RSAWindow(string key)
        {
            InitializeComponent();
            TxtKey.Content = RSAHelper.PrivateKeyString;
            _key = key;
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            using (RSA myRSA = RSA.Create())
            {
                var encrypted = RSAHelper.Encrypt(TxtInput.Text,RSAHelper.PrivateKeyString);
                string text = "";
                for (int i = 0; i < encrypted.Length; i++)
                {
                    text += encrypted[i].ToString();
                }
                TxtOutput.Text = text;
            }
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            using (RSA myAes = RSA.Create())
            {
                // Convert a C# string to a byte array  
                // var encrypted = Encoding.ASCII.GetBytes(TxtInput.Text);
                string roundtrip = RSAHelper.Decrypt(TxtInput.Text, RSAHelper.PrivateKeyString);
                TxtOutput.Text = roundtrip;
            }
        }

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
                        string ciphertext = RSAHelper.Encrypt(reader.ReadToEnd(), RSAHelper.PrivateKeyString);
                        TxtOutput.Text = ciphertext;
                }
            }
        }

        private void ChooseFileToDecrypt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString()
            };

            if (ofd.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(ofd.FileName))
                {
                        string plaintext = RSAHelper.Decrypt(reader.ReadToEnd(), RSAHelper.PrivateKeyString);
                        TxtOutput.Text = plaintext;
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

        private void SaveKey_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Alle bestanden (*.*)|*.*|Tekstbestanden (*.xml)|*.xml",
                FilterIndex = 2,
                Title = "Geef een bestandsnaam op",
                OverwritePrompt = true, // bevestiging vragen bij overschrijven van een bestand
                AddExtension = true, // extensie wordt toegevoegd
                DefaultExt = "xml", // standaard extensie
                FileName = $"{_key}RSA.xml",
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
                        sw.WriteLine(TxtKey.Content);
                    }
                }

            }
        }
    }
}
