using System;
using UnityEngine;

public class CanvasSettings : MonoBehaviour
{
        private static CanvasSettings _instance;
        public static CanvasSettings instance
        {
                get { return _instance; }
        }

        public Transform selectedGameObject = null;
        public Transform drawingGameObject = null;
        public UnityEngine.Sprite selectedSprite = null;
        public float speed = 10.5f;
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
}