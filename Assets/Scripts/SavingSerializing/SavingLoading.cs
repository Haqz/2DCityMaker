using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SavingSerializing.Definitions;

namespace SavingSerializing
{
    public class SavingLoading
    {
        public static ProjectDefinition LoadHSF(string path)
        {
            //Format the object as Binary  
            var formatter = new BinaryFormatter();

            //Reading the file from the server  
            var fs = File.Open(path, FileMode.Open);
            var obj = formatter.Deserialize(fs);
            fs.Flush();
            fs.Close();
            fs.Dispose();

            return (ProjectDefinition) obj;
        }

        public static void SaveHSF(string path, ProjectDefinition spriteDefinition)
        {
            Stream ms = File.OpenWrite(path);
            Stream ms_bak = File.OpenWrite(path + ".bak");

            //Format the object as Binary  
            var formatter = new BinaryFormatter();

            //It serialize the employee object  
            formatter.Serialize(ms, spriteDefinition);
            formatter.Serialize(ms_bak, spriteDefinition);
            ms.Flush();
            ms.Close();
            ms.Dispose();
            ms_bak.Flush();
            ms_bak.Close();
            ms_bak.Dispose();
        }
    }
}