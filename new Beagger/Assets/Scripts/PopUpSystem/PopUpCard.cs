using System.Collections;
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
    //IEnumerator DestrouPopUpCard(float timewait)
    //{
    //    yield return new WaitForSeconds(timewait);
    //    this.gameObject.TryGetComponent<Animator>(out Animator animator);
    //    animator.SetBool("PopUp", false);
    //    Destroy(this.gameObject, 0.5f);
    //}
}