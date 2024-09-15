using System.Collections;
using UnityEngine;

public class UseItemsSystem : MonoBehaviour
{
    public static UseItemsSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    bool released = true;

    public void Atack(ItemData item)
    {
        if (released) StartCoroutine(AtackRotine(item));
    }

    public void Use(ItemData item)
    {
        if (released) StartCoroutine(UseRotine(item));
    }

    IEnumerator AtackRotine(ItemData item)
    {
        if (released)
        {
            released = false;

            if (item is Weapon weapon)
            {
                HandleAttack(weapon.mask, weapon.damage);
                yield return new WaitForSeconds(weapon.cadence);
            }
            else if (item is Tool tool)
            {
                HandleAttack(tool.mask, tool.damage);
                yield return new WaitForSeconds(tool.cadence);
            }

            released = true;
        }
    }

    IEnumerator UseRotine(ItemData item)
    {
        if (item is Consumable)
        {
            item.UseItem();
            yield return null;
            // adicionar lógica de segurar para comer itens, se necessário
        }
    }

    void HandleAttack(LayerMask mask, int damage)
    {
        Transform[] targets = AimSystem.Instance.Check(mask);
        if (targets.Length > 0)
        {
            foreach (Transform target in targets)
            {
                target.TryGetComponent(out IHitable i);
                i?.Hited(damage, transform,damage/10);
            }
            print("atacou vários alvos");
        }
    }

}
