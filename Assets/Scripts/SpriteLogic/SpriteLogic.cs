using UnityEngine;

namespace SpriteLogic
{
    public class SpriteLogic : MonoBehaviour
    {
        private void OnMouseDown()
        {
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Deleting) Destroy(gameObject);
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Selecting)
                CanvasSettings.instance.selectedGameObject = transform;
        }

        private void OnMouseDrag()
        {
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Selecting &&
                CanvasSettings.instance.selectedGameObject == transform)
            {
                var worldPosition = GlobalSettings.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                transform.position = worldPosition;
            }
        }
    }
}