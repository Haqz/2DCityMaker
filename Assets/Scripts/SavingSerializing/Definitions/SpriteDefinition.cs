using System;
using System.Collections.Generic;
using UnityEngine;

namespace SavingSerializing.Definitions
{
    [Serializable]
    public class SpriteDefinition
    {
        public List<SpriteLocation> Locations = new List<SpriteLocation>();
        public Dictionary<string, string> newPaths = new Dictionary<string, string>();
        public Dictionary<string, string> oldPaths = new Dictionary<string, string>();

        [Serializable]
        public class SpriteLocation
        {
            public string name;
            public Vector3 position;
            public Vector3 rotation;
            public Vector3 scale;

            public SpriteLocation(string name, Vector3 pos, Vector3 rot, Vector3 sc)
            {
                this.name = name;
                position = pos;
                rotation = rot;
                scale = sc;
            }
        }
    }
}