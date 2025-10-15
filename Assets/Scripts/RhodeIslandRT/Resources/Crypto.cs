// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-07-17 00:48:16

using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

public class Crypto
{
    public static string TextAssetDecrypt(byte[] stream, bool hasRsa = true)
    {
        byte[] res = _TextAssetDecrypt(stream, hasRsa);
        return Encoding.UTF8.GetString(res);
    }
    public static string TableTextAssetDecrypt(byte[] stream, bool hasRsa = true)
    {
        byte[] res = _TextAssetDecrypt(stream, hasRsa);
        string s = Encoding.UTF8.GetString(res);
        if (string.IsNullOrEmpty(s) || !(s.StartsWith("{") && s.EndsWith("}")))
        {
            using BsonReader reader = new(new MemoryStream(res));
            StringBuilder sb = new();
            StringWriter sw = new(sb);
            using JsonTextWriter jWriter = new(sw);
            jWriter.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            jWriter.WriteToken(reader);
            return sb.ToString();
        }
        return s;
    }

    [Obsolete]
    public static string TableTextAssetDecrypt2(byte[] stream, bool hasRsa = true)
    {
        byte[] res = _TextAssetDecrypt(stream, hasRsa);
        try
        {
            using BsonReader reader = new(new MemoryStream(res));
            StringBuilder sb = new();
            StringWriter sw = new(sb);
            using JsonTextWriter jWriter = new(sw);
            jWriter.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            jWriter.WriteToken(reader);
            return sb.ToString();
        }
        catch
        {
            return Encoding.UTF8.GetString(res);
        }
    }

    private static byte[] _TextAssetDecrypt(byte[] stream, bool hasRsa = true)
    {
        string mask = "UITpAi82pHAWwnzqHRMCwPonJLIB3WCl"; //CHAT_MASK
        byte[] iv = new byte[16];
        byte[] data = hasRsa ? stream[128..] : stream;
        byte[] buffer = data[..16];
        byte[] xorMask = Encoding.UTF8.GetBytes(mask[16..]);
        for (int i = 0; i < 16; i++)
            iv[i] = (byte)(buffer[i] ^ xorMask[i]);
        return AESDecrypt(data[16..], Encoding.UTF8.GetBytes(mask[..16]), iv);
    }

    public static byte[] AESDecrypt(byte[] data, byte[] key, byte[] iv)
    {
        try
        {
            byte[] keyArray = new byte[key.Length];
            Array.Copy(key, keyArray, key.Length);
            byte[] decryptArray = new byte[data.Length];
            Array.Copy(data, decryptArray, data.Length);
            RijndaelManaged rDel = new();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.IV = iv;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] result = cTransform.TransformFinalBlock(decryptArray, 0, decryptArray.Length);
            return result;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }

    public static byte[] AESEncrypt(byte[] data, byte[] key, byte[] iv)
    {
        byte[] encryptArray = null;
        Rijndael Aes = Rijndael.Create();
        try
        {
            using MemoryStream Memory = new();
            using CryptoStream Encryptor = new(Memory, Aes.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            Encryptor.Write(data, 0, data.Length);
            Encryptor.FlushFinalBlock();
            encryptArray = Memory.ToArray();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            encryptArray = null;
        }
        return encryptArray;
    }
}
