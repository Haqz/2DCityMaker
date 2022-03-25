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
        private string savePath = "";

        private void Start()
        {
            spriteJSON = new SpriteDefinition();
        }

        public void UpdateSpritesList()
        {
            drop.options.Clear();
            foreach (var v in spriteJSON.files)
            {
                var fileName = Path.GetFileNameWithoutExtension(v.Value);
                drop.options.Add(new Dropdown.OptionData(fileName));
            }

            SelectSprite(0);
        }

        public void SelectSprite(string spriteIndex)
        {
            var path = spriteJSON.files[spriteIndex];
            StartCoroutine(SpriteHelper.GetTextureFromLocalPath(path));
        }

        public void SelectSprite(int spriteIndex)
        {
            var path = spriteJSON.files.ElementAt(spriteIndex);
            StartCoroutine(SpriteHelper.GetTextureFromLocalPath(path.Value));
        }

        public void OpenNewSpriteFromLocal()
        {
            var files = StandaloneFileBrowser.OpenFilePanel("Select sprite list", "", "png", true);
            foreach (var file in files)
                if (!spriteJSON.files.ContainsValue(file))
                    spriteJSON.files.Add(Path.GetFileNameWithoutExtension(file), file);

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