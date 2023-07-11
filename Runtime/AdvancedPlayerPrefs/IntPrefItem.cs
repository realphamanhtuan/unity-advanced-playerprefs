using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityAdvancedPlayerPrefs{
    internal class IntPrefItem : PrefItem{
        internal int value;
        internal IntPrefItem(int value){
            this.value = value;
        }
        internal override int GetRawBytesLength()
        {
            return 4;
        }
        internal override byte[] ExportRawBytes()
        {
            return ByteHelper.IntToBytesLittleEndian(value, 4);
        }
        internal override bool ImportRawBytes(byte[] bytes)
        {
            value = ByteHelper.BytesToIntLittleEndian(bytes);
            return true;
        }
    }
}