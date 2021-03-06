using UnityEngine;

public class SafeAreaSetter : MonoBehaviour
{
    // ???????????? ?????????? UI ? ?????????? ???? (???? ?? ?????? ??????????? ??????????? ??????, ??? ? ?????? 12 ????????)
    [SerializeField] Canvas canvas;

    private RectTransform _panelSafeArea;

    private Rect _currentSafeArea = new Rect();

    private ScreenOrientation _currentOrientation = ScreenOrientation.Portrait;

    void Start()
    {
        _panelSafeArea = GetComponent<RectTransform>();
        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        if (_panelSafeArea == null)
            return;
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        Rect pixelRect = canvas.pixelRect;
        anchorMin.x /= pixelRect.width;
        anchorMin.y /= pixelRect.height;

        anchorMax.x /= pixelRect.width;
        anchorMax.y /= pixelRect.height;

        _panelSafeArea.anchorMin = anchorMin;
        _panelSafeArea.anchorMax = anchorMax;

        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;


    }
    void Update()
    {
        if ((_currentOrientation != Screen.orientation) ||
            (_currentSafeArea != Screen.safeArea)
           )
        {
            ApplySafeArea();
        }
    }
}
