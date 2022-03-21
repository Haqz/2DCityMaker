using UnityEngine;

public class CreateSprite : MonoBehaviour
{
    public void instSprite()
    {
        var worldPosition = GlobalSettings.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        var instantiated = Instantiate(CanvasSettings.instance.drawingGameObject, worldPosition, Quaternion.identity);
        instantiated.GetComponent<SpriteRenderer>().sprite = CanvasSettings.instance.selectedSprite;
        CanvasSettings.instance.selectedGameObject = instantiated;
    }

    public void rotate(Transform spra, float direction)
    {
        var value = CanvasSettings.instance.speed * direction * -1;
        spra.Rotate(0.0f, 0.0f, spra.transform.rotation.z + value, Space.Self);
    }
}