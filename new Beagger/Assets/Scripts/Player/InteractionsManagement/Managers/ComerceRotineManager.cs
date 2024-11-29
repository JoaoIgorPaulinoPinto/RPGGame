using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComerceRotineManager : MonoBehaviour
{
    public Door? targetDoor;
    public Store? store;
    public int timeClose;
    public int timeOpen;
    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closeSprite;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Update()
    {
        float currentHour = (TimeController.Instance.dayCount * 24f + (TimeController.Instance.dayTimer / (float)TimeController.Instance.dayDuration) * 24f) % 24f;
        if (targetDoor)
        {
            if (currentHour <= timeClose && currentHour >= timeOpen)
            {
                spriteRenderer.sprite = openSprite;
                targetDoor.locked = false;
            }
            else
            {
                spriteRenderer.sprite = closeSprite;
                targetDoor.locked = true;
            }
        }
        if(store)
        {
            if (currentHour <= timeClose && currentHour >= timeOpen)
            {
                spriteRenderer.sprite = openSprite;
                store.isOpen = true;
            }
            else
            {
                spriteRenderer.sprite = closeSprite;
                store.isOpen = false;

            }
        }
       
    }

}
