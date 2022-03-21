using UnityEngine;

public class CanvasSettings : MonoBehaviour
{
    public Transform selectedGameObject;
    public Transform drawingGameObject;
    public UnityEngine.Sprite selectedSprite;
    public float speed = 10.5f;
    public static CanvasSettings instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}