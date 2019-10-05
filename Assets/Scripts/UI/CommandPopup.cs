using UnityEngine;


/// <summary>
/// Popup that is shown by hovering a specific time above a CommandButton.
/// It shows a short description of it's CommandConfig.
/// </summary>
public class CommandPopup : MonoBehaviour
{
    //Flag
    private bool showPopup;

    //Strings
    private string headlineText;
    private string descriptionText;

    //Font  
    private Font font;
    private int fontSize;
    private Color fontColor;

    //Size
    private float borderSize;
    private float headlineGap;
    private Vector2 origin;
    private Vector2 headlineSize;
    private Vector2 descriptionSize;

    //Texture
    private Texture2D popupTexture;  
    private Color backgroundColor;
    
    //Styles
    private GUIStyle styleBackground = new GUIStyle();
    private GUIStyle styleA = new GUIStyle();
    private GUIStyle styleB = new GUIStyle();
    
    


    private void Start ()
    {
        GUISetup();
    }


    /// <summary>
    /// Setup that is called once at start.
    /// </summary>
    private void GUISetup()
    {
        //Font
        font = GameController.Instance.GameParameter.font;
        fontSize = GameController.Instance.GameParameter.fontSize;
        fontColor = GameController.Instance.GameParameter.fontColor;
        backgroundColor = GameController.Instance.GameParameter.CommandPopupColor;

        //Size
        borderSize = GameController.Instance.GameParameter.borderSize;
        headlineGap = GameController.Instance.GameParameter.headlineGap;
        headlineGap += borderSize;

        //Texture
        popupTexture = new Texture2D(32, 32);
        for (int y = 0; y < popupTexture.height; ++y)
        {
            for (int x = 0; x < popupTexture.width; ++x)
            {
                popupTexture.SetPixel(x, y, backgroundColor);
            }
        }
        popupTexture.Apply();

        //Background Style
        styleBackground.normal.background = popupTexture;

        //Headline Style
        styleA.alignment = TextAnchor.MiddleLeft;
        styleA.font = font;
        styleA.fontSize = fontSize;
        styleA.fontStyle = FontStyle.Bold;
        styleA.normal.textColor = fontColor;

        //Description Style
        styleB.alignment = TextAnchor.MiddleLeft;
        styleB.font = font;
        styleB.fontSize = fontSize;
        styleB.normal.textColor = fontColor;
    }


    public void SetPopUp(bool _status, string _commandNameText = null, string _descriptionText = null, string _commandLetter = null)
    {
        showPopup = _status;

        if (_status)
        {
            headlineText = _commandNameText + " (" + _commandLetter + ")";
            descriptionText = _descriptionText;
        }
    }


    private void OnGUI()
    {
        if (showPopup)
        {
            headlineSize = styleA.CalcSize(new GUIContent(headlineText));
            descriptionSize = styleB.CalcSize(new GUIContent(descriptionText));

            float boxWidth = (headlineSize.x < descriptionSize.x) 
                ? descriptionSize.x + (borderSize * 2) 
                : headlineSize.x + (borderSize * 2);
            float boxHeight = headlineSize.y + descriptionSize.y + (borderSize * 3);

            origin = Input.mousePosition;

            if (!CheckPopupSpace(origin, boxWidth)) origin = GetNewOrigin(boxWidth, origin.y);

            GUI.BeginGroup(new Rect(origin.x, (Screen.height - origin.y) - boxHeight, boxWidth, boxHeight), styleBackground);
                GUI.Label(new Rect(borderSize, borderSize, boxWidth, headlineSize.y), headlineText, styleA);
                GUI.Label(new Rect(borderSize, headlineGap, boxWidth, descriptionSize.y), descriptionText, styleB);
            GUI.EndGroup();
        }
    }


    private bool CheckPopupSpace(Vector2 _origin, float _boxWidth)
    {
        bool enoughSpace = true;
        float right = _origin.x + _boxWidth;

        if (right > Screen.width)
        {
            enoughSpace = false;
        }
        return enoughSpace;
    }


    private Vector2 GetNewOrigin(float _boxWidth, float _originY)
    {
        float originX = Screen.width - _boxWidth;
        return new Vector2(originX, _originY);
    }
}
