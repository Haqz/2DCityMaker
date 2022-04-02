using SavingSerializing.Definitions;
using UnityEngine;

public class CreateSprite : MonoBehaviour
{
    public void instSprite()
    {
        var worldPosition = GlobalSettings.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        var instantiated = Instantiate(ProjectSettings.instance.drawingGameObject, worldPosition, Quaternion.identity);
        instantiated.GetComponent<SpriteRenderer>().sprite = ProjectSettings.instance.selectedSprite;
        ProjectSettings.instance.selectedGameObject = instantiated;
        //TODO: The fuck is this, need to change it
        ProjectSettings.instance.projectDefinition.sprites.Locations.Add(
            new SpriteDefinition.SpriteLocation(ProjectSettings.instance.drawingGameObject.name, worldPosition,
                new Vector3(0, 0, 0), new Vector3(0, 0, 0)));
    }

    public void rotate(Transform spra, float direction)
    {
        var value = ProjectSettings.instance.speed * direction * -1;
        spra.Rotate(0.0f, 0.0f, spra.transform.rotation.z + value, Space.Self);
    }
}