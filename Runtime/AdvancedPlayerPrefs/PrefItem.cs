using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace UnityAdvancedPlayerPrefs{
    internal abstract class PrefItem {
        internal abstract int GetRawBytesLength();
        internal abstract byte[] ExportRawBytes();
        internal abstract bool ImportRawBytes(byte[] bytes);
    }
}