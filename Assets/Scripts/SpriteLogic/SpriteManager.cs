using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DigitalRuby.AdvancedPolygonCollider;
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
            LoadJson();
        }

        public void LoadJson(string path = "./Assets/Scripts/sprites.json")
        {
            var text = File.ReadAllText(path);
            spriteJSON = JsonUtility.FromJson<JSONDef>(text);
            foreach (var v in spriteJSON.files) drop.options.Add(new Dropdown.OptionData("New"));
        }

        public void UpdateSpritesList()
        {
            drop.options.Clear();
            foreach (var v in spriteJSON.files) drop.options.Add(new Dropdown.OptionData("New"));
        }

        public void SaveJson()
        {
            var newJson = JsonUtility.ToJson(spriteJSON);
            File.WriteAllText("./Assets/Scripts/sprites.json", newJson);
        }

        public void SelectSprite(int spriteIndex)
        {
            var path = spriteJSON.files[spriteIndex];
            StartCoroutine(GetTexture(path));
        }

        public void OpenSpriteFromLocal()
        {
            spriteJSON.files.Add(
                EditorUtility.OpenFilePanel("Select sprite texture", "", "png"));
            UpdateSpritesList();
            SaveJson();
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
                ;

                var d_go = CanvasSettings.instance.drawingGameObject;
                if (d_go.GetComponent<SpriteRenderer>().sprite)
                    d_go.GetComponent<AdvancedPolygonCollider>().RecalculatePolygon();
            }
        }

        [Serializable]
        public class JSONDef
        {
            public List<string> files;
        }
    }
}