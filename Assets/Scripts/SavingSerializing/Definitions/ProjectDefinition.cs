using System;

namespace SavingSerializing.Definitions
{
    [Serializable]
    public class ProjectDefinition
    {
        public string projectName;

        public SpriteDefinition sprites;

        public string uuid;

        public ProjectDefinition(string projectName = "New Project")
        {
            uuid = Guid.NewGuid().ToString();
            this.projectName = projectName;
        }
    }
}