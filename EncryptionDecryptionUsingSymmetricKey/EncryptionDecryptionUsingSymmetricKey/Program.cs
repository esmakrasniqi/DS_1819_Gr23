﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace EncryptionDecryptionUsingSymmetricKey
{
    class Program
    {
        static void Main(string[] args)
        {
            var celesi = "b14ca5898a4e4133bbce2ea2315a1916";

            //Console.WriteLine("Ju lutem shenoni nje celes sekret per algoritmin simetrik.");  
            //var celesi = Console.ReadLine();  

            Console.WriteLine("Ju lutem shenoni nje tekst per enkriptim:");
            var str = Console.ReadLine();
            var enkriptimi = AesOperation.EncryptString(celesi, str);
            Console.WriteLine($"Teksti i enkriptuar = {enkriptimi}");

            var dekriptimi = AesOperation.DecryptString(celesi, enkriptimi);
            Console.WriteLine($"Teksti i dekriptuar = {dekriptimi}");

            Console.ReadKey();


        }
    }
}
public class AesOperation
{
    public static string EncryptString(string celesi, string plainText)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(celesi);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public static string DecryptString(string celesi, string cipherText)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(celesi);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
    public static byte Reverse(byte inByte)
    {
        return (byte)~inByte;
    }
}

