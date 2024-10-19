using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrunkSlot : MonoBehaviour
{
    
    [SerializeField] AudioClip clip_click;
    [SerializeField] AudioClip clip_mouseEnter;
    public ItemData item;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI lbl_itemName;
    [SerializeField] TextMeshProUGUI lbl_quant;

    TrunkSystem sysMan;
    public void ClearSlot()
    {

    }
    public void SetSlot(TrunkItems item, TrunkSystem sysMan)
    {
        icon.sprite = item.item.icon;
        lbl_itemName.text = item.item.name;
        lbl_quant.text = item.quant.ToString() + "x";

        this.item = item.item;
        this.sysMan = sysMan;
    }
    public void ToInventory()
    {
        sysMan.ToInventory(gameObject);
    }
    public void ToTrunk()
    {
        sysMan.ToTrunk(gameObject);
    }
    public void OnPointerEnter()
    {
        GeneralReferences.Instance.UIAudioSource.clip = clip_mouseEnter;
        GeneralReferences.Instance.UIAudioSource.Play();
    }
    public void OnPointerClick()
    {
        GeneralReferences.Instance.UIAudioSource.clip = clip_click;
        GeneralReferences.Instance.UIAudioSource.Play();
    }
}