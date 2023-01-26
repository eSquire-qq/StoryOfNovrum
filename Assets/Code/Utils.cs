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

    public static Vector3 PositionBetween(Vector3 v1, Vector3 v2, float percentage)
    {
        return (v2 - v1) * percentage + v1;
    }
}