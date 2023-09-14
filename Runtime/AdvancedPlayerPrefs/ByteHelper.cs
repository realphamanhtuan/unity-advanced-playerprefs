using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System;

namespace UnityAdvancedPlayerPrefs{
    internal static class ByteHelper{
        internal static string GetASCIIStringHash(string s, int length){
            return System.Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(ASCIIStringToBytes(s))).Substring(0, length);
        }
        internal static byte[] GetByteHash(byte[] bytes, int length){
            return SliceByteArray(System.Security.Cryptography.SHA256.Create().ComputeHash(bytes), 0, length);
        }
        internal static bool AreBytesEqual(byte[] bytes1, byte[] bytes2){
            if (bytes1.Length != bytes2.Length) return false;
            for (int i = 0; i < bytes1.Length; ++i)
                if (bytes1[i] != bytes2[i]) return false;
            return true;
        }
        internal static byte[] SliceByteArray(byte[] bytes, int index, int length){
            byte[] ret = new byte[length];
            for (int i = 0; i < length; ++i)
                ret[i] = bytes[index + i];
            return ret;
        }
        internal static byte[] ConcatenateByteArrays(params byte[][] byteArrays){
            int length = 0;
            for (int i = 0; i < byteArrays.Length; ++i){
                //Tebug.Log(i, byteArrays[i].Length, BytesToHexString(SliceByteArray(byteArrays[i], 0, 4)));
                length += byteArrays[i].Length;
            }
            byte[] ret = new byte[length];
            int index = 0;
            for (int i = 0; i < byteArrays.Length; ++i){
                byteArrays[i].CopyTo(ret, index);
                index += byteArrays[i].Length;
            }
            return ret;
        }
        /// <summary>
        /// the result will be the byte representation of (x % (2**length)). In other words, only length least significant bytes are returned.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static byte[] IntToBytesLittleEndian(int x, int length){
            byte[] bytes = System.BitConverter.GetBytes(x);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return SliceByteArray(bytes, 0, length);
        }
        internal static int BytesToIntLittleEndian(byte[] bytes){
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// the result will be the byte representation of (x % (2**length)). In other words, only length least significant bytes are returned.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static byte[] LongToBytesLittleEndian(long x, int length){
            byte[] bytes = System.BitConverter.GetBytes(x);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return SliceByteArray(bytes, 0, length);
        }
        internal static long BytesToLongLittleEndian(byte[] bytes){
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }
        /// <summary>
        /// the result will be the byte representation of (x % (2**length)). In other words, only length least significant bytes are returned.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static byte[] FloatToBytesLittleEndian(float x, int length){
            byte[] bytes = System.BitConverter.GetBytes(x);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return SliceByteArray(bytes, 0, length);
        }
        internal static float BytesToFloatLittleEndian(byte[] bytes){
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }
        /// <summary>
        /// the result will be the byte representation of (x % (2**length)). In other words, only length least significant bytes are returned.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static byte[] DoubleToBytesLittleEndian(double x, int length){
            byte[] bytes = System.BitConverter.GetBytes(x);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return SliceByteArray(bytes, 0, length);
        }
        internal static double BytesToDoubleLittleEndian(byte[] bytes){
            if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }
        internal static byte[] ASCIIStringToBytes(string s){
            return System.Text.Encoding.ASCII.GetBytes(s);
        }
        internal static string BytesToHexString(byte[] bytes){
            System.Text.StringBuilder hex = new System.Text.StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        internal static string BytesToString(byte[] bytes){
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        internal static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, System.IO.Compression.CompressionLevel.Fastest))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        internal static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
        internal static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes, byte[] saltBytes)
        {
            byte[] encryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    AES.Padding = PaddingMode.PKCS7;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        internal static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes, byte[] saltBytes)
        {
            byte[] decryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    AES.Padding = PaddingMode.PKCS7;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
    }
}