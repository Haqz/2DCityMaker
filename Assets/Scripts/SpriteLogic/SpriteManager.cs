using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SFB;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace SpriteLogic
{
    public class SpriteManager : MonoBehaviour
    {
        public JSONDef spriteJSON;
        public Dropdown drop;
        private string savePath = "";

        public void LoadJson(string path)
        {
            //Format the object as Binary  
            var formatter = new BinaryFormatter();

            //Reading the file from the server  
            var fs = File.Open(path, FileMode.Open);
            var obj = formatter.Deserialize(fs);
            spriteJSON = (JSONDef) obj;
            UpdateSpritesList();
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        public void SaveJson(string path)
        {
            Stream ms = File.OpenWrite(path);
            Stream ms_bak = File.OpenWrite(path + ".bak");

            //Format the object as Binary  
            var formatter = new BinaryFormatter();

            //It serialize the employee object  
            formatter.Serialize(ms, spriteJSON);
            formatter.Serialize(ms_bak, spriteJSON);
            ms.Flush();
            ms.Close();
            ms.Dispose();
        }

        public void UpdateSpritesList()
        {
            drop.options.Clear();
            foreach (var v in spriteJSON.files)
            {
                var fileName = Path.GetFileNameWithoutExtension(v);
                drop.options.Add(new Dropdown.OptionData(fileName));
            }
        }

        public void SelectSprite(int spriteIndex)
        {
            var path = spriteJSON.files[spriteIndex];
            StartCoroutine(GetTexture(path));
        }

        public void OpenNewSpriteFromLocal()
        {
            var files = StandaloneFileBrowser.OpenFilePanel("Select sprite list", "", "png", true);
            foreach (var file in files)
                if (!spriteJSON.files.Contains(file))
                    spriteJSON.files.Add(file);

            SaveJson(savePath);
            UpdateSpritesList();
        }

        public void LoadHaqzSpriteFileFromLocal()
        {
            var file = StandaloneFileBrowser.OpenFilePanel("Select sprite list", "", "hsf", false);
            try
            {
                LoadJson(file[0]);
                savePath = file[0];
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Loading file failed " + ex.Message);
            }
        }

        public void SaveHaqzSpriteFileToLocal()
        {
            var file = StandaloneFileBrowser.SaveFilePanel("Save sprite list", "", "SpriteList", "hsf");
            try
            {
                SaveJson(file);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Saving file failed " + ex.Message);
            }
        }

        private Sprite SpriteFromTexture2D(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
        }

        private IEnumerator GetTexture(string path)
        {
            var www = UnityWebRequestTexture.GetTexture("file:///" + path);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var webTexture = ((DownloadHandlerTexture) www.downloadHandler).texture;
                var webSprite = SpriteFromTexture2D(webTexture);
                CanvasSettings.instance.selectedSprite = webSprite;
            }
        }

        [Serializable]
        public class JSONDef
        {
            public List<string> files;
        }
    }
}