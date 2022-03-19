using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Sprite
{
    public class SpriteManager : MonoBehaviour
    {
        public JSONDef spriteJSON;

        private void Start()
        {
            spriteJSON = SpriteManager.loadJson();
        }

        [Serializable]
        public class JSONDef
        {
            public List<string> files;
        }
        static JSONDef loadJson(string path = "./Assets/Scripts/sprites.json")
        {
            string text = File.ReadAllText(path);
            return JsonUtility.FromJson<JSONDef>(text);
        }

        public void saveJson()
        {
            var newJson = JsonUtility.ToJson(spriteJSON);
            System.IO.File.WriteAllText("./Assets/Scripts/sprites.json", newJson);
        }
        public void selectSprite(int spriteIndex)
        {
            path = spriteJSON.files[spriteIndex];
            StartCoroutine(GetTexture());
        }

        private string path;
        
        public void loadSprite()
        {
            spriteJSON.files.Add(
                EditorUtility.OpenFilePanel("Select sprite texture", "", "png"));
            // StartCoroutine(GetTexture());
        }
        UnityEngine.Sprite SpriteFromTexture2D(Texture2D texture) {

            return UnityEngine.Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        
        IEnumerator GetTexture()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///"+path);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D webTexture = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                UnityEngine.Sprite webSprite = SpriteFromTexture2D (webTexture);
                CanvasSettings.instance.selectedSprite = webSprite;
            }
        }
    }
}