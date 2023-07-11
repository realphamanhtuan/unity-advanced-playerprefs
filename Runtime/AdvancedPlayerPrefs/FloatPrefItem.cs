using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityAdvancedPlayerPrefs{
    internal class FloatPrefItem : PrefItem{
        internal float value;
        internal FloatPrefItem(float value){
            this.value = value;
        }
        internal override int GetRawBytesLength()
        {
            return 4;
        }
        internal override byte[] ExportRawBytes()
        {
            return ByteHelper.FloatToBytesLittleEndian(value, 4);
        }
        internal override bool ImportRawBytes(byte[] bytes)
        {
            if (bytes.Length != 4) return false;
            value = ByteHelper.BytesToFloatLittleEndian(bytes);
            return true;
        }
    }
}