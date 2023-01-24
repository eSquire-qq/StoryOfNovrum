using UnityEngine;

public static class Utils 
{
    public static void HideIfClickedOutside(GameObject panel) {
        if (Input.GetMouseButton(0) && panel.activeSelf && 
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(), 
                Input.mousePosition, 
                Camera.main)) {
            panel.SetActive(false);
        }
    }
}