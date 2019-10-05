using UnityEngine;
using UnityEngine.UI;


public class SelectionTypeUI : MonoBehaviour
{
    [Header("Bars")]
    public CanvasGroup bars;
    public Transform canvasTransform;
    public Image healthBar;
    public Image energyBar;
    public float startAplha;

    [Header("Silhouette")]
    public SpriteRenderer silhouetteRenderer;
}
