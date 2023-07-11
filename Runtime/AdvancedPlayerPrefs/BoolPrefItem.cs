using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityAdvancedPlayerPrefs{
    internal class BoolPrefItem : PrefItem{
        internal bool value;
        internal BoolPrefItem(bool value){
            this.value = value;
        }
        internal override int GetRawBytesLength()
        {
            return 1;
        }
        internal override byte[] ExportRawBytes()
        {
            byte[] ret = new byte[1];
            if (!value) ret[0] = 0;
            else ret[0] = 1;
            return ret;
        }
        internal override bool ImportRawBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 1) return false;
            value = !(bytes[0] == 0);
            return true;
        }
    }
}