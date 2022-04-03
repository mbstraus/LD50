using UnityEngine;
using UnityEngine.UI;

public class LightPipHUD : MonoBehaviour
{
    [SerializeField]
    public Sprite LightPipActiveSprite;
    [SerializeField]
    public Sprite LightPipInactiveSprite;
    [SerializeField]
    public Image Image;

    public void SetActiveState(bool isActive) {
        if (isActive) {
            Image.sprite = LightPipActiveSprite;
        } else {
            Image.sprite = LightPipInactiveSprite;
        }
    }
}
