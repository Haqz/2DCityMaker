using System;
using System.IO;
using System.Linq;
using Helpers;
using SavingSerializing;
using SFB;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteLogic
{
    public class SpriteManager : MonoBehaviour
    {
        public SpriteDefinition spriteJSON;
        public Dropdown drop;
        public GameObject spriteButtonPrefab;
        public GameObject contentSprites;
        private string savePath = "";

        private void Start()
        {
            spriteJSON = new SpriteDefinition();
        }

        private void CopyOldToNew(SpriteDefinition spriteDefinition)
        {
            foreach (var v in spriteDefinition.oldPaths)
                if (spriteDefinition.newPaths.ContainsKey(v.Key) && spriteDefinition.newPaths.ContainsValue(v.Value))
                {
                    Debug.LogWarning("That file already exists");
                }
                else if (spriteDefinition.newPaths.ContainsKey(v.Key) &&
                         !spriteDefinition.newPaths.ContainsValue(v.Value))
                {
                    Debug.LogWarning("That key already exists");
                }
                else if (!spriteDefinition.newPaths.ContainsKey(v.Key) &&
                         !spriteDefinition.newPaths.ContainsValue(v.Value))
                {
                    if (!File.Exists(Application.persistentDataPath + "/" + Path.GetFileName(v.Value)))
                        if (v.Value != null)
                            File.Copy(v.Value, Application.persistentDataPath + "/" + Path.GetFileName(v.Value));
                    spriteDefinition.newPaths.Add(v.Key,
                        Application.persistentDataPath + "/" + Path.GetFileName(v.Value));
                }
                else if (!spriteDefinition.newPaths.ContainsKey(v.Key) &&
                         spriteDefinition.newPaths.ContainsValue(v.Value))
                {
                    Debug.LogWarning("That value already exists");
                }
        }

        public void UpdateSpritesList()
        {
            CopyOldToNew(spriteJSON);
            drop.options.Clear();
            contentSprites.transform.DestroyAllChildren();
            foreach (var v in spriteJSON.newPaths)
            {
                var go = Instantiate(spriteButtonPrefab, contentSprites.transform, true);
                go.name = v.Key;
                var fileName = Path.GetFileNameWithoutExtension(v.Value);
                drop.options.Add(new Dropdown.OptionData(fileName));
            }

            SelectSprite(0);
        }

        public void SelectSprite(string spriteIndex)
        {
            var path = spriteJSON.newPaths[spriteIndex];
            StartCoroutine(SpriteHelper.GetTextureFromLocalPath(path));
        }

        public void SelectSprite(int spriteIndex)
        {
            var path = spriteJSON.newPaths.ElementAt(spriteIndex);
            StartCoroutine(SpriteHelper.GetTextureFromLocalPath(path.Value));
        }

        public void OpenNewSpriteFromLocal()
        {
            var files = StandaloneFileBrowser.OpenFilePanel("Select sprite list", "", "png", true);
            foreach (var file in files)
            {
                Debug.Log(file);
                if (!spriteJSON.oldPaths.ContainsValue(file))
                    spriteJSON.oldPaths.Add(Path.GetFileNameWithoutExtension(file), file);
            }

            SavingLoading.SaveHSF(savePath, spriteJSON);
            UpdateSpritesList();
        }

        public void LoadHaqzSpriteFileFromLocal()
        {
            var file = StandaloneFileBrowser.OpenFilePanel("Select sprite list", "", "hsf", false);
            try
            {
                spriteJSON = SavingLoading.LoadHSF(file[0]);
                savePath = file[0];
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Loading file failed " + ex.Message);
            }

            UpdateSpritesList();
        }

        public void SaveHaqzSpriteFileToLocal()
        {
            var file = StandaloneFileBrowser.SaveFilePanel("Save sprite list", "", "SpriteList", "hsf");
            try
            {
                if (savePath == "") savePath = file;
                SavingLoading.SaveHSF(file, spriteJSON);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Saving file failed " + ex.Message);
            }
        }
    }
}