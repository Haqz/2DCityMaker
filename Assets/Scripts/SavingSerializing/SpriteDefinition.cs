using System;
using System.Collections.Generic;

namespace SavingSerializing
{
    [Serializable]
    public class SpriteDefinition
    {
        public Dictionary<string, string> newPaths = new Dictionary<string, string>();
        public Dictionary<string, string> oldPaths = new Dictionary<string, string>();
    }
}