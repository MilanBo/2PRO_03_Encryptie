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
        public RSAWindow()
        {
            InitializeComponent();
        }

        private void BtnFileEncrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFileDecrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            using (RSA myRSA = RSA.Create())
            {
                var encrypted = RSAHelper.Encrypt(TxtInput.Text, "12345");
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
                string roundtrip = RSAHelper.Decrypt(TxtInput.Text, "12345");
                TxtOutput.Text = roundtrip;
            }
        }
    }
}
