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
        private RSACryptoServiceProvider CryptoServiceProvider { get; set; }
        private RSAParameters PrivateKey { get; set; }
        private RSAParameters PublicKey { get; set; }

        public RSAWindow()
        {
            InitializeComponent();
            CryptoServiceProvider = new RSACryptoServiceProvider(2048); //2048 - Długość klucza
            PrivateKey = CryptoServiceProvider.ExportParameters(true); //Generowanie klucza prywatnego
            PublicKey = CryptoServiceProvider.ExportParameters(false); //Generowanie klucza publiczny

            /*
            string publicKeyString = RSAHelper.GetKeyString(PublicKey);
            string privateKeyString = RSAHelper.GetKeyString(PrivateKey);
            */
        }

        private void BtnFileEncrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFileDecrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string privateKeyString = RSAHelper.GetKeyString(PrivateKey);
            using (RSA myRSA = RSA.Create())
            {
                var encrypted = RSAHelper.Encrypt(TxtInput.Text, privateKeyString);
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
            string privateKeyString = RSAHelper.GetKeyString(PrivateKey);
            using (RSA myAes = RSA.Create())
            {
                // Convert a C# string to a byte array  
                // var encrypted = Encoding.ASCII.GetBytes(TxtInput.Text);
                string roundtrip = RSAHelper.Decrypt(TxtInput.Text, privateKeyString);
                TxtOutput.Text = roundtrip;
            }
        }
    }
}
