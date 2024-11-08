using System.Collections;
using UnityEngine;

public class UseItemsSystem : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioAtackPegou;
    public static UseItemsSystem Instance { get; private set; }

    public Animator equipedItemAnimator;

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
        if (released) { StartCoroutine(AtackRotine(item)); }
    }

    public void Use(ItemData item)
    {
        if (released) { StartCoroutine(UseRotine(item)); }
    }

    IEnumerator AtackRotine(ItemData item)
    {
        if (released)
        {
            released = false;

            if (item is Weapon weapon)
            {
                HandleAttack(weapon.damage, item);

                if (equipedItemAnimator)
                {
                    equipedItemAnimator.SetTrigger("Use");
                    print("clicou");
                }

                yield return new WaitForSeconds(weapon.cadence);

            }
            else if (item is Tool tool)
            {
                HandleAttack(tool.damage, item);

                if (equipedItemAnimator)
                {
                    equipedItemAnimator.SetTrigger("Use");
                    print("clicou");
                }
                yield return new WaitForSeconds(tool.cadence);

            }
            released = true;
        }
    }

    IEnumerator UseRotine(ItemData item)
    {
        if (item is Consumable consumable)
        {

            Consumable a = item as Consumable;
            float consumeTime = a.time;
            float timePressed = 0.0f;
            bool actionTriggered = false;
            GeneralReferences.Instance.ActionUIManager.StartProgress(a.time, "Usando");
            while (Input.GetMouseButton(PlayerControlsManager.Instance.KeyBinding.mouseKey_use))
            {
                if (equipedItemAnimator)
                {
                    equipedItemAnimator.SetBool("Use", true);
                }

                timePressed += Time.deltaTime;

                if (timePressed >= consumeTime && !actionTriggered)
                {
                    consumable.UseItem();
                    actionTriggered = true;

                    equipedItemAnimator.SetBool("Use", false);

                    Inventory.Instance.RemoveItem(item, null);
                    GeneralReferences.Instance.ActionUIManager.StopProgress();

                    break;
                }

                yield return null; // Aguarda até o próximo frame
            }

            // Se o botão for solto antes de completar
            if (!actionTriggered)
            {
                if (equipedItemAnimator)
                {
                    equipedItemAnimator.SetBool("Use", false);
                }
                print("cancelou o uso do item");
                
                    GeneralReferences.Instance.ActionUIManager.StopProgress();
            }


            // Se o botão for solto antes de terminar o tempo, pode-se considerar uma ação cancelada aqui
        }
    }

    void HandleAttack(int damage, ItemData item)
    {
        Transform[] targets = AimSystem.Instance.Check(); // Obtém os alvos da mira

        if (targets.Length > 0)
        {
            foreach (Transform target in targets)
            {
                // Verifica se o alvo possui um componente IHitable
                IHitable hitTarget = target.GetComponent<IHitable>();
                if (hitTarget != null)
                {
                    // Aplica o dano no alvo que implementa IHitable
                    hitTarget.Hited(damage, transform, damage / 10, item);
                    audioSource.clip = audioAtackPegou;
                    audioSource.Play();
                    print($"Atacou {target.name}");
                }
            }
        }
    }
}
