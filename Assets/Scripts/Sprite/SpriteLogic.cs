using UnityEngine;

namespace Sprite
{
    public class SpriteLogic : MonoBehaviour
    {
        private void OnMouseDown()
        {
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Deleting)
            {
                Destroy(gameObject);  
            }
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Selecting)
            {
                CanvasSettings.instance.selectedGameObject = transform;
            }
            
        }
        void OnMouseDrag()
        {
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Selecting)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                transform.position = worldPosition;
            }
        }
    }
}
