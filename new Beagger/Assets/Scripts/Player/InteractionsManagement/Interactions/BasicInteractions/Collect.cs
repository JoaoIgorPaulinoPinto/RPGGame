using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : InteractableGameObject, IInteractable
{
    [SerializeField] private List<ItemData> itensToCollect;
    [SerializeField] private float timeToReplace;
    [SerializeField] private float timeToCollect;

    [SerializeField] private Sprite defSprite;
    [SerializeField] private Sprite collectedSprite;
    [SerializeField] private ActionUIManager actionUIManager;
    [SerializeField] private bool canReplace;

    private bool isCollected = false;
    private SpriteRenderer spriteRenderer;
    private List<SpriteRenderer> childSpriteRenderers = new List<SpriteRenderer>();
    private Coroutine collectCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defSprite;

        // Pega todos os SpriteRenderers dos filhos e os adiciona à lista
        foreach (Transform child in transform)
        {
            SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();
            if (childRenderer != null)
            {
                childSpriteRenderers.Add(childRenderer);
            }
        }
    }

    private void Start()
    {
        if (actionUIManager == null)
        {
            actionUIManager = GeneralReferences.Instance.ActionUIManager;
            if (actionUIManager == null)
            {
                Debug.LogError("ActionUIManager não encontrado. Verifique a referência.");
            }
            else
            {
                Debug.Log("ActionUIManager encontrado com sucesso.");
            }
        }

        if (Inventory.Instance == null)
        {
            Debug.LogError("Inventory.Instance não está configurado. Verifique a inicialização do inventário.");
        }
    }

    public void Interact()
    {
        if (!isCollected)
        {
            Debug.Log("Iniciando coleta...");
            collectCoroutine = StartCoroutine(IECollect());
        }
        else
        {
            Debug.Log("Item já coletado.");
            GeneralReferences.Instance.InteractionUIManager.HideUI();
        }
    }

    public void StopInteraction()
    {
        if (collectCoroutine != null)
        {
            StopCoroutine(collectCoroutine);
            collectCoroutine = null;
            Debug.Log("Coleta cancelada.");

            actionUIManager.StopProgress(); // Para o progresso do slider
            spriteRenderer.sprite = defSprite;
        }
    }

    private IEnumerator IECollect()
    {
        Debug.Log("IECollect iniciado.");

        if (actionUIManager != null)
        {
            actionUIManager.StartProgress(timeToCollect, "Coletando...");
        }
        else
        {
            Debug.LogError("ActionUIManager não está configurado.");
        }

        float elapsedTime = 0f;
        while (elapsedTime < timeToCollect)
        {
            if (!Input.GetKey(PlayerControlsManager.Instance.KeyBinding.key_interact))
            {
                Debug.Log("Botão de interação liberado, coleta interrompida.");
                StopInteraction();
                PlayerControlsManager.Instance.realease = true;
                yield break;
            }
            elapsedTime += Time.deltaTime;
            Debug.Log($"Tempo de coleta: {elapsedTime}/{timeToCollect}");
            yield return null;
            PlayerControlsManager.Instance.realease = false;
        }
        PlayerControlsManager.Instance.realease = true;

        if (actionUIManager != null)
        {
            actionUIManager.StopProgress();
        }

        foreach (var item in itensToCollect)
        {
            if (Inventory.Instance != null)
            {
                Debug.Log($"Adicionando item ao inventário: {item.name}");
                isCollected = true;
                Inventory.Instance.AddItem(item);
            }
            else
            {
                Debug.LogError("Inventory.Instance está nulo. Item não pode ser adicionado.");
            }
        }

        Debug.Log("Atualizando sprite para coletado.");
        if (collectedSprite)
        {
            spriteRenderer.sprite = collectedSprite;
        }
        else
        {
            spriteRenderer.sprite = null;
            GetComponent<BoxCollider2D>().enabled = false;
        }

        // Desativa os sprites dos filhos
        foreach (var childRenderer in childSpriteRenderers)
        {
            childRenderer.enabled = false;
        }

        if (canReplace)
        {
            yield return new WaitForSeconds(timeToReplace);
        }
        else
        {
            Destroy(GetComponent<Collect>());
            yield break;
        }

        // Restaura o estado para coleta novamente
        spriteRenderer.sprite = defSprite;
        foreach (var childRenderer in childSpriteRenderers)
        {
            childRenderer.enabled = true;
        }

        GetComponent<BoxCollider2D>().enabled = true;
        isCollected = false;
        Debug.Log("Item restaurado e pronto para coleta novamente.");
    }
}
