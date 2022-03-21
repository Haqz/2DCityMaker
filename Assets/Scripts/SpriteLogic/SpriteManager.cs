using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace SpriteLogic
{
    public class SpriteManager : MonoBehaviour
    {
        public JSONDef spriteJSON;
        public Dropdown drop;

        private void Start()
        {
            try
            {
                LoadJson();
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }
        }

        public void LoadJson(string path = "./Scripts/sprites.json")
        {
            var text = File.ReadAllText(Application.dataPath + path);
            if (text != "")
                try
                {
                    spriteJSON = JsonUtility.FromJson<JSONDef>(text);
                    UpdateSpritesList();
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException("Loaded file doesnt match class definition");
                }
            else
                throw new Exception("Opened empty file");
        }

        public void SaveJson()
        {
            try
            {
                var newJson = JsonUtility.ToJson(spriteJSON);
                File.WriteAllText(Application.dataPath + "./Scripts/sprites.json", newJson);
            }
            catch (Exception ex)
            {
                throw new Exception("There was issue with saving json file.");
            }
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

        public void OpenSpriteFromLocal()
        {
            var file = EditorUtility.OpenFilePanel("Select sprite texture", "", "png");
            if (spriteJSON.files.Contains(file))
            {
                UpdateSpritesList();
                SaveJson();
            }
            else
            {
                spriteJSON.files.Add(file);
                UpdateSpritesList();
                SaveJson();
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