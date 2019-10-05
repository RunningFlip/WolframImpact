using UnityEngine;


public static class DragSelectionHelper
{
    private static Texture2D _whiteTexture;

    private static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }
            return _whiteTexture;
        }
    }


    private static Rect GetScreenRect(Vector3 _screenPosition1, Vector3 _screenPosition2)
    {
        // Move origin from bottom left to top left
        _screenPosition1.y = Screen.height - _screenPosition1.y;
        _screenPosition2.y = Screen.height - _screenPosition2.y;

        // Calculate corners
        Vector3 topLeft = Vector3.Min(_screenPosition1, _screenPosition2);
        Vector3 bottomRight = Vector3.Max(_screenPosition1, _screenPosition2);

        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }


    private static void DrawScreenRect(Rect _rect, Color _color)
    {
        GUI.color = _color;
        GUI.DrawTexture(_rect, WhiteTexture);
        GUI.color = Color.white;
    }


    private static void DrawScreenRectBorder(Rect _rect, float _thickness, Color _color)
    {
        // Top
        DrawScreenRect(new Rect(_rect.xMin, _rect.yMin, _rect.width, _thickness), _color);

        // Left
        DrawScreenRect(new Rect(_rect.xMin, _rect.yMin, _thickness, _rect.height), _color);

        // Right
        DrawScreenRect(new Rect(_rect.xMax - _thickness, _rect.yMin, _thickness, _rect.height), _color);

        // Bottom
        DrawScreenRect(new Rect(_rect.xMin, _rect.yMax - _thickness, _rect.width, _thickness), _color);
    }


    public static void DrawDragSelectionBox(Vector3 _screenPosition1, Vector3 _screenPosition2, Color _backgroundColor, Color _strokeColor, int _thickness)
    {
        Rect rect = GetScreenRect(_screenPosition1, _screenPosition2);

        DrawScreenRect(rect, _backgroundColor);
        DrawScreenRectBorder(rect, _thickness, _strokeColor);
    }

}
