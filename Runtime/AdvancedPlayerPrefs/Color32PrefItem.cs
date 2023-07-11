using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityAdvancedPlayerPrefs{
    internal class Color32PrefItem : PrefItem{
        internal Color32 value;
        internal Color32PrefItem(Color32 value){
            this.value = value;
        }
        internal override int GetRawBytesLength()
        {
            return 4;
        }
        internal override byte[] ExportRawBytes()
        {
            return new byte[]{value.r, value.g, value.b, value.a};
        }
        internal override bool ImportRawBytes(byte[] bytes)
        {
            if (bytes.Length < 4) return false;
            value.r = bytes[0];
            value.g = bytes[1];
            value.b = bytes[2];
            value.a = bytes[3];
            return true;
        }
    }
}