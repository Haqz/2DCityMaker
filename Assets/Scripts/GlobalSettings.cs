using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public enum DrawingModes
    {
        Drawing = 0,
        Selecting = 1,
        Deleting = 2
    }

    public Camera mainCamera;
    public DrawingModes drawingMode = DrawingModes.Drawing;

    public static GlobalSettings instance { get; private set; }

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

    public static void changeMode(int mode)
    {
        instance.drawingMode = (DrawingModes) mode;
    }
}