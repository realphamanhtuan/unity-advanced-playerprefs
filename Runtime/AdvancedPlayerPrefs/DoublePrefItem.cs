using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityAdvancedPlayerPrefs{
    internal class DoublePrefItem : PrefItem{
        internal double value;
        internal DoublePrefItem(double value){
            this.value = value;
        }
        internal override int GetRawBytesLength()
        {
            return 8;
        }
        internal override byte[] ExportRawBytes()
        {
            return ByteHelper.DoubleToBytesLittleEndian(value, 8);
        }
        internal override bool ImportRawBytes(byte[] bytes)
        {
            if (bytes.Length != 8) return false;
            value = ByteHelper.BytesToDoubleLittleEndian(bytes);
            return true;
        }
    }
}