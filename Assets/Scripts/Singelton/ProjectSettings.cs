using SavingSerializing.Definitions;
using UnityEngine;

public class ProjectSettings : MonoBehaviour
{
    public Transform selectedGameObject;
    public Transform drawingGameObject;
    public Sprite selectedSprite;
    public float speed = 10.5f;
    public ProjectDefinition projectDefinition;
    public static ProjectSettings instance { get; private set; }

    private void Awake()
    {
        projectDefinition = new ProjectDefinition
        {
            sprites = new SpriteDefinition()
        };
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