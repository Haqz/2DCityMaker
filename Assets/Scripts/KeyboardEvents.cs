using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardEvents : MonoBehaviour
{
private CreateSprite spra;
    void Start()
    {
        spra = gameObject.GetComponent<CreateSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        CanvasSettings.instance.speed = Input.GetKey(KeyCode.LeftShift) ? 5.0f : 10.0f;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0) && GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Drawing)
        {
            spra.instSprite();
        }

        if (Input.mouseScrollDelta.y != 0 
            && Input.GetKey(KeyCode.LeftControl)
            && CanvasSettings.instance.selectedGameObject != null 
            && GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Selecting)
        {
            spra.rotate(CanvasSettings.instance.selectedGameObject, Input.mouseScrollDelta.y);
        }
    }
}
