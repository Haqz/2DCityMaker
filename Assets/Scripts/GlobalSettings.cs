using System;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
        private static GlobalSettings _instance;
        public static GlobalSettings instance
        {
                get { return _instance; }
        }

        public DrawingModes drawingMode = DrawingModes.Drawing;
        public enum DrawingModes  {
                Drawing =  0,
                Selecting = 1,
                Deleting = 2
        };
        private void Awake()
        {
                if (_instance == null)
                {
                        DontDestroyOnLoad(gameObject);
                        _instance = this;
                }
                else
                {
                        Destroy(gameObject);
                }
        }
        
        public static void changeMode(int mode)
        {
                instance.drawingMode = (DrawingModes)mode;
        }  
}