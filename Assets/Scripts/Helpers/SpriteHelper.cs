using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Helpers
{
    public class SpriteHelper
    {
        public static IEnumerator GetTextureFromLocalPath(string path)
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

        public static Sprite SpriteFromTexture2D(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}