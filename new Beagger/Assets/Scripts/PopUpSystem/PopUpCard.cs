using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpCard : MonoBehaviour
{
    public TextMeshProUGUI message;
    public Image imageRender;

    public void SetValues(string message, Sprite icon, float timewait)
    {
        this.message.text = message;
        this.imageRender.sprite = icon;
        Destroy(this.gameObject, timewait);
    }
}