using System;
using System.Collections.Generic;

namespace SavingSerializing
{
    [Serializable]
    public class SpriteDefinition
    {
        public Dictionary<string, string> files = new Dictionary<string, string>();
    }
}