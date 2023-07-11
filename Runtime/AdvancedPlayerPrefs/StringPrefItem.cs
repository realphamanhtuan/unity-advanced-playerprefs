using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Text;

namespace UnityAdvancedPlayerPrefs{
    internal class StringPrefItem : PrefItem{
        internal string value;
        private Encoding encoding;
        internal StringPrefItem(string value, Encoding encoding){
            this.value = value;
            this.encoding = encoding;
        }
        internal StringPrefItem(string value){
            this.value = value;
            this.encoding = Encoding.ASCII;
        }
        internal override int GetRawBytesLength()
        {
            if (string.IsNullOrEmpty(value)) return 4;
            return 4 + encoding.GetByteCount(value);
        }
        internal override byte[] ExportRawBytes()
        {
            if (string.IsNullOrEmpty(value)) return ByteHelper.IntToBytesLittleEndian(encoding.CodePage, 4);
            return ByteHelper.ConcatenateByteArrays(ByteHelper.IntToBytesLittleEndian(encoding.CodePage, 4), encoding.GetBytes(value));
        }
        internal override bool ImportRawBytes(byte[] bytes)
        {
            if (bytes.Length < 4) return false;
            encoding = Encoding.GetEncoding(ByteHelper.BytesToIntLittleEndian(ByteHelper.SliceByteArray(bytes, 0, 4)));
            if (encoding == null) return false;
            if (bytes.Length == 4) value = "";
            else 
                value = encoding.GetString(ByteHelper.SliceByteArray(bytes, 4, bytes.Length - 4));
            return true;
        }
    }
}