using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

namespace UnityAdvancedPlayerPrefs{
    public class AdvancedPlayerPrefs{
        readonly string keyPrefix, password;
        readonly bool encrypted = true;
        readonly bool autosave = false;
        /// <summary>
        /// Initialize Advanced PlayerPrefs.
        /// </summary>
        /// <param name="keyPrefix">Data stored in the playerPrefs will have the key format of the following {keyPrefix}_{realKey}_value and {keyPrefix}_{realKey}_salt.</param>
        /// <param name="password">Leave the password empty or null to indicate plain data saving in the PlayerPrefs.</param>
        /// <param name="autosave">If set, AdvancedPlayerPrefs automatically saves changes to permanent storage on every Set() call. Otherwise, saving will be triggered onApplicationQuit(), similar to original PlayerPrefs.</param>
        public AdvancedPlayerPrefs(string keyPrefix = "", string password = "", bool autosave = false){
            if (string.IsNullOrWhiteSpace(keyPrefix)) keyPrefix = "";
            this.keyPrefix = keyPrefix;
            
            if (string.IsNullOrWhiteSpace(password)) encrypted = false;
            this.password = password;

            this.autosave = autosave;
            // Debug.Log($"Create GSM with prefix = {this.keyPrefix} and password = {this.password}");
        }
        private string GetValueKey(string key){
            return $"{keyPrefix}_{key}_value";
        }
        private string GetSaltKey(string key){
            return $"{keyPrefix}_{key}_salt";
        }
        byte[] GetRawBytes(string key){
            // Debug.Log($"CheckKey {GetValueKey(key)} {GetSaltKey(key)}");
            if (!PlayerPrefs.HasKey(GetValueKey(key))) return null;
            if (!PlayerPrefs.HasKey(GetSaltKey(key))) return null;

            // Debug.Log($"Get {GetValueKey(key)} -> {PlayerPrefs.GetString(GetValueKey(key))}");
            // Debug.Log($"Get {GetSaltKey(key)} -> {PlayerPrefs.GetString(GetSaltKey(key))}");
            try{
                string base64String = PlayerPrefs.GetString(GetValueKey(key));
                byte[] compressedBytes = System.Convert.FromBase64String(base64String);
                byte[] uncompressedBytes = ByteHelper.Decompress(compressedBytes);
                string salt = PlayerPrefs.GetString(GetSaltKey(key));

                byte[] rawBytes;
                if (encrypted)
                    rawBytes = ByteHelper.AES_Decrypt(uncompressedBytes, ByteHelper.ASCIIStringToBytes(password), ByteHelper.ASCIIStringToBytes(salt));
                else rawBytes = uncompressedBytes;
                
                return rawBytes;
            } catch {
                return null;
            }
        }
        void SetRawBytes(string key, byte[] rawBytes){
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            string salt = "";

            for(int i = 0; i < 32; i++)
                salt += characters[Random.Range(0, characters.Length)];

            byte[] encryptedBytes;
            if (encrypted)
                encryptedBytes = ByteHelper.AES_Encrypt(rawBytes, ByteHelper.ASCIIStringToBytes(password), ByteHelper.ASCIIStringToBytes(salt));
            else 
                encryptedBytes = rawBytes;

            // Debug.Log("encryptedBytes:" + ByteHelper.BytesToHexString(encryptedBytes));
            byte[] compressedBytes = ByteHelper.Compress(encryptedBytes);
            // Debug.Log("compressedBytes" + ByteHelper.BytesToHexString(compressedBytes));
            string base64String = System.Convert.ToBase64String(compressedBytes);
            PlayerPrefs.SetString(GetValueKey(key), base64String);
            PlayerPrefs.SetString(GetSaltKey(key), salt);
            // Debug.Log($"Set {GetValueKey(key)} {base64String}");
            // Debug.Log($"Set {GetSaltKey(key)} {salt}");

            if (this.autosave) 
                PlayerPrefs.Save();
        }

        public void Save(){
            PlayerPrefs.Save();
        }
        public bool GetBool(string key, bool defaultValue){
            byte[] rawBytes = GetRawBytes(key);
            if (rawBytes == null) return defaultValue;
            BoolPrefItem item = new BoolPrefItem(defaultValue);
            item.ImportRawBytes(rawBytes);
            return item.value;
        }
        public void SetBool(string key, bool value){
            BoolPrefItem item = new BoolPrefItem(value);
            SetRawBytes(key, item.ExportRawBytes());
        }
        public Color32 GetColor32(string key, Color32 defaultValue){
            byte[] rawBytes = GetRawBytes(key);
            if (rawBytes == null) return defaultValue;
            Color32PrefItem item = new Color32PrefItem(defaultValue);
            item.ImportRawBytes(rawBytes);
            return item.value;
        }
        public void SetColor32(string key, Color32 value){
            Color32PrefItem item = new Color32PrefItem(value);
            SetRawBytes(key, item.ExportRawBytes());
        }
        public double GetDouble(string key, double defaultValue){
            byte[] rawBytes = GetRawBytes(key);
            if (rawBytes == null) return defaultValue;
            DoublePrefItem item = new DoublePrefItem(defaultValue);
            item.ImportRawBytes(rawBytes);
            return item.value;
        }
        public void SetDouble(string key, double value){
            DoublePrefItem item = new DoublePrefItem(value);
            SetRawBytes(key, item.ExportRawBytes());
        }
        public float GetFloat(string key, float defaultValue){
            byte[] rawBytes = GetRawBytes(key);
            if (rawBytes == null) return defaultValue;
            FloatPrefItem item = new FloatPrefItem(defaultValue);
            item.ImportRawBytes(rawBytes);
            return item.value;
        }
        public void SetFloat(string key, float value){
            FloatPrefItem item = new FloatPrefItem(value);
            SetRawBytes(key, item.ExportRawBytes());
        }
        public int GetInt(string key, int defaultValue){
            byte[] rawBytes = GetRawBytes(key);
            if (rawBytes == null) return defaultValue;
            IntPrefItem item = new IntPrefItem(defaultValue);
            item.ImportRawBytes(rawBytes);
            return item.value;
        }
        public void SetInt(string key, int value){
            IntPrefItem item = new IntPrefItem(value);
            SetRawBytes(key, item.ExportRawBytes());
        }
        public long GetLong(string key, long defaultValue){
            byte[] rawBytes = GetRawBytes(key);
            if (rawBytes == null) return defaultValue;
            LongPrefItem item = new LongPrefItem(defaultValue);
            item.ImportRawBytes(rawBytes);
            return item.value;
        }
        public void SetLong(string key, long value){
            LongPrefItem item = new LongPrefItem(value);
            SetRawBytes(key, item.ExportRawBytes());
        }
        public string GetString(string key, string defaultValue){
            byte[] rawBytes = GetRawBytes(key);
            if (rawBytes == null) return defaultValue;
            StringPrefItem item = new StringPrefItem(defaultValue);
            item.ImportRawBytes(rawBytes);
            return item.value;
        }
        public void SetString(string key, string value){
            StringPrefItem item = new StringPrefItem(value);
            SetRawBytes(key, item.ExportRawBytes());
        }
        public void SetString(string key, string value, Encoding encoding){
            StringPrefItem item = new StringPrefItem(value, encoding);
            SetRawBytes(key, item.ExportRawBytes());
        }
    }
}