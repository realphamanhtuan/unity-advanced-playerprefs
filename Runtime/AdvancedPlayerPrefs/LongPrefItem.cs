using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityAdvancedPlayerPrefs{
    internal class LongPrefItem : PrefItem{
        internal long value;
        internal LongPrefItem(long value){
            this.value = value;
        }
        internal override int GetRawBytesLength()
        {
            return 8;
        }
        internal override byte[] ExportRawBytes()
        {
            return ByteHelper.LongToBytesLittleEndian(value, 8);
        }
        internal override bool ImportRawBytes(byte[] bytes)
        {
            if (bytes.Length != 8) return false;
            value = ByteHelper.BytesToLongLittleEndian(bytes);
            return true;
        }
    }
}