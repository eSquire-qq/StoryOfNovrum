using UnityEngine;
using UnityEngine.UI;
 
/// <summary>
/// Bug with Graphic.set_raycastTarget and GraphicRegistry.UnregisterGraphicForCanvas observed in Unity 2020.2.1f1
/// To reproduce:
///  - Create a canvas in an empty scene
///  - Attach this script
///  - Enter play mode
///  - Interact with the Game window with your mouse and observe logged errors
///  - Other UI elements using graphic raycasting will also no longer work.
/// </summary>
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GraphicRaycaster))]
public class UIBugTestCase : MonoBehaviour
{
    private void Start()
    {
        // Set up the object - an inactive graphic on a canvas. (In this case an Image)
        var obj = new GameObject("Image");
        obj.AddComponent<RectTransform>();
        obj.AddComponent<CanvasRenderer>();
        var img = obj.AddComponent<Image>();
        // raycastTarget is set to false initially so it can be changed later to true.
        img.raycastTarget = false;
        obj.transform.SetParent(this.transform);
        obj.SetActive(false);
 
        // Now the object is inactive, we set raycastTarget to true, and it erroneously registers itself with
        // GraphicRegistry.RegisterRaycastGraphicForCanvas (even though it is inactive)
        img.raycastTarget = true;
 
        // Destroy the object.
        Destroy(obj);
        /*
         * When it is destroyed (at any point in the future provided it isn't activated), it still calls
         * GraphicRegistry.UnregisterGraphicForCanvas which usually calls GraphicRegistry.UnregisterRaycastGraphicForCanvas.
         * However as it is inactive, it's not in the canvas graphic list, only the canvas raycast graphic list.
         * UnregisterGraphicForCanvas assumes that the raycast graphics are a subset of the normal graphics list (not
         * true because of the raycastTarget = true bug). This causes it to not call UnregisterRaycastGraphicForCanvas,
         * leaving the destroyed object in the raycast graphic list, which throws errors in the graphic raycaster.
         *
         * At this point, if the bug hasn't been fixed then interacting with the Game window in play mode with the mouse should throw:
         * MissingReferenceException: The object of type 'CanvasRenderer' has been destroyed but you are still trying to access it.
         */
 
        /* Suggested fixes:
         *
         * Graphic.raycastTarget: setter should not call GraphicRegistry functions if IsActive() is false.
         *
         * GraphicRegistry.UnregisterGraphicForCanvas: call UnregisterRaycastGraphicForCanvas regardless of whether a list for
         * the current canvas is found - better defensive design and prevents future issues like this.
         */
    }
}
