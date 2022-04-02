using UnityEngine;

namespace SpriteLogic
{
    public class SpriteLogic : MonoBehaviour
    {
        private void OnMouseDown()
        {
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Deleting) Destroy(gameObject);
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Selecting)
                ProjectSettings.instance.selectedGameObject = transform;
        }

        private void OnMouseDrag()
        {
            if (ProjectSettings.instance.selectedGameObject != transform) return;
            if (GlobalSettings.instance.drawingMode == GlobalSettings.DrawingModes.Selecting
               )
            {
                var worldPosition = GlobalSettings.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                var vec = GetComponent<SpriteRenderer>().bounds;
                // Debug.Log(vec);
                Debug.Log(vec);
                transform.position = worldPosition /*- new Vector3(Mathf.Abs(worldPosition.x - vec.center.x),
                    Mathf.Abs(worldPosition.x - vec.center.y), 0)*/;
            }
        }
    }
}